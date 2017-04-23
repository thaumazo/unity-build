using System.IO;
using UnityEditor;
using UnityEngine;

namespace UnityBuild
{
    [InitializeOnLoad]
    public class BuildSettings : BaseSettings
    {
        #region Singleton

        private static BuildSettings instance = null;

        public static BuildSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = CreateAsset<BuildSettings>("BuildSettings");
                }

                return instance;
            }
        }

        #endregion Singleton

        #region Variables

        [Header("Build Settings (Field Info in Tooltips)")]

        // The name of executable file (e.g. mygame.exe, mygame.app)
        [SerializeField]
        [Tooltip("The name of executable file (e.g. mygame.exe, mygame.app)")]
        private string _binName = "";

        // The base path where builds are output.
        // Path is relative to the Unity project's base folder unless an absolute path is given.
        [SerializeField]
        [Tooltip("The base path where builds are output.")]
        private string _buildPath = "Builds";

        // A list of scenes (filepaths) to include in the build. The first listed scene will be loaded first.
        [SerializeField]
        [Tooltip("A list of scenes to include in the build. First listed scene will be loaded first. ")]
        private string[] _scenesInBuild = new string[] {
        @"Scenes/Example"
    };

        // A list of files/directories to include with the build.
        // Paths are relative to Unity project's base folder unless an absolute path is given.
        [SerializeField]
        [Tooltip("A list of files/directories to include with the build.")]
        private string[] _copyToBuild = new string[] {
        @"ExampleDirectoryToInclude/"
        // @"FileToInclude.txt",
        // ...
    };

        #endregion Variables

        #region Methods & Properties

        public static string binName
        {
            get { return Instance._binName; }
        }

        public static string buildPath
        {
            get { return Instance._buildPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar); }
        }

        public static string[] scenesInBuild
        {
            get { return Instance._scenesInBuild; }
        }

        public static string[] copyToBuild
        {
            get { return Instance._copyToBuild; }
        }

        #endregion Methods & Properties
    }
}