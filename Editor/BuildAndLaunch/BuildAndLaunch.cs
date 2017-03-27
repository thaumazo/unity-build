using UnityEditor;
using System.IO;
using System.Diagnostics;

namespace UnityBuild
{
    public class BuildAndLaunch : PostBuildAction
    {
        private const string shouldPerformLaunchKey = "shouldPerformLaunch";

        #region MenuItems

        [MenuItem(executeBasePath + "Launch Builds")]
        private static void Launch()
        {
            LaunchBuilds();
        }

        [MenuItem(settingsBasePath + "Build And Launch Settings")]
        public static void EditSettings()
        {
            Selection.activeObject = BuildAndLaunchSettings.Instance;
            EditorApplication.ExecuteMenuItem("Window/Inspector");
        }

        [MenuItem(customizeBuildBasePath + "Launch After Build")]
        private static void ToggleAutoUpload()
        {
            EditorPrefs.SetBool(shouldPerformLaunchKey, !EditorPrefs.GetBool(shouldPerformLaunchKey, false));
        }

        [MenuItem(customizeBuildBasePath + "Launch After Build", true)]
        private static bool ToggleAutoUploadValidate()
        {
            Menu.SetChecked(customizeBuildBasePath + "Launch After Build", EditorPrefs.GetBool(shouldPerformLaunchKey, false));
            return true;
        }
        #endregion

        #region Public Methods

        public override void Execute()
        {
            if(EditorPrefs.GetBool(shouldPerformLaunchKey, false))
            {
                LaunchBuilds();
            }         
        }

        public override void Execute(BuildPlatform platform)
        {
        }

        #endregion

        #region Private Methods
        private static void LaunchBuilds()
        {
            string pathToExecutable = Path.Combine(BuildSettings.buildPath, BuildAndLaunchSettings.pathToExecutable);
            string arguments = string.Format("-Screen-fullscreen 0 -Screen-height {0} -screen-width {1}", BuildAndLaunchSettings.heightOfInstance, BuildAndLaunchSettings.widthOfInstance);

            for (int i = 0; i < BuildAndLaunchSettings.amountOfInstances; i++)
            {
                Process.Start(pathToExecutable, arguments);
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