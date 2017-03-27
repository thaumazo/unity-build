using UnityEditor;

namespace UnityBuild
{
    public static class BuildPlatformGenerator
    {
        [MenuItem("Build/Generate/BuildPlatform")]
        private static void GenerateBuildSettings()
        {
            Generator.Generate("BuildCustomPlatform.cs", "BuildPlatform", "BuildPlatformTemplate");
        }
    }
}