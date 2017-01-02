using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace UnityBuild
{
    [InitializeOnLoad]
    public abstract class BuildPlatform : ScriptableObject
    {
        #region Abstract

        /// <summary>
        /// Unity build target definition.
        /// </summary>
        public abstract BuildTarget target { get; }

        /// <summary>
        /// Platform name.
        /// </summary>
        public abstract string name { get; }

        /// <summary>
        /// The format of the binary executable name (e.g. {0}.exe). {0} = Executable name specified in BuildSettings.
        /// </summary>
        public abstract string binaryNameFormat { get; }

        /// <summary>
        /// The format of the data directory (e.g. {0}_Data). {0} = Executable name specified in BuildSettings.
        /// </summary>
        public abstract string dataDirNameFormat { get; }

        #endregion
    }
}