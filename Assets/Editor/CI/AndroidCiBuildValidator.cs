using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.Build.Player;
using UnityEditor.Compilation;
using UnityEngine;

namespace IntergalacticNinja.Editor
{
    /// <summary>
    /// Fast CI validation entry point for PR checks targeting <c>develop</c>.
    /// Verifies enabled scenes exist and compiles all runtime C# scripts for
    /// <see cref="BuildTarget.Android"/> (with <c>UNITY_ANDROID</c> and without
    /// <c>UNITY_EDITOR</c>) without running IL2CPP or Gradle packaging.
    /// </summary>
    public static class AndroidCiBuildValidator
    {
        private const string ReportRelativePath = "build/Android/compile-validation.txt";
        private const string TempOutputRelativePath = "Temp/CiAndroidScriptCompile";

        public static void ValidateAndroidScriptCompilation()
        {
            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            string reportPath = Path.Combine(projectRoot, ReportRelativePath);
            string tempOutputDir = Path.Combine(projectRoot, TempOutputRelativePath);

            // Clear any stale report so CI cannot mistake a previous run for success.
            if (File.Exists(reportPath))
                File.Delete(reportPath);

            var errors = new List<string>();
            var warnings = new List<string>();

            // 1. Validate enabled build scenes exist on disk.
            EditorBuildSettingsScene[] enabledScenes = EditorBuildSettings.scenes
                .Where(scene => scene.enabled)
                .ToArray();

            if (enabledScenes.Length == 0)
            {
                errors.Add("No enabled scenes are configured in EditorBuildSettings.");
            }
            else
            {
                foreach (EditorBuildSettingsScene scene in enabledScenes)
                {
                    string sceneFullPath = Path.Combine(projectRoot, scene.path);
                    if (!File.Exists(sceneFullPath))
                    {
                        errors.Add($"Enabled scene in Build Settings does not exist on disk: {scene.path}");
                    }
                }
            }

            // 2. Compile player scripts for Android (release defines: UNITY_ANDROID, !UNITY_EDITOR).
            if (Directory.Exists(tempOutputDir))
                Directory.Delete(tempOutputDir, recursive: true);
            Directory.CreateDirectory(tempOutputDir);

            void OnAssemblyCompilationFinished(string assemblyPath, CompilerMessage[] messages)
            {
                if (messages == null)
                    return;

                foreach (CompilerMessage message in messages)
                {
                    string location = string.IsNullOrEmpty(message.file)
                        ? assemblyPath
                        : $"{message.file}({message.line},{message.column})";

                    if (message.type == CompilerMessageType.Error)
                    {
                        errors.Add($"{location}: {message.message}");
                    }
                    else if (message.type == CompilerMessageType.Warning)
                    {
                        warnings.Add($"{location}: {message.message}");
                    }
                }
            }

            ScriptCompilationResult result = default;
            CompilationPipeline.assemblyCompilationFinished += OnAssemblyCompilationFinished;
            try
            {
                var settings = new ScriptCompilationSettings
                {
                    target = BuildTarget.Android,
                    group = BuildTargetGroup.Android,
                    options = ScriptCompilationOptions.None
                };

                Debug.Log("[CI] Starting Android player script compilation check...");
                result = PlayerBuildInterface.CompilePlayerScripts(settings, tempOutputDir);
            }
            catch (Exception ex)
            {
                errors.Add($"Exception during Android script compilation: {ex}");
            }
            finally
            {
                CompilationPipeline.assemblyCompilationFinished -= OnAssemblyCompilationFinished;
            }

            List<string> assemblies = result.assemblies != null
                ? result.assemblies.Where(a => !string.IsNullOrEmpty(a)).OrderBy(a => a, StringComparer.Ordinal).ToList()
                : new List<string>();

            if (assemblies.Count == 0)
            {
                errors.Add("PlayerBuildInterface.CompilePlayerScripts produced 0 assemblies.");
            }

            if (errors.Count > 0)
            {
                Debug.LogError($"[CI] Android script compilation check FAILED with {errors.Count} error(s):");
                foreach (string error in errors)
                {
                    Debug.LogError($"[CI] {error}");
                }

                if (Application.isBatchMode)
                {
                    EditorApplication.Exit(1);
                    return;
                }

                throw new InvalidOperationException($"Android script compilation check failed with {errors.Count} error(s). See Console for details.");
            }

            string reportDir = Path.GetDirectoryName(reportPath);
            if (!string.IsNullOrEmpty(reportDir) && !Directory.Exists(reportDir))
                Directory.CreateDirectory(reportDir);

            var report = new StringBuilder();
            report.AppendLine("status=passed");
            report.AppendLine($"target={BuildTarget.Android}");
            report.AppendLine($"unity_version={Application.unityVersion}");
            report.AppendLine($"enabled_scenes={enabledScenes.Length}");
            report.AppendLine($"compiled_assemblies={assemblies.Count}");
            report.AppendLine($"warnings={warnings.Count}");
            foreach (string assembly in assemblies)
            {
                report.AppendLine($"assembly={assembly}");
            }

            File.WriteAllText(reportPath, report.ToString(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            Debug.Log($"[CI] Android script compilation check PASSED: {assemblies.Count} assemblies compiled, {enabledScenes.Length} scenes verified, {warnings.Count} warning(s).");

            if (Application.isBatchMode)
            {
                EditorApplication.Exit(0);
            }
        }
    }
}
