#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace MiniclipTrick.Game.Editor
{
    public class LevelEditor : ScriptableWizard
    {
        [MenuItem("Tools/Level Editor")]
        private static void MenuEntryCall()
        {
            DisplayWizard<LevelEditor>("Level Editor", "Save as New");
        }

        private void OnWizardCreate()
        {
            LevelSettingsInstaller newScriptable = ScriptableObject.CreateInstance<LevelSettingsInstaller>();
        }
    }
}
#endif