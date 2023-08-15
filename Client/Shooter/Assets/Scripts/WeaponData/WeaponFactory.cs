using UnityEngine;

public class WeaponFactory : MonoBehaviour 
{
    public static Weapon CreateWeapon(WeaponData weaponData, Transform weaponPoint)
    {
        GameObject weaponObject = Instantiate(weaponData.weaponPrefab, weaponPoint.position, weaponPoint.rotation);
        return weaponObject.GetComponent<Weapon>();
    }
}
