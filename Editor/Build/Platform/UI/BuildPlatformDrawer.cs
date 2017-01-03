﻿using UnityEngine;
using UnityEditor;

namespace Unitybuild
{
    [CustomPropertyDrawer(typeof(BuildPlatform))]
    public class BuildPlatformDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, GUIContent.none, property);

            bool show = property.isExpanded;
            UnityBuildGUIUtility.DropdownHeader(property.FindPropertyRelative("platformName").stringValue, ref show);
            property.isExpanded = show;

            if (show)
            {
                EditorGUILayout.BeginVertical(UnityBuildGUIUtility.dropdownContentStyle);

                SerializedProperty archList = property.FindPropertyRelative("architectures");

                if (archList.arraySize > 1)
                {
                    GUILayout.Label("Architectures", UnityBuildGUIUtility.midHeaderStyle);
                    for (int i = 0; i < archList.arraySize; i++)
                    {
                        SerializedProperty archProperty = archList.GetArrayElementAtIndex(i);
                        SerializedProperty archName = archProperty.FindPropertyRelative("name");
                        SerializedProperty archEnabled = archProperty.FindPropertyRelative("enabled");

                        archEnabled.boolValue = GUILayout.Toggle(archEnabled.boolValue, archName.stringValue);
                        archProperty.serializedObject.ApplyModifiedProperties();
                    }
                }

                SerializedProperty distList = property.FindPropertyRelative("distributionList.distributions");

                if (distList.arraySize > 0)
                {
                    GUILayout.Space(20);
                    GUILayout.Label("Distributions", UnityBuildGUIUtility.midHeaderStyle);

                    for (int i = 0; i < distList.arraySize; i++)
                    {
                        SerializedProperty dist = distList.GetArrayElementAtIndex(i);
                        SerializedProperty distEnabled = dist.FindPropertyRelative("enabled");
                        SerializedProperty distName = dist.FindPropertyRelative("distributionName");

                        GUILayout.BeginHorizontal();

                        distEnabled.boolValue = GUILayout.Toggle(distEnabled.boolValue, GUIContent.none, GUILayout.ExpandWidth(false));
                        distName.stringValue = GUILayout.TextField(distName.stringValue);

                        if (GUILayout.Button("X", UnityBuildGUIUtility.helpButtonStyle))
                        {
                            distList.DeleteArrayElementAtIndex(i);
                        }

                        dist.serializedObject.ApplyModifiedProperties();

                        GUILayout.EndHorizontal();
                    }
                }

                GUILayout.Space(10);
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.BeginVertical();
                if (GUILayout.Button("Add Distribution", GUILayout.MaxWidth(150)))
                {
                    int addedIndex = distList.arraySize;
                    distList.InsertArrayElementAtIndex(addedIndex);
                    property.serializedObject.ApplyModifiedProperties();
                    GUIUtility.keyboardControl = 0;
                }
                if (GUILayout.Button("Delete", GUILayout.MaxWidth(150)))
                {
                    property.FindPropertyRelative("enabled").boolValue = false;
                }
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                EditorGUILayout.EndVertical();
            }

            property.serializedObject.ApplyModifiedProperties();

            EditorGUI.EndProperty();
        }
    }
}