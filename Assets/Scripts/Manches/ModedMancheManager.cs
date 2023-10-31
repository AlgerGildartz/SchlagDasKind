using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModedMancheManager : MancheManager
{
    [SerializeField] private WaveList waveList;
    [SerializeField] private EnemyList enemyList;
    [SerializeField] private List<ModdedEnemieSpawner> _spawnerModed;
    // Start is called before the first frame update
    public override void FillSpawner() 
    {
        if(waveList.value == null || waveList.value.Count == 0 
            || enemyList.value == null || enemyList.value.Count == 0)
        {
            base.FillSpawner();
        }
        List<EnemyData> gos = new List<EnemyData>();

        foreach (var enemi in waveList.value[_currentWave].wave)
        {
            EnemyData toAdd = FindEnemyData(enemi.name);
            for (int i = 0; i < enemi.Numbers; i++)
            {
                gos.Add(toAdd);
            }
        }

        foreach (ModdedEnemieSpawner sp in _spawnerModed)
        {
            sp.SpawnEnemies(gos);
        }

        _currentTimeBeforeNextWave = TimeBeforeNextWave;
    }

    private EnemyData FindEnemyData(string name) 
    {
        foreach(EnemyData data in enemyList.value) 
        {
            if (name.Equals(data.nameReference))
                return data;
        }
        return enemyList.value[0];
    }

    public override bool AreFilled()
    {
        if (waveList.value == null || waveList.value.Count == 0
               || enemyList.value == null || enemyList.value.Count == 0)
        {
            return base.AreFilled();
        }
        foreach (ModdedEnemieSpawner go in _spawnerModed)
        {
            if (go.IsReady)
                return true;
        }
        return false;
    }


    protected override bool IsWaveFinished()
    {
        return _currentWave >= waveList.value.Count;
    }
}
