using System;
using UnityEngine;
using Application = UnityEngine.Device.Application;

public class MainMenuManager : MonoBehaviour
{
    [Header("Main")] 
    [SerializeField] private GameObject _mainCanvas;
    
    [Space(10)]
    [Header("Levels")]
    [SerializeField] private GameObject _levelCanvas;
    
    [Space(10)]
    [Header("Mods")]
    [SerializeField] private GameObject _modsCanvas;
    [SerializeField] private ModManager _modManager;

    private MainMenuState _currentState;
    private enum MainMenuState
    {
        Main,
        Level,
        Mod
    }

    private void Start()
    {
        ChangeState(MainMenuState.Main);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (_currentState == MainMenuState.Mod)
            ModsClickRefresh();
    }

    #region Buttons

    #region Main

    public void MainClickPlay()
    {
        ChangeState(MainMenuState.Level);
    }

    public void MainClickMods()
    {
        ChangeState(MainMenuState.Mod);
    }

    public void MainClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    #endregion

    #region Mods

    public void ModsClickRefresh()
    {
        _modManager.RefreshMods();
    }

    public void ModsClickOpenFolder()
    {
        System.Diagnostics.Process.Start(_modManager.ModFolderPath);
    }

    public void ModsClickApply()
    {
        _modManager.ApplyMods();
        ChangeState(MainMenuState.Main);
    }

    public void ModsClickCancel()
    {
        _modManager.CancelSelection();
        _modManager.RefreshMods();
        ChangeState(MainMenuState.Main);
    }

    #endregion

    #region Level

    public void LevelClickBack()
    {
        ChangeState(MainMenuState.Main);
    }

    public void LevelClickPlay(LevelWrapper levelToLoad)
    {
        SceneLoader.LoadLevel(levelToLoad.value);
    }

    #endregion
    #endregion

    #region Utils

    private void ChangeState(MainMenuState state)
    {
        _currentState = state;
        _mainCanvas.SetActive(false);
        _levelCanvas.SetActive(false);
        _modsCanvas.SetActive(false);
        
        switch (state)
        {
            case MainMenuState.Main:
                _mainCanvas.SetActive(true);
                break;
            case MainMenuState.Level:
                _levelCanvas.SetActive(true);
                break;
            case MainMenuState.Mod:
                _modsCanvas.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    #endregion
    
}
