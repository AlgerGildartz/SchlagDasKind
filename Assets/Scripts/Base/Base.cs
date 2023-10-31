using System;
using ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.Serialization;

public class Base : MonoBehaviour
{
    [SerializeField] private FloatVariable _currentHp;
    [SerializeField] private FloatVariable _maxHp;
    
    public float MaxHealthPoint;

    public static Action OnLooseHealthPoint;
    public static event Action OnBaseDestruction;


    private void Start()
    {
        _currentHp.value = MaxHealthPoint;
        _maxHp.value = MaxHealthPoint;
    }

    private void OnEnable()
    {
        _currentHp.value = MaxHealthPoint;
        OnLooseHealthPoint += LooseHealthPoint;
    }

    private void OnDisable()
    {
        OnLooseHealthPoint -= LooseHealthPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Get Enemy damage
            Enemy enemy = collision.GetComponent<Enemy>();
            if (!enemy)
                return;

            float enemyDamage = enemy.EnemyDamage;

            _currentHp.value -= enemyDamage;
            
            // Loose HP
            OnLooseHealthPoint?.Invoke();

            // Destroy the ennemy that contacted
            Destroy(collision.gameObject);
        }
    }

    private void LooseHealthPoint()
    {
        if(_currentHp.value <= 0)
        {
            OnBaseDestruction?.Invoke();
        }
    }
}
