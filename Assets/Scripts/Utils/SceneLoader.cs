using System;
using UnityEngine.SceneManagement;

[Serializable]
public enum Level
{
    Level1,
    Level2,
    Level3,
    LevelMod
}

public static class SceneLoader 
{
    private const string MAIN_MENU = "MainMenu";
    private const string END_SCREEN = "EndScreen";
    private const string LEVEL_1 = "Level1";
    private const string LEVEL_2 = "Level2";
    private const string LEVEL_3 = "Level3";
    private const string LEVEL_MOD = "ModedWaveLevel";

    public static void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(MAIN_MENU, LoadSceneMode.Single);
    }

    public static void LoadLevel(Level level)
    {
        switch (level)
        {
            case Level.Level1:
                SceneManager.LoadSceneAsync(LEVEL_1, LoadSceneMode.Single);
                break;
            case Level.Level2:
                SceneManager.LoadSceneAsync(LEVEL_2, LoadSceneMode.Single);
                break;
            case Level.Level3:
                SceneManager.LoadSceneAsync(LEVEL_3, LoadSceneMode.Single);
                break;
            case Level.LevelMod:
                SceneManager.LoadSceneAsync(LEVEL_MOD, LoadSceneMode.Single);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(level), level, null);
        }
    }

    public static void LoadEndScreen()
    {
        SceneManager.LoadSceneAsync(END_SCREEN, LoadSceneMode.Additive);
    }
}
