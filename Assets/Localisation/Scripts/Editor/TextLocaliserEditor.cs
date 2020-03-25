using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Localisation
{
    public class TextLocaliserEditWindow : EditorWindow
    {
        private static TextLocaliserEditWindow window;

        public static void Open(string key)
        {
            if (window != null)
            {
                window.Close();
                window = null;
            }

            window = (TextLocaliserEditWindow)CreateInstance(typeof(TextLocaliserEditWindow));
            window.titleContent = new GUIContent("Localiser Window");
            window.ShowUtility();
            window.key = key;

            window.SetCurrentValues();
        }

        void SetCurrentValues()
        {
            values = new string[Enum.GetNames(typeof(Language)).Length];

            for (int i = 0; i < values.Length; i++)
            {
                values[i] = LocalisationManager.GetLocalisedValue(key, (Language)i);
            }
        }

        public string key;
        public string[] values;

        public Vector2 scroll;

        public void OnGUI()
        {
            EditorGUILayout.LabelField(key, KeyGUIStyle());

            scroll = EditorGUILayout.BeginScrollView(scroll);

            string[] languages = Enum.GetNames(typeof(Language));
            for (int i = 0; i < languages.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(languages[i] + ": ", GUILayout.MaxWidth(70));

                EditorStyles.textArea.wordWrap = true;
                values[i] = EditorGUILayout.TextArea(values[i], EditorStyles.textArea, GUILayout.Height(50), GUILayout.Width(400));
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();

            if (GUILayout.Button("Update"))
            {
                for (int i = 0; i < values.Length; i++)
                    if (values[i] == null)
                        values[i] = string.Empty;

                LocalisationManager.Replace(key, values);

                window.Close();
                window = null;
            }

            minSize = new Vector2(480, 250);
            maxSize = minSize;
        }

        private GUIStyle KeyGUIStyle()
        {
            GUIStyle style = new GUIStyle(EditorStyles.label);

            style.fontSize = 15;
            style.alignment = TextAnchor.MiddleCenter;

            return style;
        }
    }

    public class TextLocaliserSearchWindow : EditorWindow
    {
        public string value;
        public Vector2 scroll;
        public Dictionary<string, string> anyDictionary;
        
        public static void Open()
        {
            TextLocaliserSearchWindow searchWindow = (TextLocaliserSearchWindow)CreateInstance(typeof(TextLocaliserSearchWindow));
            searchWindow.titleContent = new GUIContent("Localisation Search");

            Vector2 mouse = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
            Rect rect = new Rect(mouse.x - 450, mouse.y + 10, 10, 10);
            searchWindow.ShowAsDropDown(rect, new Vector2(480, 250));

            searchWindow.value = string.Empty; // show all results
        }
        
        private void OnEnable()
        {
            anyDictionary = LocalisationManager.GetAnyDictionaryForKeys();
        }

        public void OnGUI()
        {
            EditorGUILayout.BeginHorizontal("Box");
            EditorGUILayout.LabelField("Search: ", EditorStyles.boldLabel);
            value = EditorGUILayout.TextField(value);
            EditorGUILayout.EndHorizontal();

            GetSearchResults();
        }

        void GetSearchResults()
        {
            EditorGUILayout.BeginVertical();
            scroll = EditorGUILayout.BeginScrollView(scroll);
            foreach (KeyValuePair<string, string> element in anyDictionary)
            {
                if (element.Key.ToLower().Contains(value.ToLower()))
                {
                    EditorGUILayout.BeginHorizontal("box");
                    EditorGUILayout.LabelField(element.Key);
                    
                    GUIContent editContent = new GUIContent("edit");

                    if (GUILayout.Button(editContent, GUILayout.MaxWidth(40), GUILayout.MaxHeight(20)))
                    {
                        TextLocaliserEditWindow.Open(element.Key);
                    }
                    
                    GUIContent deleteContent = new GUIContent("delete");

                    if (GUILayout.Button(deleteContent, DeleteGUIStyle(), GUILayout.MaxWidth(55), GUILayout.MaxHeight(20)))
                    {
                        if (EditorUtility.DisplayDialog("Removing key", "Are you sure you want to remove key " + element.Key + "?", "Remove", "Cancel"))
                        {
                            LocalisationManager.Remove(element.Key);
                            AssetDatabase.Refresh();
                            LocalisationManager.Init();
                            anyDictionary = LocalisationManager.GetAnyDictionaryForKeys();
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        private GUIStyle DeleteGUIStyle()
        {
            GUIStyle style = new GUIStyle(EditorStyles.textField);

            style.normal.textColor = Color.red;
            style.active.textColor = Color.red;
            style.focused.textColor = Color.red;
            style.hover.textColor = Color.red;
            style.alignment = TextAnchor.MiddleCenter;

            return style;
        }
    }
}
