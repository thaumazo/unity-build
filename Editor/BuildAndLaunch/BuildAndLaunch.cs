using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace UnityBuild
{
    public class BuildAndLaunch : PostBuildAction
    {
        const string shouldPerformLaunchKey = "shouldPerformLaunch";

        #region MenuItems
        [MenuItem("Build/Launch/Build And Launch", false, 1)]
        private static void BuildAndLaunchExecution()
        {
            EditorPrefs.SetBool(shouldPerformLaunchKey, true);
            BuildProject.BuildAll();      
        }

        [MenuItem("Build/Launch/Launch", false, 2)]
        private static void Launch()
        {
            LaunchBuilds();
        }

        #endregion

        #region Public Methods

        public override void Execute()
        {
            if(EditorPrefs.GetBool(shouldPerformLaunchKey, false))
            {
                EditorPrefs.SetBool(shouldPerformLaunchKey, false);

                LaunchBuilds();
            }         
        }

        #endregion

        #region Private Methods
        private static void LaunchBuilds()
        {
            string pathToExecutable = Path.Combine(BuildSettings.buildPath, BuildAndLaunchSettings.buildPlatformPath);
            string arguments = string.Format("-Screen-fullscreen 0 -Screen-height {0} -screen-width {1}", BuildAndLaunchSettings.heightOfInstance, BuildAndLaunchSettings.widthOfInstance);

            for (int i = 0; i < BuildAndLaunchSettings.amountOfInstances; i++)
            {
                Process process = Process.Start(pathToExecutable, arguments);
            }
        }

        #endregion

        #region Public Properties

        public override int priority
        {
            get
            {
                return 1000;
            }
        }

        #endregion
    }
}