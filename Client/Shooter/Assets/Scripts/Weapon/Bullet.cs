using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _lifeTime = 5f;

    private int _damage;
    private int _headshotDamage;

    public void Init(Vector3 velocity, int damage = 0)
    {
        _damage = damage;
        _headshotDamage = damage * 3;
        _rigidbody.velocity = velocity;
        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out EnemyCharacter enemy))
        {
            enemy.ApplyDamage(_damage);
        }
        else if(collision.collider.TryGetComponent(out Head head))
        {
            head.ApplyDamage(_headshotDamage);
        }

        Destroy();
    }
}
