using UnityEngine;
using UnityEngine.UI;
using WieBenIk.Data;
using WieBenIk.UI;
using System.Collections.Generic;

namespace WieBenIk.Core
{
    public class LevelSettingsSelecter : MonoBehaviour
    {
        [Tooltip("Keep this on 2 for singleplayer for now.")]
        [SerializeField]
        private int _PlayerCount = 2;
        public int PlayerCount
        {
            get { return _PlayerCount; }
            set { _PlayerCount = value; }
        }

        [SerializeField]
        private string _PaintingsDatapath;

        [SerializeField]
        private string _QuestionsDatapath;


        [SerializeField]
        private DatabasePaintings _PaintingsDatabase;

        [SerializeField]
        private DatabaseQuestions _QuestionsDatabase;

        [SerializeField]
        private Toggle[] _ArtDirections;


        //Sends the artdirections to the LevelSettingsScript which can be found in the gamemanager.
        public void SetLevelDataAndLoadLevel(string sceneName)
        {
            //Import all the paintings and questions.
            DatabasePainting[] _AllPaintings = Resources.LoadAll<DatabasePainting>(_PaintingsDatapath);
            DatabaseQuestion[] _AllQuestions = Resources.LoadAll<DatabaseQuestion>(_QuestionsDatapath);

            LevelSettings levelSettings = FindObjectOfType<LevelSettings>();

            List<DatabasePainting> databasePaintings = new List<DatabasePainting>();
            DatabasePainting[][] levelPaintings = new DatabasePainting[_PlayerCount][];


            //Select all the data for the paintings.
            foreach (Toggle artdirection in _ArtDirections)
            {
                if(artdirection.isOn)
                {
                    ArtDirectionToggleButton artdirectionToggle = artdirection.GetComponent<ArtDirectionToggleButton>();

                    int length = _AllPaintings.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if(_AllPaintings[i].ArtDirection == artdirectionToggle.ArtDirection)
                        {
                            databasePaintings.Add(_AllPaintings[i]);
                        }
                    }
                }
            }


            //Shuffle the paintings before pulling the count of paintings out of them so the program won't pick the same paintings to randomize.
            //Make sure to do this for each player. (including AI).
            int count = databasePaintings.Count;
            for (int x = 0; x < _PlayerCount; x++)
            {
                //Shuffle.
                for (int y = 0; y < count; y++)
                {
                    DatabasePainting tmp = databasePaintings[y];
                    int r = Random.Range(y, count);
                    databasePaintings[y] = databasePaintings[r];
                    databasePaintings[r] = tmp;
                }

                //Initialize the array.
                levelPaintings[x] = new DatabasePainting[levelSettings.LevelPaintingCount];

                //Assign the paintings.
                for (int z = 0; z < levelSettings.LevelPaintingCount; z++)
                {
                    levelPaintings[x][z] = databasePaintings[z];
                }
            }

            //Set the data for the level.
            levelSettings.SetLevelData(levelPaintings, _AllQuestions);

            //Load the next scene in.
            FindObjectOfType<SceneLoader>().LoadSceneWithFade(sceneName);
        }
    }
}