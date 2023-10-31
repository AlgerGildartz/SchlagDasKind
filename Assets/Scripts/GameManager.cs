using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _emplacementTourPfb;
    [SerializeField] private Transform _emplacementParent;
    public Level CurrentLevel;
    
    public static Action<Transform> SpawnEmplacement;
    
    [HideInInspector] public bool GameWon;

    private bool _gameEnded;
    private void OnEnable()
    {
        MancheManager.GameWon += OnGameWin;
        Base.OnBaseDestruction += OnGameLose;
        SpawnEmplacement += OnSpawnEmplacement;
    }

    private void OnDisable()
    {
        MancheManager.GameWon -= OnGameWin;
        Base.OnBaseDestruction -= OnGameLose;
        SpawnEmplacement -= OnSpawnEmplacement;
    }

    private void Start()
    {
        _gameEnded = false;
    }

    private void OnGameWin()
    {
        if (_gameEnded) return; 
        
        _gameEnded = true;
        GameWon = true;
        SceneLoader.LoadEndScreen();
    }
    
    private void OnGameLose()
    {
        if (_gameEnded) return;
        
        _gameEnded = true;
        GameWon = false;
        SceneLoader.LoadEndScreen();
    }
    
    private void OnSpawnEmplacement(Transform position)
    {
        Instantiate(_emplacementTourPfb, position.position, Quaternion.identity, _emplacementParent);
    }
}
