using System.Collections;
using MiniclipTest.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniclipTest.Init
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