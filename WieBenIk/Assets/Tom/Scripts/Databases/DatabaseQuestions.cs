using UnityEngine;
using WieBenIk.LevelCore;


namespace WieBenIk.Data
{
    //Holds all the questions in the game.
    public class DatabaseQuestions : MonoBehaviour
    {
        [SerializeField]
        public Question[] _Questions;

        //Resets all the data in the database.
        public void EmptyDatabase()
        {
            _Questions = new Question[0];
        }
    }


    [System.Serializable]
    public struct Question
    {
        public string QuestionText;
        public EPaintingProperty QuestionIsAbout;
    }
}