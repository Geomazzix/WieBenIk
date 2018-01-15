using UnityEngine;
using WieBenIk.LevelCore;

namespace WieBenIk.Data
{
    //Holds all the paintings for each kunststroming.
    [System.Serializable]
    public struct GeneralArtDirection
    {
        public EArtDirections GeneralArtDirectionName;
        //public Painting[] ArtDirections;
    }

    //Holds an array which contains the kunstromingen.
    public class DatabasePaintings : MonoBehaviour
    {
        [Tooltip("Make sure that the ArtDirections are numbered:\n 1 = oudestijl\n 2 = oudestijlinnieuwjasje")]
        [SerializeField]
        public GeneralArtDirection[] _ArtDirectionPaintings;


        //Resets all the data in the database.
        public void EmptyDatabase()
        {
            _ArtDirectionPaintings = new GeneralArtDirection[0];
        }
    }
}