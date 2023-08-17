using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    [SerializeField] private WeaponData _pistolData;
    [SerializeField] private WeaponData _thompsonData;
    [SerializeField] private GunAnimation _gunAnimation;
    [SerializeField] private Transform _weaponPoint;

    private Weapon _currentWeapon;

    private void Start()
    {
        _currentWeapon = WeaponFactory.CreateWeapon(_thompsonData, _weaponPoint);
        _currentWeapon.gameObject.transform.SetParent(_weaponPoint);
        _gunAnimation.SetGun(_currentWeapon, _currentWeapon.Animator);
    }

    public void ChangeWeapon(int index)
    {
        if(index == 1)
        {
            _currentWeapon.Destroy();
            _currentWeapon = WeaponFactory.CreateWeapon(_thompsonData, _weaponPoint);
            _currentWeapon.gameObject.transform.SetParent(_weaponPoint);
            _gunAnimation.SetGun(_currentWeapon, _currentWeapon.Animator);
        }
        else if(index == 2)
        {
            _currentWeapon.Destroy();
            _currentWeapon = WeaponFactory.CreateWeapon(_pistolData, _weaponPoint);
            _currentWeapon.gameObject.transform.SetParent(_weaponPoint);
            _gunAnimation.SetGun(_currentWeapon, _currentWeapon.Animator);
        }
    }

    public void Shoot(Vector3 position, Vector3 velocity)
    {
        Instantiate(_currentWeapon.BulletPrefab, _currentWeapon.BulletPoint.position, _currentWeapon.BulletPoint.rotation).Init(velocity, _currentWeapon.Damage);
        _currentWeapon.shoot?.Invoke();
    }
}
