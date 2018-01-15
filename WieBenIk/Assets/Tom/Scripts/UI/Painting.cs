using UnityEngine;
using UnityEngine.UI;


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

        [SerializeField]
        public PaintingCharacteristic[] _Characteristics;
    }
}