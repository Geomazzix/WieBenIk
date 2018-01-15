using UnityEngine;
using WieBenIk.Data;
using WieBenIk.LevelCore;

namespace WieBenIk.Core
{
    public class LevelSettings : MonoBehaviour
    {
        [SerializeField]
        private int _LevelPaintingCount;
        public int LevelPaintingCount
        {
            get { return _LevelPaintingCount; }
        }

        [HideInInspector]
        public DatabaseQuestion[] _Questions;

        [HideInInspector]
        public DatabasePainting[][] _Paintings;


        //Set the leveldata.
        public void SetLevelData(DatabasePainting[][] paintings, DatabaseQuestion[] questions)
        {
            //Assign the questions.
            _Questions = questions;
            _Paintings = paintings;
        }
    }
}