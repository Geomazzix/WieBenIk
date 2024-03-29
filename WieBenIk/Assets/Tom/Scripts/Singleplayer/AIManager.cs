﻿using UnityEngine;
using WieBenIk.Core;
using System.Collections.Generic;

namespace WieBenIk.LevelCore
{
    public sealed class AIManager : PlayerEntity
    {
        private List<DatabaseQuestion> _Questions;

        //Start the first turn of the AI.
        private new void Start()
        {
            base.Start();

            _Questions = new List<DatabaseQuestion>(_LevelSettings._Questions);
            Invoke("AssignPaintingID", 0.1f);
            

            _LevelManager.ReturnQuestionAnswerEvent += UpdateGame;
        }


        //Sends a question to the other playerentity in the level.
        public override void SendQuestionToPlayerEntity(DatabaseQuestion question)
        {
            _QuestionAnswer = FindObjectOfType<PlayerManager>().AnswerQuestion(question);
            FindObjectOfType<LevelManager>().NotifyQuestionSend(_PlayerID - 1);
            _LastQuestion = question;
        }


        //Guesses the other players painting.
        public override void GuessTheOtherPlayerPainting(Painting otherPaintingGuess)
        {
            print("Check other painting!!");
            print("Other paintingsprite: " + otherPaintingGuess.Sprite.name);
            print("Paintingsprite: " + FindObjectOfType<PlayerManager>().PaintingID.Sprite);

            if (otherPaintingGuess.Sprite == FindObjectOfType<PlayerManager>().PaintingID.Sprite)
            {
                print("Endgame");
                _LevelManager.EndGame(this);
            }
        }


        //When the AI gets can answer, he will update his playboard immidiately and than start his new turn immidiately after.
        public override void UpdateGame()
        {
            base.UpdateGame();  //Updates board.

            //Check before sending a new question if the AI can guess.
            Painting LastPainting = null;
            int deactivatedCount = 0;
            int length = _LevelPaintings.Length;
            for (int i = 0; i < length; i++)
            {
                if(!_LevelPaintings[i].ActiveInGame)
                {
                    deactivatedCount++;
                }
                else
                {
                    LastPainting = _LevelPaintings[i];//This is not efficient, but once there is only one remaining this will find it.
                }
            }

            //Look if there is only 1 card remaining on the board, if so guess the other painting (which will have a 100% guess chance), else keep guessing.
            if(deactivatedCount == _LevelSettings.LevelPaintingCount)
            {
                Debug.LogError("None paintingproperties have been set in the database folder!");
            }
            else if (deactivatedCount >= _LevelSettings.LevelPaintingCount - 1)
            {
                GuessTheOtherPlayerPainting(LastPainting);
            }
            else
            {
                //Sends new question instantly.
                int chosenQuestionIndex = Random.Range(0, _Questions.Count);
                if(chosenQuestionIndex <= _Questions.Count) //Put check in here, because it kept giving null references.
                {
                    SendQuestionToPlayerEntity(_Questions[chosenQuestionIndex]);
                    _Questions.RemoveAt(chosenQuestionIndex);
                }
            }
        }


        //Sets the player identity.
        protected override void AssignPaintingID()
        {
            PlayerManager PlayerManager = FindObjectOfType<PlayerManager>();
            int index = Random.Range(0, PlayerManager._ImportedPaintings.Length);
            _PaintingID.Sprite = PlayerManager._ImportedPaintings[index].PaintingSprite;
            _PaintingID._Characteristics = PlayerManager._ImportedPaintings[index]._PaintingCharacteristics.ToArray();

            //Sends the first question.
            int chosesQuestionIndex = Random.Range(0, _Questions.Count - 1);
            SendQuestionToPlayerEntity(_Questions[chosesQuestionIndex]);
            _Questions.RemoveAt(chosesQuestionIndex);
        }


        //When this instance gets destroyed handle the subscribtions.
        public void OnDestroy()
        {
            _LevelManager.ReturnQuestionAnswerEvent -= UpdateGame;
        }
    }
}