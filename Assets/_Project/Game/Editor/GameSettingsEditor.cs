using UnityEditor;
using UnityEngine;

namespace MiniclipTest.Game.Editor
{
    [CustomEditor(typeof(GameSettingsScriptable))]
    public class GameSettingsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (!GUILayout.Button("Open Editor")) return;

            GameSettingsWindow.Show((GameSettingsScriptable)target);
        }
    }
}