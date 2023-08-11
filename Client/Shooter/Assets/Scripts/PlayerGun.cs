using System;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shootDelay;

    private float _lastShootTime;
    public Action shoot;
    public void Shoot()
    {
        if (Time.time - _lastShootTime < _shootDelay) return;
        _lastShootTime = Time.time;
        Instantiate(_bulletPrefab, _bulletPoint.position, _bulletPoint.rotation).Init(_bulletPoint.forward, _bulletSpeed);
        shoot?.Invoke();
    }
}
