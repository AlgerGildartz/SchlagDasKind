using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModdedEnemieSpawner : MonoBehaviour
{
    [SerializeField] private PathCreator _pathCreator;

    private List<EnemyData> _enemiesToSpawn = new List<EnemyData>();
    [SerializeField] private GameObject _prefab;
    private List<Enemy> _enemiesSpawned;
    private float _interval;

    public bool IsReady => _enemiesSpawned.Count == 0;

    public void SpawnEnemies(List<EnemyData> enemies)
    {
        _enemiesToSpawn.Clear();
        _enemiesToSpawn = enemies;
        if (_enemiesToSpawn.Count > 0)
            StartCoroutine(SpawnEnemy());
    }

    private void Start()
    {
        _enemiesSpawned = new List<Enemy>();
    }


    private void OnSpawnerEnemyDestroy(Enemy destroyed)
    {
        _enemiesSpawned.Remove(destroyed);

        if (_enemiesSpawned.Count <= 0)
        {
            // do things here if necessary
        }
    }

    private IEnumerator SpawnEnemy()
    {
        foreach (EnemyData enemyData in _enemiesToSpawn)
        {
            _interval = Random.Range(0.1f, 0.5f);

            GameObject enemySpawned = Instantiate(_prefab, transform.position, Quaternion.identity);
            var enemyScript = enemySpawned.GetComponent<Enemy>();
            
            enemyScript.SetPathCreator(_pathCreator);
            SetEnemyData(enemyScript, enemyData);
            _enemiesSpawned.Add(enemyScript);
            enemyScript.onEnemyDestroy += OnSpawnerEnemyDestroy;
            yield return new WaitForSeconds(_interval);
        }
    }

    public void SetEnemyData(Enemy enemy, EnemyData data) 
    {
        enemy.lifePoint = data.lifePoint;
        enemy.sprite = data.EnemySprite;
        enemy.walkSpeed = data.walkSpeed;
        enemy.GoldEarned = data.GoldEarned;
        enemy.enemyDamageOverride = data.enemyDamageOverride;

    }
}
