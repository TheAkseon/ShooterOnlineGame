using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shootDelay;
    [SerializeField] private int _damage; 
    public Action shoot;
    public Bullet BulletPrefab => _bulletPrefab;
    public Animator Animator => _animator;
    public Transform BulletPoint => _bulletPoint;
    public float BulletSpeed => _bulletSpeed;
    public float ShootDelay => _shootDelay;
    public int Damage => _damage;

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
