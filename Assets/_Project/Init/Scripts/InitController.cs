using System.Collections;
using MiniclipTrick.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniclipTrick.Init
{
    public class InitController : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
            SceneController.LoadScene("StartMenu", LoadSceneMode.Single);
        }
    }
}