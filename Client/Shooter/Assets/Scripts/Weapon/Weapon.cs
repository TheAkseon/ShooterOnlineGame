using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    public Action shoot;
    protected abstract WeaponData WeaponData { get; }
    public Bullet BulletPrefab => _bulletPrefab;
}
