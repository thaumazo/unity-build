﻿using UnityEditor;
using UnityEngine;

namespace UnityBuild
{
    [InitializeOnLoad]
    public class BuildAssetBundlesSettings : BaseSettings
    {
        #region Singleton

        private static BuildAssetBundlesSettings instance = null;

        public static BuildAssetBundlesSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = CreateAsset<BuildAssetBundlesSettings>("BuildAssetBundlesSettings");
                }

                return instance;
            }
        }

        #endregion Singleton

        #region Variables

        [Header("AssetBundle Build Settings (Field Info in Tooltips)")]

        /// <summary>
        /// The path where AssetBundles are built. {0} = binPath
        /// </summary>
        [SerializeField]
        [Tooltip("The path where AssetBundles are built. {0} = binPath")]
        private string _buildPath = "{0}/Bundles";

        /// <summary>
        /// Flag indicating if the AssetBundles should be copied into the game's data directory.
        /// </summary>
        [SerializeField]
        [Tooltip("Flag indicating if the AssetBundles should be copied into the game's data directory.")]
        private bool _copyToBuild = true;

        #endregion Variables

        #region Public Properties

        public static string buildPath
        {
            get
            {
                return string.Format(Instance._buildPath, BuildSettings.buildPath);
            }
        }

        public static bool copyToBuild
        {
            get
            {
                return Instance._copyToBuild;
            }
        }

        #endregion Public Properties
    }
}