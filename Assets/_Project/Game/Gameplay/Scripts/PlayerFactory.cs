using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MiniclipTrick.Game.Player
{
    public static class PlayerFactory
    {
        public static string PLAYER_PREFAB_ID = "Gameplay/Player";
        
        public static void CreateNewHumanPlayer(string playerId, Transform parent, Action<HumanController> callback)
        {
            Addressables.InstantiateAsync($"{PLAYER_PREFAB_ID}/Human", parent).Completed += handle =>
            {
                if (handle.Status != AsyncOperationStatus.Succeeded)
                {
                    Debug.LogError("An error occured while loading the human player prefab.");
                    return;
                }

                HumanController newPlayer = handle.Result.GetComponent<HumanController>();
                newPlayer.Init(playerId, 5);
                callback?.Invoke(newPlayer);
            };
        }
    }
}