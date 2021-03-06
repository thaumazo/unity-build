﻿using System.IO;
using UnityEditor;
using UnityEngine;

namespace UnityBuild
{
    public class UploadItch : PostBuildAction
    {
        private const string WINDOWS = "windows";
        private const string OSX = "osx";
        private const string LINUX = "linux";

        private const string shouldUploadItchKey = "shouldUploadItch";

        #region MenuItems

        [MenuItem(executeBasePath + "Upload Itch")]
        private static void UploadAll()
        {
            for (int i = 0; i < BuildProject.platforms.Count; i++)
            {
                BuildPlatform platform = BuildProject.platforms[i];
                PerformUpload(platform);
            }
        }

        [MenuItem(customizeBuildBasePath + "Auto Upload to Itch")]
        private static void ToggleAutoUpload()
        {
            EditorPrefs.SetBool(shouldUploadItchKey, !EditorPrefs.GetBool(shouldUploadItchKey, false));
        }

        [MenuItem(customizeBuildBasePath + "Auto Upload to Itch", true)]
        private static bool ToggleAutoUploadValidate()
        {
            Menu.SetChecked(customizeBuildBasePath + "Auto Upload to Itch", EditorPrefs.GetBool(shouldUploadItchKey, false));
            return true;
        }

        [MenuItem(settingsBasePath + "Upload To Itch Settings")]
        public static void EditSettings()
        {
            Selection.activeObject = UploadItchSettings.Instance;
            EditorApplication.ExecuteMenuItem("Window/Inspector");
        }

        #endregion MenuItems

        #region Public Methods

        public override void Execute(BuildPlatform platform)
        {
            if (EditorPrefs.GetBool(shouldUploadItchKey, false))
            {
                PerformUpload(platform);
            }
        }

        #endregion Public Methods

        #region private Methods

        private static void PerformUpload(BuildPlatform platform)
        {
            if (!platform.buildEnabled)
            {
                return;
            }

            string absolutePath = Path.GetFullPath(platform.buildPath).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            if (File.Exists(absolutePath))
            {
                Debug.Log("UploadItch: Upload Failed - Build does not exist for platform " + platform.name + " - " + absolutePath);
                return;
            }

            string channel = platform.name;

            //string channel = GetChannelName(platform.target);
            if (string.IsNullOrEmpty(channel))
            {
                Debug.Log("UploadItch: Upload Failed - Unknown platform " + platform.name);
                return;
            }

            string arguments = "push \"" + absolutePath + "\" " + UploadItchSettings.itchUserName + "/" + UploadItchSettings.itchGameName + ":" + channel;

            if (!string.IsNullOrEmpty(UploadItchSettings.versionNumber))
            {
                arguments += "--userversion " + UploadItchSettings.versionNumber;
            }

            System.Diagnostics.Process uploadProc = new System.Diagnostics.Process();
            uploadProc.StartInfo.FileName = UploadItchSettings.butlerPath;

            uploadProc.StartInfo.Arguments = arguments;
            uploadProc.StartInfo.CreateNoWindow = false;
            uploadProc.StartInfo.UseShellExecute = false;
            uploadProc.Start();
        }

        private static string GetChannelName(BuildTarget target)
        {
            switch (target)
            {
                // Windows
                case BuildTarget.StandaloneWindows:
                    return WINDOWS + "-x86";

                case BuildTarget.StandaloneWindows64:
                    return WINDOWS + "-x64";

                // Linux
                case BuildTarget.StandaloneLinux:
                    return LINUX + "-x86";

                case BuildTarget.StandaloneLinux64:
                    return LINUX + "-x64";

                case BuildTarget.StandaloneLinuxUniversal:
                    return LINUX + "-universal";

                // OSX
                case BuildTarget.StandaloneOSXIntel:
                    return OSX + "-intel";

                case BuildTarget.StandaloneOSXIntel64:
                    return OSX + "-intel64";

                case BuildTarget.StandaloneOSXUniversal:
                    return OSX + "-universal";

                default:
                    return null;
            }
        }

        #endregion private Methods

        #region Public Properties

        public override int priority
        {
            get
            {
                return 1000;
            }
        }

        #endregion Public Properties
    }
}