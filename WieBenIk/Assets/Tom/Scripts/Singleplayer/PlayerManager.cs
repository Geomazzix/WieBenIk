using UnityEngine;
using UnityEngine.UI;
using WieBenIk.Core;
using TMPro;
using System.Linq;

namespace WieBenIk.LevelCore
{
    public class PlayerManager : PlayerEntity
    {
        [SerializeField]
        private TextMeshProUGUI _QuestionTextComp; 

        [SerializeField]
        private Button _GuessButton;

        [SerializeField]
        private Button _QuestionButton;

        [SerializeField]
        private QuestionAnswerDisplay _AnswerDisplay;


        //Set subscribtions.
        private new void Start()
        {
            base.Start();

            Invoke("AssignPaintingID", 0.1f);


            _LevelManager.ReturnQuestionAnswerEvent += ResetPlayerActions;
            _LevelManager.ReturnQuestionAnswerEvent += UpdateGame;
        }


        //Sends a question to the other playerentity in the level and sets the data for updating the board.
        public override void SendQuestionToPlayerEntity(DatabaseQuestion question)
        {
            _QuestionTextComp.text = question.QuestionText;
            _LastQuestion = question;
            _QuestionAnswer = FindObjectOfType<AIManager>().AnswerQuestion(question);
            _LevelManager.NotifyQuestionSend(_PlayerID - 1);

            //Make sure that when the player has taken an action he can't do anymore actions.
            _GuessButton.interactable = false;
            _QuestionButton.interactable = false;
        }


        //Enables the buttons so the player can take action again.
        public void ResetPlayerActions()
        {
            _GuessButton.interactable = true;
            _QuestionButton.interactable = true;
        }


        //Guesses the other players painting.
        public override void GuessTheOtherPlayerPainting(Painting otherPaintingGuess)
        {
            if(otherPaintingGuess == FindObjectOfType<AIManager>().PaintingID)
            {
                _LevelManager.EndGame(this);
            }
            else
            {
                _LevelManager.EndGame(FindObjectOfType<AIManager>());
            }
        }


        //Sets the player identity.
        protected override void AssignPaintingID()
        {
            AIManager AIManager = FindObjectOfType<AIManager>();
            int index = Random.Range(0, AIManager._ImportedPaintings.Length);
            _PaintingID.Sprite = AIManager._ImportedPaintings[index].PaintingSprite;
            _PaintingID._Characteristics = AIManager._ImportedPaintings[index]._PaintingCharacteristics.ToArray();
        }


        //Makes sure
        public override void UpdateGame()
        {
            base.UpdateGame();
            _AnswerDisplay.DisplayQuestionAnswer(_QuestionAnswer);
            ResetPlayerActions();   //Make sure the player can do all his actions again.
        }


        //Make sure to unsubscribe the subscribtions.
        public void OnDestroy()
        {
            _LevelManager.ReturnQuestionAnswerEvent -= UpdateGame;
            _LevelManager.ReturnQuestionAnswerEvent -= ResetPlayerActions;
        }
    }
}