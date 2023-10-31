using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectSource;

    [Header("Sound")]
    [SerializeField] private AudioClip _victoryMusic;
    [SerializeField] private AudioClip _looseMusic;
    [Space(10)]
    [SerializeField] private AudioClip _musicMenu;
    [SerializeField] private AudioClip _musicGame;
    [Space(10)]
    [SerializeField] private AudioClip _buttonSound;
    [SerializeField] private AudioClip _touchEnemySound;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMenuMusic()
    {
        _musicSource.loop = true;
        _musicSource.clip = _musicMenu;
        _musicSource.Play();
    }

    public void PlayLevelMusic()
    {
        _musicSource.loop = true;
        _musicSource.clip = _musicGame;
        _musicSource.Play();
    }

    public void PlayButtonSound()
    {
        _effectSource.PlayOneShot(_buttonSound);
    }

    public void PlayTouchEnemySound()
    {
        _effectSource.PlayOneShot(_touchEnemySound);
    }

    public void PlayLooseMusic()
    {
        _effectSource.PlayOneShot(_looseMusic);
    }

    public void PlayVictoryMusic()
    {
        _effectSource.PlayOneShot(_victoryMusic);
    }
}
