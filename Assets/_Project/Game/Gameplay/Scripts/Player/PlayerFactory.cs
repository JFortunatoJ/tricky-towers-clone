using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MiniclipTest.Game.Player
{
    public static class PlayerFactory<T> where T : PlayerController
    {
        public static string PLAYER_PREFAB_ID = "Gameplay/Player";
        
        public static void CreateNewPlayer(string playerId, Transform parent, float horizontalPosition, Action<T> callback)
        {
            InstantiatePlayer("Human", playerId, parent, horizontalPosition, callback);
        }
        
        public static void CreateNewAIPlayer(string playerId, Transform parent, float horizontalPosition, Action<T> callback)
        {
            InstantiatePlayer("AI", playerId, parent, horizontalPosition, callback);
        }

        private static void InstantiatePlayer(string assetReferenceId, string playerId, Transform parent, float horizontalPosition, Action<T> callback)
        {
            Addressables.InstantiateAsync($"{PLAYER_PREFAB_ID}/{assetReferenceId}", parent).Completed += handle =>
            {
                if (handle.Status != AsyncOperationStatus.Succeeded)
                {
                    Debug.LogError("An error occured while loading the player prefab.");
                    return;
                }

                T newPlayer = handle.Result.GetComponent<T>();
                newPlayer.transform.localPosition = new Vector3(horizontalPosition, 0, 0);
                newPlayer.Init(playerId);
                callback?.Invoke(newPlayer);
            };
        }
    }
}