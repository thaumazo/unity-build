using UnityEngine;
using UnityEditor;

namespace Unitybuild
{
    [System.Serializable]
    public class BuildPlatformList
    {
        [SerializeField]
        public BuildPlatform[] platforms = new BuildPlatform[] 
        {
            new BuildPC(),
            new BuildOSX(),
            new BuildLinux(),
            new BuildAndroid()
        };
    }
}