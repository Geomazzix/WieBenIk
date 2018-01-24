using UnityEngine;

using WieBenIk.Core;
using UnityEngine.UI;

namespace WieBenIk.UI
{
    public sealed class MedalDisplay : MonoBehaviour
    {
        [SerializeField]
        private Sprite _GoldMedal;

        [SerializeField]
        private Sprite _SilverMedal;


        private void Start()
        {
            LevelSettings levelsettings = FindObjectOfType<LevelSettings>();
            if (levelsettings.Winner.PlayerID == 1)
            {
                GetComponent<Image>().sprite = _GoldMedal;
            }
            else
            {
                GetComponent<Image>().sprite = _SilverMedal;
            }
        }
    }
}