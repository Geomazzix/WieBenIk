using UnityEngine;
using System.Collections.Generic;
using WieBenIk.LevelCore;
using WieBenIk.Core;

namespace WieBenIk.UI
{
    public sealed class QuestionDisplayController : MonoBehaviour
    {
        [SerializeField]
        private QuestionContent _QuestionPrefab;

        [SerializeField]
        private Transform _QuestionPrefabParent;

        [SerializeField]
        private PlayerManager _PlayerManager;

        private DatabaseQuestion _SelectedQuestion;
        public DatabaseQuestion SelectedQuestion
        {
            get { return _SelectedQuestion; }
            set { _SelectedQuestion = value; }
        }

        private List<QuestionContent> _QuestionButtons;


        //Load all questions in.
        public void Start()
        {
            _QuestionButtons = new List<QuestionContent>();
            LevelSettings levelSettings = FindObjectOfType<LevelSettings>();
            int length = levelSettings._Questions.Length;
            for (int i = 0; i < length; i++)
            {
                QuestionContent currQuestion = Instantiate(_QuestionPrefab, _QuestionPrefabParent);
                currQuestion.Question = levelSettings._Questions[i];
                _QuestionButtons.Add(currQuestion);
            }
        }


        //Sends the selected question to the player.
        public void SendSelectedQuestionToPlayer()
        {
            _PlayerManager.SendQuestionToPlayerEntity(_SelectedQuestion);
        }
    }
}