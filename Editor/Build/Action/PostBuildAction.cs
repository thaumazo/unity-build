using System;
using System.Reflection;
using UnityEditor;

namespace UnityBuild
{
    [InitializeOnLoad]
    public abstract class PostBuildAction : BuildAction
    {
        #region Contructor

        /// <summary>
        /// Constructor
        /// </summary>
        static PostBuildAction()
        {
            // Find all classes that inherit from BuildPlatform and register them with BuildProject.
            Type childClasses = typeof(PostBuildAction);

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type t in assembly.GetTypes())
                {
                    if (childClasses.IsAssignableFrom(t) && childClasses != t)
                    {
                        BuildProject.RegisterPostBuildAction((BuildAction)Activator.CreateInstance(t));
                    }
                }
            }
        }

        #endregion
    }
}