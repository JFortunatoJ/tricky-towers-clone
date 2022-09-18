using UnityEditor;
using UnityEngine;

namespace MiniclipTest.Game.Editor
{
    public class ExtendedEditorWindow : EditorWindow
    {
        protected static SerializedObject _serializedObject;
        protected SerializedProperty _currentProperty;

        protected string _selectedPropertyPath;
        protected SerializedProperty _selectedProperty;

        protected void Draw(SerializedProperty property, bool drawChildren)
        {
            string lastPropPath = string.Empty;
            foreach (SerializedProperty prop in property)
            {
                if (prop.isArray && prop.propertyType.Equals(SerializedPropertyType.Generic))
                {
                    EditorGUILayout.BeginHorizontal();
                    prop.isExpanded = EditorGUILayout.Foldout(prop.isExpanded, prop.displayName);
                    EditorGUILayout.EndHorizontal();

                    if (prop.isExpanded)
                    {
                        EditorGUI.indentLevel++;
                        Draw(prop, drawChildren);
                        EditorGUI.indentLevel--;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(lastPropPath) && prop.propertyPath.Contains(lastPropPath))
                    {
                        continue;
                    }

                    lastPropPath = prop.propertyPath;
                    EditorGUILayout.PropertyField(prop, drawChildren);
                }
            }
        }

        protected void DrawSideBar(SerializedProperty property)
        {
            foreach (SerializedProperty prop in property)
            {
                if(prop.propertyType != SerializedPropertyType.Generic) continue;
                
                if (GUILayout.Button(prop.displayName))
                {
                    _selectedPropertyPath = prop.propertyPath;
                }
            }

            if (!string.IsNullOrEmpty(_selectedPropertyPath))
            {
                _selectedProperty = _serializedObject.FindProperty(_selectedPropertyPath);
            }
        }

        protected void ApplyChanges()
        {
            _serializedObject.ApplyModifiedProperties();
        }
    }
}