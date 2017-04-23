using UnityEngine;

namespace UnityBuild
{
    public class BuildAndLaunchSettings : BaseSettings
    {
        #region Singleton

        private static BuildAndLaunchSettings instance = null;

        public static BuildAndLaunchSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = CreateAsset<BuildAndLaunchSettings>("BuildAndLaunchSettings");
                }

                return instance;
            }
        }

        #endregion Singleton

        #region Variables

        [Header("Launch After Build Settings (Field Info in Tooltips)")]
        [SerializeField]
        [Tooltip("The relative path from the build folder to the executable.")]
        private string _pathToExecutable = "";

        [SerializeField]
        [Tooltip("The amount of game instances to launch after a build. If you want more than 16 instances you can just launch twice.")]
        [Range(1, 16)]
        private int _amountOfInstances = 1;

        [SerializeField]
        [Tooltip("The x part of the resolution of the instance.")]
        private int _widthOfInstance = 800;

        [SerializeField]
        [Tooltip("The y part of the resolution of the instance.")]
        private int _heightOfInstance = 600;

        #endregion Variables

        #region Public Properties

        public static string pathToExecutable
        {
            get
            {
                return Instance._pathToExecutable;
            }
            set
            {
                Instance._pathToExecutable = value;
            }
        }

        public static int amountOfInstances
        {
            get
            {
                return Instance._amountOfInstances;
            }
            set
            {
                Instance._amountOfInstances = value;
            }
        }

        public static int widthOfInstance
        {
            get
            {
                return Instance._widthOfInstance;
            }
            set
            {
                Instance._widthOfInstance = value;
            }
        }

        public static int heightOfInstance
        {
            get
            {
                return Instance._heightOfInstance;
            }
            set
            {
                Instance._heightOfInstance = value;
            }
        }

        #endregion Public Properties
    }
}