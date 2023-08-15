using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Animator _animator;
    public Action shoot;
    public Bullet BulletPrefab => _bulletPrefab;
    public Animator Animator => _animator;

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
