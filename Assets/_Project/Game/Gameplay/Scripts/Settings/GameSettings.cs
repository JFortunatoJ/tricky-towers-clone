using MiniclipTest.Utility;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MiniclipTest.Game
{
    public class GameSettings : PersistentSingleton<GameSettings>
    {
        protected override void SetupInstance()
        {
            Addressables.LoadAssetAsync<GameSettingsScriptable>("GameSettings").Completed += handle =>
            {
                if (handle.Status != AsyncOperationStatus.Succeeded)
                {
                    Debug.LogError("The Global Settings asset could not be loaded.");
                    return;
                }

                _settings = handle.Result;
            };
        }

        public float GravityMultiplier => _settings.gameSettings.pieceSettings.gravityMultiplier;
        public float PieceHorizontalStep => _settings.gameSettings.pieceSettings.horizontalStep;
        public float PieceNormalDescendSpeed => _settings.gameSettings.pieceSettings.normalDescendSpeed;
        public float PieceBoostDescendSpeed => _settings.gameSettings.pieceSettings.boostDescendSpeed;

        public int FinishLineHeight => _settings.gameSettings.levelSettings.finishLineHeight;
        public int PiecesLostToGameOver => _settings.gameSettings.levelSettings.piecesLostToGameOver;

        private GameSettingsScriptable _settings;
    }
}