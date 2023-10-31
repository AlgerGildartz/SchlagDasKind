using ScriptableObjects.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Game/Wave", fileName = "New Wave List")]
public class WaveList :  GenericVariableSO<List<WaveData>>
{
    public void AddWaveData(WaveData inWaveData)
    {
        if (value == null)
            value = new List<WaveData>();

        value.Add(inWaveData);
    }

    public void AddWaveData(List<WaveData> inWaveData)
    {
        foreach (WaveData wave in inWaveData)
        {
            AddWaveData(wave);
        }
    }
}
