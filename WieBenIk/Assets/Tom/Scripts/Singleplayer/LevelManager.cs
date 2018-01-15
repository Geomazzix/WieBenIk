using UnityEngine;
using WieBenIk.Utility;
using WieBenIk.Core;
using System.Collections;

namespace WieBenIk.LevelCore
{
    public delegate void ReturnQuestionAnswer();

    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private string _EndgameSceneName;

        [SerializeField]
        private FadeScreen _PaintingVisualsFadeScreen;

        [SerializeField]
        private FadeScreen _QuestionDisplayFadeScreen;

        [SerializeField]
        private LevelPainting[] _LevelPaintings;

        private LevelSettings _LevelSettings;
        private bool[] _PlayerQuestionStatus;

        public event ReturnQuestionAnswer ReturnQuestionAnswerEvent;

        //Initilialize all private members.
        private void Awake()
        {
            _PlayerQuestionStatus = new bool[2];
        }

        //Find all objects.
        private void Start()
        {
            _LevelSettings = FindObjectOfType<LevelSettings>();
        }


        //Fade the background back and the questionselected menu to the front.
        public void FadeQuestionSelectionDisplayIn()
        {
            _QuestionDisplayFadeScreen.gameObject.SetActive(true);
            _QuestionDisplayFadeScreen.StartFadeToVisible(0.0f, 1.0f);
            _PaintingVisualsFadeScreen.StartFadeToInvisible(1.0f, 0.5f);
        }


        //Fade the questionselecteddisplay out and fade the background back in.
        public void FadeQuestionSelectionDisplayOut()
        {
            StartCoroutine(WaitForFadeOut(_QuestionDisplayFadeScreen));
        }

        
        //Waits until the fadeout is completed and then disables the object.
        private IEnumerator WaitForFadeOut(FadeScreen fadescreen)
        {
            _QuestionDisplayFadeScreen.StartFadeToInvisible(1.0f, 0.0f);
            _PaintingVisualsFadeScreen.StartFadeToVisible(0.5f, 1.0f);


            //Not the best way but it works.
            #region Alterative to the build-in WaitUntil() class used in IEnumarators.
            while (fadescreen.FadeScreenCanvasGroup.alpha > 0f)
            {
                yield return null;
            }
            #endregion


            fadescreen.gameObject.SetActive(false);
        }


        //Gets called when one of the player sends an question.
        public void NotifyQuestionSend(int playerID)
        {
            _PlayerQuestionStatus[playerID] = true;
            
            
            //Check if all the players did send their question, if so fire the update event.
            foreach(bool status in _PlayerQuestionStatus)
            {
                if(!status)
                {
                    return; //Someone didnt send his question yet, so dont fire the event.
                }
            }


            //Reset the array.
            int length = _PlayerQuestionStatus.Length;
            for (int i = 0; i < length; i++)
            {
                _PlayerQuestionStatus[i] = false;
            }


            //Everyone send their questions so we can fire the event.
            if (ReturnQuestionAnswerEvent != null)
            {
                ReturnQuestionAnswerEvent.Invoke();
            }
            else
            {
                Debug.LogWarning("ReturnQuestionAnswerEvent has 0 subscribers while getting fired!");
            }


        }


        //Shows all painting, as if they would all be selected for a splitsecond so the user knows that he needs to select a painting.
        public void LightAllPaintings()
        {
            int length = _LevelPaintings.Length;
            for (int i = 0; i < length; i++)
            {
                _LevelPaintings[i].HighlightPainting();
            }
        }


        //One of the players won the game.
        public void EndGame(PlayerEntity victor)
        {
            FindObjectOfType<SceneLoader>().LoadSceneWithFade(_EndgameSceneName);
        }
    }
}