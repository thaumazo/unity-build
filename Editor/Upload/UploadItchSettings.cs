﻿using UnityEngine;

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

        #endregion Singleton

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
        private string _itchGameName = "gameName";

        [SerializeField]
        [Tooltip("Upload version number (optional).")]
        private string _versionNumber = "";

        #endregion Variables

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

        #endregion Public Properties
    }
}