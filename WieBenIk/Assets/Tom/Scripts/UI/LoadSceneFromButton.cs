using UnityEngine;
using UnityEngine.UI;
using WieBenIk.Core;

namespace WieBenIk.UI
{
    public class LoadSceneFromButton : MonoBehaviour
    {
        [SerializeField]
        private string _SceneName;

        private Button _Button;


        //Assign the 'loadscene' function to the button.
        private void Awake()
        {
            _Button = GetComponent<Button>();
            _Button.onClick.AddListener(LoadScene);
        }


        //Call the sceneloader to load a new scene.
        public void LoadScene()
        {
            FindObjectOfType<SceneLoader>().LoadSceneWithFade(_SceneName);
        }


        //Unsubscribe all listeners from the button so there won't be any memory leaks.
        public void OnDestroy()
        {
            _Button.onClick.RemoveAllListeners();
        }
    }
}