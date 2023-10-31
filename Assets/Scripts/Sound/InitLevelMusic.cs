using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitLevelMusic : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlayLevelMusic();
    }
}
