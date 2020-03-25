using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Localisation
{
    [CustomPropertyDrawer(typeof(LocalisedString))]
    public class LocalisedStringDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            position.width -= 48;
            position.height = 18;

            Rect valueRect = new Rect(position);
            valueRect.x += 15;
            valueRect.width -= 15;

            Rect foldButtonRect = new Rect(position);
            foldButtonRect.width = 15;

            EditorGUI.LabelField(position, "Key", EditorStyles.wordWrappedLabel);

            position.x += 30;
            position.width -= 80;

            SerializedProperty key = property.FindPropertyRelative("key");

            key.stringValue = EditorGUI.TextField(position, key.stringValue, GetGUIStyle(key.stringValue));

            position.x += position.width + 2;
            position.width = 40;
            position.height = 18;
            
            GUIContent editContent = new GUIContent("edit");

            GUI.enabled = key.stringValue != string.Empty;
            if (GUI.Button(position, editContent))
            {
                TextLocaliserEditWindow.Open(key.stringValue);
            }
            GUI.enabled = true;

            position.x += position.width + 2;
            position.width = 54;
            
            GUIContent searchContent = new GUIContent("search");

            if (GUI.Button(position, searchContent))
            {
                TextLocaliserSearchWindow.Open();
            }

            EditorGUI.EndProperty();
        }

        private GUIStyle GetGUIStyle(string key)
        {
            GUIStyle style = new GUIStyle(EditorStyles.textField);

            Dictionary<string, string> anyDictionary = LocalisationManager.GetAnyDictionaryForKeys();
            foreach (KeyValuePair<string, string> element in anyDictionary)
            {
                if (string.Equals(element.Key, key))
                {
                    return style; // return default
                }
            }

            style = new GUIStyle(EditorStyles.textField);
            style.normal.textColor = Color.gray;
            style.active.textColor = Color.gray;
            style.focused.textColor = Color.gray;
            style.hover.textColor = Color.gray;

            return style;
        }
    }
}
