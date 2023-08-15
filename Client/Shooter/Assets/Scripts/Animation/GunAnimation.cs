using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    private Weapon _gun;
    private Animator _animator;

    private const string shoot = "Shoot";

    /*private void Start()
    {
        _gun.shoot += Shoot;
    }*/


    private void OnDestroy()
    {
        _gun.shoot -= Shoot;
    }
    private void Shoot()
    {
        _animator.SetTrigger(shoot);
    }

    public void SetGun(Weapon weapon, Animator animator)
    {
        _gun = weapon;
        _animator = animator;
        _gun.shoot += Shoot;
    }
}
