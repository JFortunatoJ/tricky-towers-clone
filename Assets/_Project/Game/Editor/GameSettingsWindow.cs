using UnityEditor;
using UnityEngine;

namespace MiniclipTest.Game.Editor
{
    public class GameSettingsWindow : ExtendedEditorWindow
    {
        private static GameSettingsScriptable _settings;

        public static void Show(GameSettingsScriptable settings)
        {
            _settings = settings;
            GameSettingsWindow window = GetWindow<GameSettingsWindow>("Game Settings");

            _serializedObject ??= new SerializedObject(settings);

            window.minSize = new Vector2(400, 300);
        }

        private void OnGUI()
        {
            //Fazer a camera seguir o ponto lá
            if(_serializedObject is null) return;
            
            _currentProperty = _serializedObject.FindProperty("gameSettings");

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(100), GUILayout.ExpandHeight(true));
            
            DrawSideBar(_currentProperty);
            
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));

            if (_selectedProperty is not null)
            {
                EditorGUILayout.LabelField(_selectedProperty.displayName.ToUpper(), new GUIStyle{fontSize = 15, alignment = TextAnchor.MiddleCenter,fontStyle = FontStyle.Bold, normal = new GUIStyleState{textColor = Color.white}});
                EditorGUILayout.Space();
                Draw(_selectedProperty, true);
            }
            else
            {
                EditorGUILayout.LabelField("Select a setting to edit!");
            }
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            
            ApplyChanges();
        }
    }
}