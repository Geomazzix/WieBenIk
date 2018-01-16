using UnityEngine;
using UnityEngine.UI;
using WieBenIk.Utility;

namespace WieBenIk.Core
{
    //Represents the core of a painting.
    public class Painting : MonoBehaviour
    {
        [SerializeField]
        private Image _ImageComp;
        public Image ImageComp
        {
            get { return ImageComp; }
        }


        protected bool _ActiveInGame;
        public bool ActiveInGame
        {
            get { return _ActiveInGame; }
            set { _ActiveInGame = value; DisablePainting(); }
        }


        [HideInInspector]
        public PaintingCharacteristic[] _Characteristics;


        //Disable the painting, not the gameobject.
        public void DisablePainting()
        {
            GetComponentInParent<FadeScreen>().StartFadeToInvisible(1.0f, 0.3f);
        }
    }
}