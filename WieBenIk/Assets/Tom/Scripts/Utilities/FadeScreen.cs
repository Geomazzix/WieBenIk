using System.Collections;
using UnityEngine;

/*
TODO: +Create a static class instead of a class that still uses monobehaviour. 
*/

namespace WieBenIk.Utility
{
    public sealed class FadeScreen : MonoBehaviour
    {
        [SerializeField]
        private float _FadeSpeed;


        [SerializeField]
        private CanvasGroup _FadeScreen;
        public CanvasGroup FadeScreenCanvasGroup
        {
            get { return _FadeScreen; }
        }


        //Fade from 0 to full.
        public void StartFadeToVisible(float startvalue, float endvalue)
        {
            _FadeScreen.alpha = startvalue;
            StartCoroutine(IEFade(1, endvalue));
        }


        //Fade from full to 0.
        public void StartFadeToInvisible(float startvalue, float endvalue)
        {
            _FadeScreen.alpha = startvalue;
            StartCoroutine(IEFade(-1, endvalue));
        }


        //Fades the screen by correcting the alphavalue of an overlapping UI image.
        public IEnumerator IEFade(int direction, float endvalue)
        {
            while (true)
            {
                _FadeScreen.alpha += direction * _FadeSpeed * Time.deltaTime;

                if ((direction == 1) && (_FadeScreen.alpha >= endvalue))
                {
                    _FadeScreen.alpha = endvalue;
                    break;
                }
                else if((direction == -1) && (_FadeScreen.alpha <= endvalue))
                {
                    _FadeScreen.alpha = endvalue;
                    break;
                }

                yield return null;
            }
        }
    }
}