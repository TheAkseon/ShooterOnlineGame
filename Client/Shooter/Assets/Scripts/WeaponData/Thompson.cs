using UnityEngine;

public class Thompson : Weapon
{
    [SerializeField] private WeaponData _weaponData;

    protected override WeaponData WeaponData => _weaponData;
}
