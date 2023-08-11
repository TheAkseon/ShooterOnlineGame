using System;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    [SerializeField] private PlayerGun _playerGun;
    [SerializeField] private Animator _animator;

    private const string shoot = "Shoot";

    private void Start()
    {
        _playerGun.shoot += Shoot;
    }


    private void OnDestroy()
    {
        _playerGun.shoot -= Shoot;
    }
    private void Shoot()
    {
        _animator.SetTrigger(shoot);
    }
}
