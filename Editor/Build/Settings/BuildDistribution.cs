using UnityEngine;
using UnityEditor;

namespace Unitybuild
{
    [System.Serializable]
    public class BuildDistribution
    {
        public string distributionName;
        public bool enabled;

        public BuildDistribution(string distributionName, bool enabled)
        {
            this.distributionName = distributionName;
            this.enabled = enabled;
        }
    }
}