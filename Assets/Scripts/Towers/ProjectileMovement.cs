using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private Collider2D _target;
    public float Speed;
    public float Damage;

    public void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Quaternion rotation = Quaternion.LookRotation
            (_target.transform.position - transform.position, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        transform.position += (_target.transform.position - transform.position).normalized * Speed;
    }

    public void SetTarget(Collider2D target)
    {
        _target = target;
    }

    public void SetBulletProperties(float speed, float damage) 
    {
        Speed = speed;
        Damage = damage;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (_target != collision)
            return;
        collision.GetComponent<Enemy>().DamageEnemy(Damage);

        Destroy(gameObject);
    } 
}