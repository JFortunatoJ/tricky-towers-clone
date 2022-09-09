using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MiniclipTrick.Game.Player
{
    public static class PlayerFactory
    {
        public static string PLAYER_PREFAB_ID = "Gameplay/Player";
        
        public static void CreateNewHumanPlayer(string playerId, Transform parent, float horizontalPosition, Action<HumanController> callback)
        {
            Addressables.InstantiateAsync($"{PLAYER_PREFAB_ID}/Human", parent).Completed += handle =>
            {
                if (handle.Status != AsyncOperationStatus.Succeeded)
                {
                    Debug.LogError("An error occured while loading the human player prefab.");
                    return;
                }

                HumanController newPlayer = handle.Result.GetComponent<HumanController>();
                newPlayer.transform.localPosition = new Vector3(horizontalPosition, 0, 0);
                newPlayer.Init(playerId);
                callback?.Invoke(newPlayer);
            };
        }
        
        public static void CreateNewAIPlayer(string playerId, Transform parent, float horizontalPosition, Action<AiController> callback)
        {
            Addressables.InstantiateAsync($"{PLAYER_PREFAB_ID}/AI", parent).Completed += handle =>
            {
                if (handle.Status != AsyncOperationStatus.Succeeded)
                {
                    Debug.LogError("An error occured while loading the human player prefab.");
                    return;
                }

                AiController newPlayer = handle.Result.GetComponent<AiController>();
                newPlayer.transform.localPosition = new Vector3(horizontalPosition, 0, 0);
                newPlayer.Init(playerId);
                callback?.Invoke(newPlayer);
            };
        }
    }
}