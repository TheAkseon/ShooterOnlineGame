using System;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private Animator _animator;

    private const string shoot = "Shoot";

    private void Start()
    {
        _gun.shoot += Shoot;
    }


    private void OnDestroy()
    {
        _gun.shoot -= Shoot;
    }
    private void Shoot()
    {
        _animator.SetTrigger(shoot);
    }
}
