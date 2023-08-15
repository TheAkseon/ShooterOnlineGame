using System;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private WeaponData _pistolData;
    [SerializeField] private WeaponData _thompsonData;
    [SerializeField] private int _damage;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shootDelay;
    [SerializeField] private GunAnimation _gunAnimation;

    private float _lastShootTime;
    private Weapon _currentWeapon;

    private void Start()
    {
        _currentWeapon = WeaponFactory.CreateWeapon(_thompsonData, _weaponPoint);
        _currentWeapon.gameObject.transform.SetParent(_weaponPoint);
        _gunAnimation.SetGun(_currentWeapon, _currentWeapon.Animator);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _currentWeapon.Destroy();
            _currentWeapon = WeaponFactory.CreateWeapon(_thompsonData, _weaponPoint);
            _currentWeapon.gameObject.transform.SetParent(_weaponPoint);
            _gunAnimation.SetGun(_currentWeapon, _currentWeapon.Animator);
            //_currentWeapon.gameObject.transform.SetParent(_weaponPoint.gameObject.transform, true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _currentWeapon.Destroy();
            _currentWeapon = WeaponFactory.CreateWeapon(_pistolData, _weaponPoint);
            _currentWeapon.gameObject.transform.SetParent(_weaponPoint);
            _gunAnimation.SetGun(_currentWeapon, _currentWeapon.Animator);
        }
    }

    public bool TryShoot(out ShootInfo shootInfo)
    {
        shootInfo = new ShootInfo();

        if (Time.time - _lastShootTime < _shootDelay) return false;

        Vector3 position = _bulletPoint.position;
        Vector3 velocity = _bulletPoint.forward * _bulletSpeed;

        _lastShootTime = Time.time;
        //сделать для каждого оружия свою точку спавна пули
        Instantiate(_currentWeapon.BulletPrefab, _bulletPoint.position, _bulletPoint.rotation).Init(velocity, _damage);
        _currentWeapon.shoot?.Invoke();

        shootInfo.pX = position.x;
        shootInfo.pY = position.y;
        shootInfo.pZ = position.z;
        shootInfo.dX = velocity.x;
        shootInfo.dY = velocity.y;
        shootInfo.dZ = velocity.z;

        return true;
    }
}
