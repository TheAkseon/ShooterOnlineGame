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
    public bool TryShoot(out ShootInfo shootInfo)
    {
        shootInfo = new ShootInfo();

        if (Time.time - _lastShootTime < _shootDelay) return false;

        Vector3 position = _bulletPoint.position;
        Vector3 direction = _bulletPoint.forward;

        _lastShootTime = Time.time;
        Instantiate(_bulletPrefab, _bulletPoint.position, _bulletPoint.rotation).Init(_bulletPoint.forward, _bulletSpeed);
        shoot?.Invoke();

        direction *= _bulletSpeed;

        shootInfo.pX = position.x;
        shootInfo.pY = position.y;
        shootInfo.pZ = position.z;
        shootInfo.dX = direction.x;
        shootInfo.dY = direction.y;
        shootInfo.dZ = direction.z;

        return true;
    }
}
