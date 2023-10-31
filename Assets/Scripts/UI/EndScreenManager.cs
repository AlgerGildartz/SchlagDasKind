using System;
using UnityEngine;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject _winCanvas;
    [SerializeField] private GameObject _loseCanvas;

    private GameManager _gameManager;
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        Init();
    }

    private void Init()
    {
        if (_gameManager.GameWon)
            InitWin();
        else
            InitLose();
    }

    private void InitWin()
    {
        SoundManager.Instance.PlayVictoryMusic();

        _winCanvas.SetActive(true);
        _loseCanvas.SetActive(false);
    }

    private void InitLose()
    {
        SoundManager.Instance.PlayLooseMusic();

        _winCanvas.SetActive(false);
        _loseCanvas.SetActive(true);
    }

    #region Buttons

    public void ClickNextLevel()
    {
        int res = ((int)_gameManager.CurrentLevel + 1) % (Enum.GetNames(typeof(Level)).Length);
        SceneLoader.LoadLevel((Level)res);
    }

    public void ClickBackToMenu()
    {
        SceneLoader.LoadMainMenu();
    }

    public void ClickRetry()
    {
        SceneLoader.LoadLevel(_gameManager.CurrentLevel);
    }

    #endregion
}
