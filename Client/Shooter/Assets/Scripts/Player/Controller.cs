using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCharacter))]
public class Controller : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 2f;
    [SerializeField] private PlayerGun _gun;

    private PlayerCharacter _player;

    private void Start() => _player = GetComponent<PlayerCharacter>();

    private void Update()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        bool isShoot = Input.GetMouseButton(0);

        bool isSpacePressed = Input.GetKeyDown(KeyCode.Space); 

        float rotateY = mouseX * _mouseSensitivity;

        _player.SetInput(inputHorizontal, inputVertical, rotateY);
        _player.RotateX(-mouseY * _mouseSensitivity);

        if (isSpacePressed) 
            _player.Jump();

        if (isShoot)
            _gun.Shoot();
        
        SendMove();
    }

    private void SendMove()
    {
        _player.GetMoveInfo(out Vector3 position, out Vector3 velocity, out float rotateX, out float rotateY);
        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            { "pX", position.x},
            { "pY", position.y},
            { "pZ", position.z},
            { "vX", velocity.x},
            { "vY", velocity.y},
            { "vZ", velocity.z},
            { "rX", rotateX},
            { "rY", rotateY}
        };
        MultiplayerManager.Instance.SendMessage("move", data);
    }
}
