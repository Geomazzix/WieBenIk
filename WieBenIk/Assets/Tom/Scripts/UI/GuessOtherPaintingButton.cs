using UnityEngine;
using UnityEngine.UI;
using WieBenIk.LevelCore;

namespace WieBenIk.UI
{
    public sealed  class GuessOtherPaintingButton : MonoBehaviour
    {
        [SerializeField]
        private PlayerManager _PlayerManager;

        [SerializeField]
        private AIManager _AIManager;

        private Toggle _SelectedToggle;

        [SerializeField]
        private LevelManager _LevelManager;


        //Guess the other player's painting.
        public void GuessOtherPainting()
        {
            if(_SelectedToggle != null)
            {
                _PlayerManager.GuessTheOtherPlayerPainting(_SelectedToggle.gameObject.GetComponent<LevelPainting>());
            }
            else
            {
                _LevelManager.LightAllPaintings();
            }
        }

        
        //Assigns the active toggle of the paintings.
        public void SetActiveToggle(Toggle toggle)
        {
            _SelectedToggle = toggle;
        }
    }
}