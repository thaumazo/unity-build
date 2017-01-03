using UnityEngine;
using UnityEditor;

namespace UnityBuild
{
    public class UploadItchSettings : BaseSettings
    {
        #region Singleton

        private static UploadItchSettings instance = null;

        public static UploadItchSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = CreateAsset<UploadItchSettings>("UploadItchSettings");
                }

                return instance;
            }
        }
        
        #endregion

        #region MenuItems

        [MenuItem("Build/Upload/itch.io/Edit Settings", priority = 0)]
        public static void EditSettings()
        {
            Selection.activeObject = Instance;
            EditorApplication.ExecuteMenuItem("Window/Inspector");
        }

        #endregion

        #region Variables

        [Header("Itch.io Upload Settings (Field Info in Tooltips)")]

        [SerializeField]
        [Tooltip("Path to butler executable. If you added butler to your path variable you can get this using the \"butler which\" command.")]
        private string _butlerPath = "";

        [SerializeField]
        [Tooltip("itch.io username.")]
        private string _itchUserName = "username";

        [SerializeField]
        [Tooltip("itch.io project name.")]
        private string _itchGameName = Application.productName;

        [SerializeField]
        [Tooltip("Upload version number (optional).")]
        private string _versionNumber = "";

        #endregion
        
        #region Public Properties

        public static string versionNumber
        {
            get
            {
                return Instance._versionNumber;
            }
            set
            {
                Instance._versionNumber = value;
            }
        }

        public static string butlerPath
        {
            get
            {
                return Instance._butlerPath;
            }
        }

        public static string itchUserName
        {
            get
            {
                return Instance._itchUserName;
            }
        }

        public static string itchGameName
        {
            get
            {
                return Instance._itchGameName;
            }
        }

        #endregion
    }
}