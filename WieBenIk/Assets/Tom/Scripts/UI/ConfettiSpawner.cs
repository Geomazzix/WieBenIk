using UnityEngine;
using WieBenIk.Core;

namespace WieBenIk.UI
{
    public sealed class ConfettiSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _WinnerParticles;

        [SerializeField]
        private GameObject _LoserParticles;

        private void Start()
        {
            LevelSettings levelsettings = FindObjectOfType<LevelSettings>();
            if(levelsettings.Winner.PlayerID == 1)
            {
                Instantiate(_WinnerParticles, transform);
            }
            else
            {
                Instantiate(_LoserParticles, transform);
            }
        }
    }
}