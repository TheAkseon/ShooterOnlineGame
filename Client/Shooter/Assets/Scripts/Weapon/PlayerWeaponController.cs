using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private WeaponData _pistolData;
    [SerializeField] private WeaponData _thompsonData;
    [SerializeField] private Transform _weaponPoint;
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
            ChangeWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon(2);
        }
    }

    private void ChangeWeapon(int index)
    {
        if (index == 1)
        {
            _currentWeapon.Destroy();
            _currentWeapon = WeaponFactory.CreateWeapon(_thompsonData, _weaponPoint);
            _currentWeapon.gameObject.transform.SetParent(_weaponPoint);
            _gunAnimation.SetGun(_currentWeapon, _currentWeapon.Animator);
        }
        else if (index == 2)
        {
            _currentWeapon.Destroy();
            _currentWeapon = WeaponFactory.CreateWeapon(_pistolData, _weaponPoint);
            _currentWeapon.gameObject.transform.SetParent(_weaponPoint);
            _gunAnimation.SetGun(_currentWeapon, _currentWeapon.Animator);
        }

        ChangeWeaponInfo changeWeaponInfo = new ChangeWeaponInfo();
        {
            changeWeaponInfo.key = MultiplayerManager.Instance.GetSessionID();
            changeWeaponInfo.index = index;
        };

        string json = JsonUtility.ToJson(changeWeaponInfo);
        MultiplayerManager.Instance.SendMessage("changeWeapon", json);
    }

    public bool TryShoot(out ShootInfo shootInfo)
    {
        shootInfo = new ShootInfo();

        if (Time.time - _lastShootTime < _currentWeapon.ShootDelay) return false;

        Vector3 velocity = _currentWeapon.BulletPoint.forward * _currentWeapon.BulletSpeed;
        int damage = _currentWeapon.Damage;

        _lastShootTime = Time.time;
        Instantiate(_currentWeapon.BulletPrefab, _currentWeapon.BulletPoint.position, _currentWeapon.BulletPoint.rotation).Init(velocity, damage);
        _currentWeapon.shoot?.Invoke();

        shootInfo.pX = _currentWeapon.BulletPoint.position.x;
        shootInfo.pY = _currentWeapon.BulletPoint.position.y;
        shootInfo.pZ = _currentWeapon.BulletPoint.position.z;
        shootInfo.dX = velocity.x;
        shootInfo.dY = velocity.y;
        shootInfo.dZ = velocity.z;

        return true;
    }
}
