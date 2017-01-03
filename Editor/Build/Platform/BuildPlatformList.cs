﻿using UnityEngine;
using UnityEditor;

namespace SuperSystems.UnityBuild
{
    [System.Serializable]
    public class BuildPlatformList
    {
        [SerializeField]
        public BuildPlatform[] platforms = new BuildPlatform[] {
        new BuildPC(),
        new BuildOSX(),
        new BuildLinux()
    };
    }
}