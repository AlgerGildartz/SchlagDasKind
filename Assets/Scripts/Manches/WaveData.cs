using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData 
{
    public List<EnemiesName> wave;
    public int goldEarned;
}

[System.Serializable]
public struct EnemiesName
{
    public string name;
    public float Numbers;
}