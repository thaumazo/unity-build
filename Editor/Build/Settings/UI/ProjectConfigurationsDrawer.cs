﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Unitybuild
{
    [CustomPropertyDrawer(typeof(ProjectConfigurations))]
    public class ProjectConfigurationsDrawer : PropertyDrawer
    {
        private bool show = false;
        private bool showViewOptions = false;
        private bool showConfigs = false;
        private bool showBuildInfo = false;

        private bool hideDisabled = true;
        private bool treeView = true;

        private string selectedKeyChain = "";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            EditorGUILayout.BeginHorizontal();
            UnityBuildGUIUtility.DropdownHeader("Build Configurations", ref show, GUILayout.ExpandWidth(true));
            UnityBuildGUIUtility.HelpButton("Parameter-Details#Release-Types");
            EditorGUILayout.EndHorizontal();

            if (show)
            {
                EditorGUILayout.BeginVertical(UnityBuildGUIUtility.dropdownContentStyle);

                EditorGUILayout.BeginHorizontal();
                UnityBuildGUIUtility.DropdownHeader("View Options", ref showViewOptions, GUILayout.ExpandWidth(true));
                EditorGUILayout.EndHorizontal();

                if (showViewOptions)
                {
                    EditorGUILayout.BeginVertical(UnityBuildGUIUtility.dropdownContentStyle);

                    hideDisabled = EditorGUILayout.ToggleLeft("Hide disabled configurations", hideDisabled);
                    treeView = EditorGUILayout.ToggleLeft("Show full configurations tree", treeView);

                    EditorGUILayout.EndVertical();
                }

                GUILayout.Space(5);
                EditorGUILayout.BeginHorizontal();
                UnityBuildGUIUtility.DropdownHeader("Configurations", ref showConfigs, GUILayout.ExpandWidth(true));
                EditorGUILayout.EndHorizontal();


                if (showConfigs)
                {
                    EditorGUILayout.BeginVertical(UnityBuildGUIUtility.dropdownContentStyle);

                    foreach (string key in BuildSettings.projectConfigurations.configSet.Keys)
                    {
                        Configuration config = BuildSettings.projectConfigurations.configSet[key];
                        DisplayConfigTree(key, ref config, 0);

                        BuildSettings.projectConfigurations.configSet[key] = config;
                    }


                    EditorGUILayout.EndVertical();
                }

                GUILayout.Space(5);
                EditorGUILayout.BeginHorizontal();
                UnityBuildGUIUtility.DropdownHeader("Build Info", ref showBuildInfo, GUILayout.ExpandWidth(true));
                EditorGUILayout.EndHorizontal();

                if (showBuildInfo)
                {
                    EditorGUILayout.BeginVertical(UnityBuildGUIUtility.dropdownContentStyle);

                    if (string.IsNullOrEmpty(selectedKeyChain))
                    {
                        EditorGUILayout.HelpBox("Click a build configuration above in \"Configurations\" to view full details.", MessageType.Info);
                    }
                    else
                    {
                        BuildReleaseType releaseType;
                        BuildPlatform platform;
                        BuildArchitecture arch;
                        BuildDistribution dist;

                        BuildSettings.projectConfigurations.ParseKeychain(selectedKeyChain, out releaseType, out platform, out arch, out dist);

                        EditorGUILayout.LabelField("Misc Info", UnityBuildGUIUtility.midHeaderStyle);
                        EditorGUILayout.LabelField("Defines:");
                        EditorGUILayout.LabelField(BuildProject.GenerateDefaultDefines(releaseType, platform, arch, dist), EditorStyles.wordWrappedLabel);
                        EditorGUILayout.LabelField("Output Build Directory:");

                        if (releaseType != null)
                        {
                            EditorGUILayout.LabelField("Release Type", UnityBuildGUIUtility.midHeaderStyle);
                            EditorGUILayout.LabelField("Type Name:\t" + releaseType.typeName);
                            EditorGUILayout.LabelField("Bundle Identifier:\t" + releaseType.bundleIndentifier);
                            EditorGUILayout.LabelField("Product Name:\t" + releaseType.productName);
                        }

                        if (platform != null)
                        {
                            EditorGUILayout.LabelField("Platform", UnityBuildGUIUtility.midHeaderStyle);
                            EditorGUILayout.LabelField("Name:\t\t" + platform.platformName);
                        }

                        if (arch != null)
                        {
                            EditorGUILayout.LabelField("Architecture", UnityBuildGUIUtility.midHeaderStyle);
                            EditorGUILayout.LabelField("Name:\t\t" + arch.name);
                        }

                        if (dist != null)
                        {
                            EditorGUILayout.LabelField("Distribution", UnityBuildGUIUtility.midHeaderStyle);
                            EditorGUILayout.LabelField("Name:\t\t" + dist.distributionName);
                        }

                        GUILayout.Space(20);
                        GUILayout.BeginHorizontal();
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("Build This Config Now", GUILayout.MaxWidth(200)))
                        {

                        }
                        GUILayout.FlexibleSpace();
                        GUILayout.EndHorizontal();
                    }

                    EditorGUILayout.EndVertical();
                }

                property.serializedObject.ApplyModifiedProperties();

                EditorGUILayout.EndVertical();
            }

            EditorGUI.EndProperty();
        }

        private void DisplayConfigTree(string key, ref Configuration config, int depth, bool enabled = true, string keychain = "")
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginDisabledGroup(!enabled);

            bool displayButton = (depth >= 2 && (config.childConfigurations == null || config.childConfigurations.Count == 0));

            if (treeView)
            {
                GUILayout.Space(20 * depth);
                config.enabled = EditorGUILayout.Toggle(config.enabled, GUILayout.ExpandWidth(false), GUILayout.MaxWidth(10));
            }

            if (displayButton)
            {
                string displayText;

                if (treeView)
                {
                    displayText = key;
                }
                else
                {
                    displayText = keychain + "/" + key;
                }

                if (GUILayout.Button(displayText, UnityBuildGUIUtility.dropdownHeaderStyle))
                {
                    selectedKeyChain = keychain + "/" + key;
                }
            }
            else if (treeView)
            {
                EditorGUILayout.LabelField(key, UnityBuildGUIUtility.midHeaderStyle);
            }

            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();

            if (!treeView && displayButton)
            {
                GUILayout.Space(5);
            }

            if (config.childConfigurations != null && config.childConfigurations.Count > 0 && (!hideDisabled || config.enabled))
            {
                if (string.IsNullOrEmpty(keychain))
                    keychain = key;
                else
                    keychain += "/" + key;

                foreach (string childKey in config.childConfigurations.Keys)
                {
                    Configuration childConfig = config.childConfigurations[childKey];
                    DisplayConfigTree(childKey, ref childConfig, depth + 1, config.enabled && enabled, keychain);
                }
            }
        }
    }
}