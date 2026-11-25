using System;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace IntergalacticNinja.Editor
{
    /// <summary>
    /// Allows CI validation builds (such as GitHub Pull Request checks) to build Android
    /// using Unity's default debug keystore when the <c>-useDebugKeystore</c> command-line
    /// argument is passed, without requiring production signing secrets or modifying
    /// <c>ProjectSettings.asset</c> in version control.
    /// </summary>
    [InitializeOnLoad]
    public class AndroidCiBuildPreprocessor : IPreprocessBuildWithReport
    {
        private const string UseDebugKeystoreFlag = "-useDebugKeystore";

        public int callbackOrder => -100;

        static AndroidCiBuildPreprocessor()
        {
            if (IsDebugKeystoreRequested())
            {
                DisableCustomKeystore("domain load");
            }
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            if (report.summary.platform != BuildTarget.Android)
                return;

            if (IsDebugKeystoreRequested())
            {
                DisableCustomKeystore("prebuild");
            }
        }

        private static bool IsDebugKeystoreRequested()
        {
            string[] args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++)
            {
                if (!string.Equals(args[i], UseDebugKeystoreFlag, StringComparison.OrdinalIgnoreCase))
                    continue;

                if (i + 1 < args.Length && !args[i + 1].StartsWith("-", StringComparison.Ordinal))
                {
                    string value = args[i + 1];
                    return !string.Equals(value, "false", StringComparison.OrdinalIgnoreCase) &&
                           !string.Equals(value, "0", StringComparison.Ordinal);
                }

                return true;
            }

            return false;
        }

        private static void DisableCustomKeystore(string phase)
        {
            Debug.Log($"[CI] {UseDebugKeystoreFlag} detected ({phase}): switching Android signing to Unity's default debug keystore.");
            PlayerSettings.Android.useCustomKeystore = false;
            PlayerSettings.Android.keystoreName = string.Empty;
            PlayerSettings.Android.keystorePass = string.Empty;
            PlayerSettings.Android.keyaliasName = string.Empty;
            PlayerSettings.Android.keyaliasPass = string.Empty;
        }
    }
}
