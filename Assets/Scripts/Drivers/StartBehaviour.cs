using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GeneticAlgorithm.Drivers
{
    public class StartBehaviour : MonoBehaviour
    {
        public void Start()
        {
            StartCoroutine(LoadCoroutine());
        }

        private IEnumerator LoadCoroutine()
        {
            Debug.Log("Started Loading Behaviour");
            yield return SceneManager.LoadSceneAsync(StartUpConstants.UIScene, LoadSceneMode.Additive);
            Debug.Log("Loaded UI Scene");
            yield return SceneManager.LoadSceneAsync(StartUpConstants.MainScene, LoadSceneMode.Additive);
            Debug.Log("Loaded Main Scene");
        }
    }

}