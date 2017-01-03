﻿using UnityEngine;
using UnityEditor;

namespace Unitybuild
{
    public class UnityBuildWindow : EditorWindow
    {
        private Vector2 scrollPos = Vector2.zero;

        #region MenuItems

        //[MenuItem("Window/SuperUnityBuild")]
        [MenuItem("Window/UnityBuild")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow<UnityBuildWindow>();
        }

        #endregion

        protected void OnEnable()
        {
            //GUIContent title = new GUIContent("SuperUnityBuild");
            GUIContent title = new GUIContent("UnityBuild");
            titleContent = title;
        }

        protected void OnGUI()
        {
            GUIStyle mainTitleStyle = new GUIStyle(EditorStyles.centeredGreyMiniLabel);
            mainTitleStyle.fontSize = 18;
            mainTitleStyle.fontStyle = FontStyle.Bold;
            mainTitleStyle.fixedHeight = 30;

            GUIStyle subTitleStyle = new GUIStyle(mainTitleStyle);
            subTitleStyle.fontSize = 11;
            subTitleStyle.fontStyle = FontStyle.Normal;

            SerializedObject obj = new SerializedObject(BuildSettings.Instance);

            EditorGUILayout.LabelField("Unity Build", mainTitleStyle);
            //EditorGUILayout.LabelField("Super Unity Build", mainTitleStyle);
            //EditorGUILayout.LabelField("by Super Systems Softworks", subTitleStyle);
            GUILayout.Space(10);

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);

            EditorGUILayout.PropertyField(obj.FindProperty("_basicSettings"), GUILayout.MaxHeight(0));
            EditorGUILayout.PropertyField(obj.FindProperty("_productParameters"), GUILayout.MaxHeight(10));
            EditorGUILayout.PropertyField(obj.FindProperty("_releaseTypeList"), GUILayout.MaxHeight(10));
            EditorGUILayout.PropertyField(obj.FindProperty("_platformList"), GUILayout.MaxHeight(10));

            BuildSettings.projectConfigurations.Refresh();
            EditorGUILayout.PropertyField(obj.FindProperty("_projectConfigurations"), GUILayout.MaxHeight(10));

            GUILayout.Space(30);


            EditorGUILayout.EndScrollView();

            obj.ApplyModifiedProperties();
        }
    }
}