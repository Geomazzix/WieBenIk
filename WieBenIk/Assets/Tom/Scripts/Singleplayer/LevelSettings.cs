using UnityEngine;
using WieBenIk.Data;
using WieBenIk.LevelCore;

namespace WieBenIk.Core
{
    public sealed class LevelSettings : MonoBehaviour
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


        private PlayerEntity _Winner;
        public PlayerEntity Winner
        {
            get { return _Winner; }
            set { _Winner = value; }
        }


        //Set the leveldata.
        public void SetLevelData(DatabasePainting[][] paintings, DatabaseQuestion[] questions)
        {
            //Assign the questions.
            _Questions = questions;
            _Paintings = paintings;
        }
    }
}