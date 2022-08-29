using Zenject;

namespace MiniclipTrick.Game
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInstance(FindObjectOfType<PiecesCamera>());
        }
    }
}