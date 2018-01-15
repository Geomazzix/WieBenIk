using UnityEngine;
using UnityEngine.UI;
using WieBenIk.Core;
using TMPro;

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
            _LevelManager.ReturnQuestionAnswerEvent += ResetPlayerActions;
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
            
        }


        //Makes sure
        public override void UpdateGame()
        {
            base.UpdateGame();
            _AnswerDisplay.DisplayQuestionAnswer(_QuestionAnswer);
            ResetPlayerActions();   //Make sure the player can do all his actions again.
        }


        //Make sure to unsubscribe the subscribtions.
        public new void OnDestroy()
        {
            _LevelManager.ReturnQuestionAnswerEvent -= ResetPlayerActions;
        }
    }
}