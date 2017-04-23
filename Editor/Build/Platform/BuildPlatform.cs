using System;
using System.IO;
using System.Reflection;
using UnityEditor;

namespace UnityBuild
{
    [InitializeOnLoad]
    public abstract class BuildPlatform
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

        #endregion Abstract

        #region Contructor

        /// <summary>
        /// Constructor
        /// </summary>
        static BuildPlatform()
        {
            // Find all classes that inherit from BuildPlatform and register them with BuildProject.
            Type ti = typeof(BuildPlatform);

            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type t in asm.GetTypes())
                {
                    if (ti.IsAssignableFrom(t) && ti != t)
                    {
                        BuildProject.RegisterPlatform((BuildPlatform)Activator.CreateInstance(t));
                    }
                }
            }
        }

        #endregion Contructor

        #region Public Methods

        /// <summary>
        /// Perform build for platform.
        /// </summary>
        public void Build()
        {
            BuildProject.PerformBuild(this);
        }

        #endregion Public Methods

        #region private Methods

        /// <summary>
        /// Toggle if a target platform should be built.
        /// </summary>
        /// <param name="targetName">Platform name. Passed in from descendant class.</param>
        protected static void Toggle(string targetName)
        {
            EditorPrefs.SetBool("buildGame" + targetName, !EditorPrefs.GetBool("buildGame" + targetName, false));
        }

        /// <summary>
        /// UI Validation for platform build setting.
        /// </summary>
        /// <param name="targetName">Platform name. Passed in from descendant class.</param>
        /// <returns></returns>
        protected static bool ToggleValidate(string targetName)
        {
            Menu.SetChecked("Build/Platforms/" + targetName, EditorPrefs.GetBool("buildGame" + targetName, false));
            return true;
        }

        #endregion private Methods

        #region Public Properties

        public bool buildEnabled
        {
            get
            {
                return EditorPrefs.GetBool("buildGame" + name, false);
            }
        }

        public string buildPath
        {
            get
            {
                return BuildSettings.buildPath + Path.DirectorySeparatorChar + name + Path.DirectorySeparatorChar;
            }
        }

        public string dataDirectory
        {
            get
            {
                return buildPath + string.Format(dataDirNameFormat, BuildSettings.binName) + Path.DirectorySeparatorChar;
            }
        }

        public string exeName
        {
            get
            {
                return string.Format(binaryNameFormat, BuildSettings.binName);
            }
        }

        #endregion Public Properties
    }
}