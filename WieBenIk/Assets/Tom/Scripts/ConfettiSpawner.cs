using UnityEngine;

public sealed class ConfettiSpawner : MonoBehaviour 
{
    [SerializeField]
    private GameObject _WinnerParticles;

    [SerializeField]
    private GameObject _LoserParticles;

    public void SpawnWinnerParticles(bool playerWon)
    {
        if(playerWon)
        {
            Instantiate(_WinnerParticles, transform);
        }
        else
        {
            Instantiate(_LoserParticles, transform);
        }
    }
}
