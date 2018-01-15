using UnityEngine;
using UnityEngine.UI;
using WieBenIk.Core;
using WieBenIk.UI;
using System.Collections;

namespace WieBenIk.LevelCore
{
    public class LevelPainting : Painting
    {
        private Toggle _HighlightToggle;
        private ToggleGroup _SelectedToggleGroup;
        private bool _HighlightRunning;


        //Can be awake because it only requires one component.
        private void Awake()
        {
            _HighlightToggle = GetComponent<Toggle>();
            _HighlightToggle.onValueChanged.AddListener((value) => { SetActivePainting(value); });
        }


        //Highlight the selectedPainting.
        public void HighlightPainting()
        {
            if(_ActiveInGame)
            {
                _SelectedToggleGroup = _HighlightToggle.group;
                _HighlightToggle.group = null;

                StartCoroutine(HighlightPaintings(2, 0.25f));
            }
        }


        //Highlights the paintings.
        private IEnumerator HighlightPaintings(int numBlinks, float intervalInSeconds)
        {
            _HighlightRunning = true;
            for (int i = 0; i < numBlinks; i++)
            {
                //toggle on.
                _HighlightToggle.isOn = true;
                yield return new WaitForSeconds(intervalInSeconds);

                //toggle off.
                _HighlightToggle.isOn = false;
                yield return new WaitForSeconds(intervalInSeconds);
            }

            _HighlightToggle.group = _SelectedToggleGroup;
            _HighlightRunning = false;
        }


        //Check if the highlight is running, if not select the toggle to be active.
        public void SetActivePainting(bool value)
        {
            if ((!_HighlightRunning) && (_ActiveInGame))
            {
                FindObjectOfType<GuessOtherPaintingButton>().SetActiveToggle(_HighlightToggle);
            }
        }


        //When a new scene loads in handle the delegates.
        public void OnDestroy()
        {
            _HighlightToggle.onValueChanged.RemoveAllListeners();
        }
    }
}