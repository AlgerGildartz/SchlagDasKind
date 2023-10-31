using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMenuMusic : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlayMenuMusic();
    }

}
