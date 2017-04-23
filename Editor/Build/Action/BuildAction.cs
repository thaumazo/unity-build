using System;

namespace UnityBuild
{
    public abstract class BuildAction : IComparable<BuildAction>
    {
        protected const string settingsBasePath = "Build/Edit Settings/";
        protected const string customizeBuildBasePath = "Build/Customize Build/";
        protected const string executeBasePath = "Build/Execute/";

        /// <summary>
        /// Build action.
        /// </summary>
        public virtual void Execute()
        {
        }

        /// <summary>
        /// Platform-specific build action.
        /// </summary>
        /// <param name="platform"></param>
        public virtual void Execute(BuildPlatform platform)
        {
        }

        /// <summary>
        /// Priority of this build action. Lower values execute earlier.
        /// </summary>
        public virtual int priority
        {
            get
            {
                return 100;
            }
        }

        #region IComparable

        public int CompareTo(BuildAction other)
        {
            if (other == null)
                return 1;
            else
                return priority.CompareTo(other.priority);
        }

        #endregion IComparable
    }
}