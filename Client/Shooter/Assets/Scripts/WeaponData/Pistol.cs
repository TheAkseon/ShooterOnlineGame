using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private WeaponData _weaponData;

    protected override WeaponData WeaponData => _weaponData;
}
