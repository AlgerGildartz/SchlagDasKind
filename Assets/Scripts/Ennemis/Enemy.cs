using System;
using PathCreation;
using PathCreation.Examples;
using UnityEngine;

public enum EnemyGradeDamage
{
    Weak = 1,
    Normal = 3,
    Strong = 8,
    Boss = 50,
    Snail = 99999
}


public class Enemy : MonoBehaviour
{
    
    public Action<Enemy> onEnemyDeath;
    public Action<Enemy> onEnemyDestroy;
    
    public float lifePoint = 20.0f;
    public float walkSpeed = 2.0f;
    public EnemyGradeDamage enemyGradeDamage = EnemyGradeDamage.Weak;
    public float enemyDamageOverride = 0.0f;
    public int GoldEarned;
    public Sprite sprite;

    public string nameReference;
    private float _enemyDamage;
    public float EnemyDamage => _enemyDamage;
    
    [SerializeField] private PathFollower _pathFollower;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _pathFollower.speed = walkSpeed;

        _spriteRenderer.sprite = sprite;

        _enemyDamage = enemyDamageOverride > 0.0f ? enemyDamageOverride : (int) enemyGradeDamage;
        
        //Destroy(gameObject, 5.0f);
    }

    public void DamageEnemy(float damage)
    {
        lifePoint -= damage;
        SoundManager.Instance.PlayTouchEnemySound();

        if (lifePoint > 0.0f) return;

        Monnaie.MoneySystem.AddMoney(GoldEarned);
        onEnemyDeath?.Invoke(this);
        Destroy(gameObject);
    }

    public void SetPathCreator(PathCreator pathCreator)
    {
        _pathFollower.pathCreator = pathCreator;
    }

    private void OnDisable()
    {
        onEnemyDestroy.Invoke(this);
    }

    private void OnDestroy()
    {
    }
}
