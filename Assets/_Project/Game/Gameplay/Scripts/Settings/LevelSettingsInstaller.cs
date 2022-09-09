using UnityEngine;
using Zenject;

namespace MiniclipTrick.Game
{
    [CreateAssetMenu(menuName = "Settings/Installer")]
    public class LevelSettingsInstaller : ScriptableObjectInstaller<LevelSettingsInstaller>
    {
        public LevelSettings settings;
        public PiecesSettings pieceSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(settings);
            Container.BindInstance(pieceSettings);
        }
    }
}