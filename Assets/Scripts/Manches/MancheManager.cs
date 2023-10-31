using System;
using System.Collections.Generic;
using UnityEngine;

public class MancheManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] _spawners;
    [SerializeField] private List<Waves> _waves;
    public static event Action GameWon;

    [SerializeField] protected float TimeBeforeNextWave;
    protected float _currentTimeBeforeNextWave;
    protected int _currentWave;
    private bool _countStarted;

    public void Start()
    {
        _currentTimeBeforeNextWave = 0;
    }

    public virtual void FillSpawner() 
    {
        List<GameObject> gos = new List<GameObject>();

        foreach (var enemi in _waves[_currentWave].Enemies)
        {
            for (int i = 0; i < enemi.Numbers; i++)
            {
                gos.Add(enemi.Prefab);
            }
        }

        foreach (EnemySpawner sp in _spawners)
        {
            sp.SpawnEnemies(gos);
        }

        _currentTimeBeforeNextWave = TimeBeforeNextWave;
    }

    public virtual bool AreFilled() 
    {
        foreach (EnemySpawner go in _spawners) 
        {
            if (!go.IsReady) // isready => Empty
                return false;
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsWaveFinished())
        {
            GameWon?.Invoke();
            Destroy(this);
            return;
        }
        
        if(_currentTimeBeforeNextWave <= 0 && AreFilled()) 
        {
            _currentTimeBeforeNextWave = TimeBeforeNextWave;
            _countStarted = false;
            FillSpawner();
            // Add Coins 
        }

        if (AreFilled())
        {
            _currentTimeBeforeNextWave -= Time.deltaTime;
            if (!_countStarted)
            {
                AddGold();
                _currentWave++;
            }

        }
    }
    protected virtual bool IsWaveFinished() 
    {
        return _currentWave >= _waves.Count;
    }

    public void AddGold() 
    {
        if (_countStarted = false || _currentWave >= _waves.Count)
            return;

        Monnaie.MoneySystem.AddMoney((int)_waves[_currentWave].Gold);
        // ADD GOLD
        _countStarted = true;

    }
}
[Serializable]
public struct EnemiesToSpawn 
{
    public GameObject Prefab;
    public float Numbers;
}
[Serializable]
public struct Waves 
{
    public List<EnemiesToSpawn> Enemies;
    public float Gold;
}