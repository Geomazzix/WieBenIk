using UnityEngine;
using UnityEngine.UI;
using WieBenIk.Utility;

namespace WieBenIk.Core
{
    //Represents the core of a painting.
    [RequireComponent(typeof(Image))]
    public class Painting : MonoBehaviour
    {
        private Sprite _Sprite;
        public Sprite Sprite
        {
            get { return _Sprite; }
            set { _Sprite = value; GetComponent<Image>().sprite = value;}
        }

        protected bool _ActiveInGame;
        public bool ActiveInGame
        {
            get { return _ActiveInGame; }
            set { _ActiveInGame = value; if(!value)DisablePainting(); }
        }


        public PaintingCharacteristic[] _Characteristics;


        //Disable the painting, not the gameobject.
        public void DisablePainting()
        {
            GetComponentInParent<FadeScreen>().StartFadeToInvisible(1.0f, 0.3f);
        }
    }
}