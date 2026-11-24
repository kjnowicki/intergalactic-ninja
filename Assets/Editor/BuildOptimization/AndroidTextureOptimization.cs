using System;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace IntergalacticNinja.Editor
{
    public class AndroidTextureOptimization : AssetPostprocessor, IPreprocessBuildWithReport
    {
        private const string DesignRoot = "Assets/Design";
        private const string AndroidPlatformName = "Android";

        public int callbackOrder => -10;

        private void OnPreprocessTexture()
        {
            if (!IsManagedDesignTexture(assetPath))
                return;

            var importer = assetImporter as TextureImporter;
            if (importer == null)
                return;

            ApplyAndroidSettings(importer, assetPath);
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            if (report.summary.platform != BuildTarget.Android)
                return;

            EditorUserBuildSettings.androidBuildSubtarget = MobileTextureSubtarget.ASTC;
            ApplyToAllDesignTextures();
        }

        public static int ApplyToAllDesignTextures()
        {
            EditorUserBuildSettings.androidBuildSubtarget = MobileTextureSubtarget.ASTC;

            if (!AssetDatabase.IsValidFolder(DesignRoot))
                return 0;

            string[] guids = AssetDatabase.FindAssets("t:Texture2D", new[] { DesignRoot });
            int updatedCount = 0;

            AssetDatabase.StartAssetEditing();
            try
            {
                foreach (string guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    if (!IsManagedDesignTexture(path))
                        continue;

                    var importer = AssetImporter.GetAtPath(path) as TextureImporter;
                    if (importer == null)
                        continue;

                    if (ApplyAndroidSettings(importer, path))
                    {
                        importer.SaveAndReimport();
                        updatedCount++;
                    }
                }
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }

            AssetDatabase.SaveAssets();
            return updatedCount;
        }

        private static bool IsManagedDesignTexture(string assetPath)
        {
            return !string.IsNullOrEmpty(assetPath) &&
                   assetPath.StartsWith(DesignRoot + "/", StringComparison.OrdinalIgnoreCase);
        }

        private static bool ApplyAndroidSettings(TextureImporter importer, string assetPath)
        {
            GetTargetSettings(assetPath, out int maxSize, out TextureImporterFormat format, out TextureImporterCompression compression);

            TextureImporterPlatformSettings current = importer.GetPlatformTextureSettings(AndroidPlatformName);
            if (current.overridden &&
                current.maxTextureSize == maxSize &&
                current.format == format &&
                current.textureCompression == compression &&
                current.compressionQuality == 50)
            {
                return false;
            }

            var updated = new TextureImporterPlatformSettings
            {
                name = AndroidPlatformName,
                overridden = true,
                maxTextureSize = maxSize,
                format = format,
                textureCompression = compression,
                compressionQuality = 50,
                crunchedCompression = false,
                allowsAlphaSplitting = false
            };

            importer.SetPlatformTextureSettings(updated);
            return true;
        }

        private static bool IsPackedIntoSpriteAtlas(string normalizedPath)
        {
            if (normalizedPath.StartsWith("Assets/Design/Animation/Hacking/", StringComparison.OrdinalIgnoreCase) ||
                normalizedPath.StartsWith("Assets/Design/Animations/WallJumping/", StringComparison.OrdinalIgnoreCase) ||
                normalizedPath.StartsWith("Assets/Design/Animation/NinjaHero/DefaultElement/", StringComparison.OrdinalIgnoreCase) ||
                normalizedPath.StartsWith("Assets/Design/Animation/NinjaHero/Element1/Idle/", StringComparison.OrdinalIgnoreCase) ||
                normalizedPath.StartsWith("Assets/Design/Animation/NinjaHero/Element2/Idle/", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (normalizedPath.StartsWith("Assets/Design/Images/CockpitActionHub/", StringComparison.OrdinalIgnoreCase) &&
                normalizedPath.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
            {
                string fileName = System.IO.Path.GetFileName(normalizedPath);
                if (fileName.StartsWith("CockpitActionHub_dev01_", StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        private static void GetTargetSettings(
            string assetPath,
            out int maxSize,
            out TextureImporterFormat format,
            out TextureImporterCompression compression)
        {
            string normalized = assetPath.Replace('\\', '/');

            // Sprites packed into SpriteAtlas V2: keep source uncompressed so the atlas
            // compresses once to ASTC_6x6 without double-compression or Unity warnings.
            if (IsPackedIntoSpriteAtlas(normalized))
            {
                maxSize = normalized.StartsWith("Assets/Design/Images/", StringComparison.OrdinalIgnoreCase) ? 1024 : 512;
                format = TextureImporterFormat.RGBA32;
                compression = TextureImporterCompression.Uncompressed;
                return;
            }

            compression = TextureImporterCompression.Compressed;

            // Full-screen background loops and static web/background images
            if (normalized.StartsWith("Assets/Design/Animation/NinjaHero/Background/", StringComparison.OrdinalIgnoreCase) ||
                normalized.IndexOf("NinjaHeroBackground", StringComparison.OrdinalIgnoreCase) >= 0 ||
                normalized.IndexOf("web-background", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                maxSize = 1024;
                format = TextureImporterFormat.ASTC_8x8;
                return;
            }

            // Large standalone frame animations (Killer, unreferenced NinjaHero Destroy)
            if (normalized.StartsWith("Assets/Design/Animations/Killer/", StringComparison.OrdinalIgnoreCase) ||
                normalized.StartsWith("Assets/Design/Animation/NinjaHero/", StringComparison.OrdinalIgnoreCase))
            {
                maxSize = 512;
                format = TextureImporterFormat.ASTC_6x6;
                return;
            }

            // UI and hero images under Assets/Design/Images/ and any remaining Design textures
            maxSize = 1024;
            format = TextureImporterFormat.ASTC_6x6;
        }
    }
}
