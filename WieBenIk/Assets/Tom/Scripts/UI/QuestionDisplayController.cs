using UnityEngine;
using WieBenIk.Data;
using WieBenIk.LevelCore;
using WieBenIk.Core;

namespace WieBenIk.UI
{
    public class QuestionDisplayController : MonoBehaviour
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


        //Load all questions in.
        public void Start()
        {
            LevelSettings levelSettings = FindObjectOfType<LevelSettings>();
            int length = levelSettings._Questions.Length;
            if(length != 0)
            {
                for (int i = 0; i < length; i++)
                {
                    Instantiate(_QuestionPrefab, _QuestionPrefabParent);
                    _QuestionPrefab.Question = levelSettings._Questions[i];
                }
            }
            else
            {
                Debug.LogWarning("No questions in Levelsettings!");
            }
        }


        //Sends the selected question to the player.
        public void SendSelectedQuestionToPlayer()
        {
            _PlayerManager.SendQuestionToPlayerEntity(_SelectedQuestion);
        }
    }
}