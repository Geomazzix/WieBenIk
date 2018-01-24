using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using WieBenIk.Utility;

namespace WieBenIk.Core
{
    public sealed class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        private FadeScreen _FadeScreen;

        //Makes sure to call the fadescreen class and loads in a new scene.
        public void LoadSceneWithFade(string sceneName)
        {
            StartCoroutine(IELoadSceneWithFade(sceneName));
        }


        //Loads the scene with the fadein and fadeout.
        private IEnumerator IELoadSceneWithFade(string sceneName)
        {
            //Fade out.
            _FadeScreen.StartFadeToVisible(0.0f, 1.0f);
            yield return new WaitUntil(() => _FadeScreen.FadeScreenCanvasGroup.alpha >= 1f);

            //Load scene
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

            //Fade back in.
            _FadeScreen.StartFadeToInvisible(1.0f, 0.0f);
            yield return new WaitUntil(() => _FadeScreen.FadeScreenCanvasGroup.alpha <= 0f);
        }
    }
}