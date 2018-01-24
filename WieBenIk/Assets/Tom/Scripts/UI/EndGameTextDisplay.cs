using UnityEngine;
using WieBenIk.Core;
using TMPro;

namespace WieBenIk.UI
{
    public class EndGameTextDisplay : MonoBehaviour
    {
        [SerializeField]
        private string _WinText;

        [SerializeField]
        private string _LoseText;


        private void Start()
        {
            if(FindObjectOfType<LevelSettings>().Winner.PlayerID == 1)
            {
                GetComponent<TextMeshProUGUI>().text = _WinText;
            }
            else
            {
                GetComponent<TextMeshProUGUI>().text = _LoseText;
            }
        }
    }
}