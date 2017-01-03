﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Unitybuild
{
    [System.Serializable]
    public class ProjectConfigurations
    {
        public SerializableDictionary<string, Configuration> configSet;

        public void Refresh()
        {
            SerializableDictionary<string, Configuration> refreshedConfigSet = new SerializableDictionary<string, Configuration>();

            BuildReleaseType[] releaseTypes = BuildSettings.releaseTypeList.releaseTypes;
            for (int i = 0; i < releaseTypes.Length; i++)
            {
                string key = releaseTypes[i].typeName;
                Configuration relConfig = new Configuration();
                SerializableDictionary<string, Configuration> prevChildConfig = null;

                if (refreshedConfigSet.ContainsKey(key))
                    continue;

                if (configSet != null && configSet.ContainsKey(key))
                {
                    relConfig.enabled = configSet[key].enabled;
                    prevChildConfig = configSet[key].childConfigurations;
                }

                relConfig.childConfigurations = RefreshPlatforms(prevChildConfig);

                refreshedConfigSet.Add(key, relConfig);
            }

            configSet = refreshedConfigSet;
        }

        public string[] BuildAllKeychains()
        {
            List<string> keychains = new List<string>();

            foreach (string key in configSet.Keys)
            {
                Configuration config = configSet[key];
                BuildKeychainsRecursive(ref keychains, config, key, "", 0);
            }

            return keychains.ToArray();
        }

        private void BuildKeychainsRecursive(ref List<string> keychains, Configuration config, string key, string currentKeychain, int depth)
        {
            if (depth >= 2 && (config.childConfigurations == null || config.childConfigurations.Count == 0))
            {
                keychains.Add(currentKeychain + "/" + key);
            }
            else if (config.childConfigurations != null && config.childConfigurations.Count > 0 && config.enabled)
            {
                if (string.IsNullOrEmpty(currentKeychain))
                    currentKeychain = key;
                else
                    currentKeychain += "/" + key;

                foreach (string childKey in config.childConfigurations.Keys)
                {
                    Configuration childConfig = config.childConfigurations[childKey];
                    BuildKeychainsRecursive(ref keychains, childConfig, childKey, currentKeychain, depth + 1);
                }
            }
        }

        public bool ParseKeychain(string keychain, out BuildReleaseType releaseType, out BuildPlatform platform, out BuildArchitecture architecture, out BuildDistribution distribution)
        {
            bool success = false;
            string[] keys = keychain.Split('/');
            int keyCount = keys.Length;
            int targetKey = 0;
            Configuration childConfig = null;

            releaseType = null;
            platform = null;
            architecture = null;
            distribution = null;

            if (keyCount > targetKey && configSet.ContainsKey(keys[targetKey]))
            {
                for (int i = 0; i < BuildSettings.releaseTypeList.releaseTypes.Length; i++)
                {
                    BuildReleaseType rt = BuildSettings.releaseTypeList.releaseTypes[i];

                    if (keys[targetKey] == rt.typeName)
                    {
                        releaseType = rt;
                        childConfig = configSet[keys[targetKey]];
                    }
                }
            }

            ++targetKey;
            if (keyCount > targetKey && childConfig != null && childConfig.childConfigurations != null && childConfig.childConfigurations.ContainsKey(keys[targetKey]))
            {
                for (int i = 0; i < BuildSettings.platformList.platforms.Length; i++)
                {
                    BuildPlatform p = BuildSettings.platformList.platforms[i];

                    if (keys[targetKey] == p.platformName)
                    {
                        platform = p;
                        childConfig = childConfig.childConfigurations[keys[targetKey]];
                    }
                }
            }

            ++targetKey;
            if (keyCount > targetKey && childConfig != null && childConfig.childConfigurations != null && childConfig.childConfigurations.ContainsKey(keys[targetKey]))
            {
                for (int i = 0; i < platform.architectures.Length; i++)
                {
                    BuildArchitecture arch = platform.architectures[i];

                    if (keys[targetKey] == arch.name)
                    {
                        architecture = arch;
                        childConfig = childConfig.childConfigurations[keys[targetKey]];
                        success = true;
                    }
                }
            }

            ++targetKey;
            if (keyCount > targetKey && childConfig != null && childConfig.childConfigurations != null && childConfig.childConfigurations.ContainsKey(keys[targetKey]))
            {
                success = false;
                for (int i = 0; i < platform.distributionList.distributions.Length; i++)
                {
                    BuildDistribution dist = platform.distributionList.distributions[i];

                    if (keys[targetKey] == dist.distributionName)
                    {
                        distribution = dist;
                        success = true;
                    }
                }
            }

            return success;
        }

        private SerializableDictionary<string, Configuration> RefreshPlatforms(SerializableDictionary<string, Configuration> prevConfigSet)
        {
            SerializableDictionary<string, Configuration> refreshedConfigSet = new SerializableDictionary<string, Configuration>();

            BuildPlatform[] platforms = BuildSettings.platformList.platforms;
            for (int i = 0; i < platforms.Length; i++)
            {
                if (!platforms[i].enabled && platforms[i].atLeastOneArch)
                    continue;

                string key = platforms[i].platformName;
                Configuration relConfig = new Configuration();
                SerializableDictionary<string, Configuration> prevChildConfig = null;

                if (refreshedConfigSet.ContainsKey(key))
                    continue;

                if (prevConfigSet != null && prevConfigSet.ContainsKey(key))
                {
                    relConfig.enabled = prevConfigSet[key].enabled;
                    prevChildConfig = prevConfigSet[key].childConfigurations;
                }

                BuildArchitecture[] architectures = platforms[i].architectures;

                if (architectures.Length > 1)
                {
                    relConfig.childConfigurations = RefreshArchitectures(architectures, platforms[i].distributionList.distributions, prevChildConfig);
                }

                refreshedConfigSet.Add(key, relConfig);
            }

            return refreshedConfigSet;
        }

        private SerializableDictionary<string, Configuration> RefreshArchitectures(BuildArchitecture[] architectures, BuildDistribution[] distributions, SerializableDictionary<string, Configuration> prevConfigSet)
        {
            SerializableDictionary<string, Configuration> refreshedConfigSet = new SerializableDictionary<string, Configuration>();

            for (int i = 0; i < architectures.Length; i++)
            {
                if (!architectures[i].enabled)
                    continue;

                string key = architectures[i].name;
                Configuration relConfig = new Configuration();
                SerializableDictionary<string, Configuration> prevChildConfig = null;

                if (refreshedConfigSet.ContainsKey(key))
                    continue;

                if (prevConfigSet != null && prevConfigSet.ContainsKey(key))
                {
                    relConfig.enabled = prevConfigSet[key].enabled;
                    prevChildConfig = prevConfigSet[key].childConfigurations;
                }

                if (distributions.Length > 0)
                    relConfig.childConfigurations = RefreshDistributions(distributions, prevChildConfig);

                refreshedConfigSet.Add(key, relConfig);
            }

            return refreshedConfigSet;
        }

        private SerializableDictionary<string, Configuration> RefreshDistributions(BuildDistribution[] distributions, SerializableDictionary<string, Configuration> prevConfigSet)
        {
            SerializableDictionary<string, Configuration> refreshedConfigSet = new SerializableDictionary<string, Configuration>();

            for (int i = 0; i < distributions.Length; i++)
            {
                if (!distributions[i].enabled)
                    continue;

                string key = distributions[i].distributionName;
                Configuration relConfig = new Configuration();
                SerializableDictionary<string, Configuration> prevChildConfig = null;

                if (refreshedConfigSet.ContainsKey(key))
                    continue;

                if (prevConfigSet != null && prevConfigSet.ContainsKey(key))
                {
                    relConfig.enabled = prevConfigSet[key].enabled;
                    prevChildConfig = prevConfigSet[key].childConfigurations;
                }

                refreshedConfigSet.Add(key, relConfig);
            }

            return refreshedConfigSet;
        }
    }
}