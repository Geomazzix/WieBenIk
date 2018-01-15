﻿using UnityEngine;
using UnityEngine.UI;
using WieBenIk.Core;
using WieBenIk.Utility;

namespace WieBenIk.LevelCore
{
    public abstract class PlayerEntity : MonoBehaviour
    {
        [SerializeField]
        protected int _PlayerID;

        [SerializeField]
        protected LevelManager _LevelManager;

        [SerializeField]
        protected LevelPainting[] _LevelPaintings;

        [SerializeField]
        protected Painting _PaintingID;
        public Painting PaintingID
        {
            get { return _PaintingID; }
        }

        protected DatabasePainting[] _ImportedPaintings;
        protected LevelSettings _LevelSettings;
        protected bool _QuestionAnswer;
        protected DatabaseQuestion _LastQuestion;


        //Assigns random sprites to the empty paintings.
        protected void Start()
        {
            _LevelSettings = FindObjectOfType<LevelSettings>();

            int length = _LevelSettings.LevelPaintingCount;
            _ImportedPaintings = new DatabasePainting[length];
            for (int i = 0; i < length; i++)
            {
                _ImportedPaintings[i] = _LevelSettings._Paintings[_PlayerID - 1][i];
            }


            //Check if the images did load in.
            if (_ImportedPaintings.Length > 0)
            {
                AssignPaintingSprites();
                AssignPaintingID();
            }
            else
            {
                Debug.LogWarning("No paintings were loaded in, in: " + name + "!");
            }

            //Check if the questions did load in.
            if(_LevelSettings._Questions.Length <= 0)
            {
                Debug.LogWarning("No questions were loaded in, in: " + name + "!");
            }


            //Handle the subscribtion.
            _LevelManager.ReturnQuestionAnswerEvent += UpdateGame;
        }


        //Assign all the sprites to the paintings.
        private void AssignPaintingSprites()
        {
            int length = _LevelPaintings.Length;
            int propertiesLength = _ImportedPaintings[0]._PaintingCharacteristics.Length;
            for (int x = 0; x < length; x++)
            {
                _LevelPaintings[x].ImageComp.sprite = _ImportedPaintings[x].PaintingSprite;

                //Set the painting properties.
                for (int y = 0; y < propertiesLength; y++)
                {
                    if (_ImportedPaintings[x]._PaintingCharacteristics[y]._DoesContain)
                    {
                        //This will never get an index is out of bounce exception because the class sets its properties with a Reset void upon adding the component to an object.
                        _LevelPaintings[x]._Characteristics[y]._DoesContain = true;
                    }
                }
            }
        }


        //Gets called when there is a question to this player entity.
        public bool AnswerQuestion(DatabaseQuestion question)
        {
            //Loop through the paintingID properties, when the program finds the property the question is about the program finds if it has it or not and returns that value.
            int paintingPropertiesLength = _PaintingID._Characteristics.Length;
            for (int i = 0; i < paintingPropertiesLength; i++)
            {
                if(_PaintingID._Characteristics[i]._PaintingProperty == question.QuestionIsAbout)
                {
                    if(_PaintingID._Characteristics[i]._DoesContain)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        //Change the paintings if the property is none of them.
        private void ChangePaintingsBoard(PaintingProperty paintingProperty, bool answerOfQuestion)
        {
            int length = _LevelPaintings.Length;
            int propertiesLength = _LevelPaintings[0]._Characteristics.Length;
            for (int x = 0; x < length; x++)
            {
                for (int y = 0; y < propertiesLength; y++)
                {
                    if (_LevelPaintings[x]._Characteristics[y]._PaintingProperty == paintingProperty)
                    {
                        if (!_LevelPaintings[x]._Characteristics[y]._DoesContain)
                        {
                            _LevelPaintings[x].gameObject.GetComponentInParent<FadeScreen>().StartFadeToInvisible(1.0f, 0.3f);
                            _LevelPaintings[x].GetComponent<Toggle>().isOn = false;
                            _LevelPaintings[x].Active = false;
                        }
                    }
                }
            }
        }


        //Gets called when all the question have been received by the levelmanager.
        public virtual void UpdateGame()
        {
            //Update the board.
            ChangePaintingsBoard(_LastQuestion.QuestionIsAbout, _QuestionAnswer);
        }


        //Handle subscribtions.
        public void OnDestroy()
        {
            _LevelManager.ReturnQuestionAnswerEvent -= UpdateGame;
        }


        //Abstract functions.
        public abstract void SendQuestionToPlayerEntity(DatabaseQuestion question);
        public abstract void GuessTheOtherPlayerPainting(LevelPainting otherPaintingGuess);
        protected abstract void AssignPaintingID();
    }
}