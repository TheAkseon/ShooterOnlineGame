using System;
using UnityEngine;

public class PlayerGun : Gun
{
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shootDelay;

    private float _lastShootTime;

    public bool TryShoot(out ShootInfo shootInfo)
    {
        shootInfo = new ShootInfo();

        if (Time.time - _lastShootTime < _shootDelay) return false;

        Vector3 position = _bulletPoint.position;
        Vector3 velocity = _bulletPoint.forward * _bulletSpeed;

        _lastShootTime = Time.time;
        Instantiate(_bulletPrefab, _bulletPoint.position, _bulletPoint.rotation).Init(velocity);
        shoot?.Invoke();

        shootInfo.pX = position.x;
        shootInfo.pY = position.y;
        shootInfo.pZ = position.z;
        shootInfo.dX = velocity.x;
        shootInfo.dY = velocity.y;
        shootInfo.dZ = velocity.z;

        return true;
    }
}
