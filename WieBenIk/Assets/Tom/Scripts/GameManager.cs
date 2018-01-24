using UnityEngine;

namespace WieBenIk.Core
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField]
        private string _FirstSceneName;

        private SceneLoader _SceneLoader;

        //Load the first scene.
        private void Awake()
        {
            _SceneLoader = GetComponent<SceneLoader>();
            _SceneLoader.LoadSceneWithFade(_FirstSceneName);
        }
    }
}