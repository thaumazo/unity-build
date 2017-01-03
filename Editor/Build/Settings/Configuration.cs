using UnityEngine;
using UnityEditor;

namespace Unitybuild
{

    [System.Serializable]
    public class Configuration
    {
        public bool enabled = true;
        public SerializableDictionary<string, Configuration> childConfigurations;
    }
}