using UnityEditor;
using UnityEditor.Callbacks;

namespace MiniclipTest.Game.Editor
{
    public static class AssetHandler
    {
        [OnOpenAsset]
        public static bool OpenEditor(int instanceId, int line)
        {
            if (EditorUtility.InstanceIDToObject(instanceId) is not GameSettingsScriptable settings) return false;

            GameSettingsWindow.Show(settings);
            return true;
        }
    }
}