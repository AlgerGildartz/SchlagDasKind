using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private PathCreator _pathCreator;

    private List<GameObject> _enemiesToSpawn = new List<GameObject>();

    private List<Enemy> _enemiesSpawned;
    private float _interval;
    
    public bool IsReady => _enemiesSpawned.Count == 0;

    public void SpawnEnemies(List<GameObject> enemies)
    {
        _enemiesToSpawn.Clear();
        _enemiesToSpawn = enemies;
        
        if(_enemiesToSpawn.Count > 0)
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
        foreach (GameObject enemy in _enemiesToSpawn)
        {
            _interval = Random.Range(0.1f, 0.5f);
            
            GameObject enemySpawned = Instantiate(enemy, transform.position, Quaternion.identity);
            var enemyScript = enemySpawned.GetComponent<Enemy>();
            enemyScript.SetPathCreator(_pathCreator);
            
            _enemiesSpawned.Add(enemyScript);
            enemyScript.onEnemyDestroy += OnSpawnerEnemyDestroy;
            yield return new WaitForSeconds(_interval);
        }
    }
}
