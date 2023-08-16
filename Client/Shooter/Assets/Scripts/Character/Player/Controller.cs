using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(PlayerCharacter))]
public class Controller : MonoBehaviour
{
    [SerializeField] private float _restartDelay = 3f;
    [SerializeField] private float _mouseSensitivity = 2f;
    [SerializeField] private PlayerWeaponController _gun;

    private PlayerCharacter _player;
    private MultiplayerManager _multiplayerManager;
    private bool _hold = false;

    private void Start()
    {
        _player = GetComponent<PlayerCharacter>();
        _multiplayerManager = MultiplayerManager.Instance;
    }

    private void Update()
    {
        if (_hold) return;

        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        bool isShoot = Input.GetMouseButton(0);

        bool isSpacePressed = Input.GetKeyDown(KeyCode.Space); 

        bool isSitDownKeyPressed = Input.GetKeyDown(KeyCode.Z);

        bool isSitDownKeyUp = Input.GetKeyUp(KeyCode.Z);

        float rotateY = mouseX * _mouseSensitivity;

        _player.SetInput(inputHorizontal, inputVertical, rotateY);
        _player.RotateX(-mouseY * _mouseSensitivity);

        if (isSpacePressed) 
            _player.Jump();

        if (isShoot && _gun.TryShoot(out ShootInfo shootInfo))
        {
            SendShoot(ref shootInfo);
        }

        if (isSitDownKeyPressed)
        {
            _player.SitDown();
        }

        if (isSitDownKeyUp)
        {
            _player.GetUp();
        }


        SendMove();
        SendScale();
    }

    private void SendShoot(ref ShootInfo shootInfo)
    {
        shootInfo.key = _multiplayerManager.GetSessionID();
        string json = JsonUtility.ToJson(shootInfo);

        _multiplayerManager.SendMessage("shoot", json);
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

        _multiplayerManager.SendMessage("move", data);
    }

    private void SendScale()
    {
        _player.GetScaleYInfo(out float scaleY);

        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            { "sY", scaleY}
        };

        _multiplayerManager.SendMessage("scale", data);
    }

    public void Restart(string jsonRestartInfo)
    {
        RestartInfo info = JsonUtility.FromJson<RestartInfo>(jsonRestartInfo);
        StartCoroutine(Hold());
        _player.transform.position = new Vector3(info.x, 0, info.z);
        _player.SetInput(0, 0, 0);

        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            { "pX", info.x},
            { "pY", 0},
            { "pZ", info.z},
            { "vX", 0},
            { "vY", 0},
            { "vZ", 0},
            { "rX", 0},
            { "rY", 0}
        };

        _multiplayerManager.SendMessage("move", data);
    }

    private IEnumerator Hold()
    {
        _hold = true;
        yield return new WaitForSecondsRealtime(_restartDelay);
        _hold = false;
    }
}

[System.Serializable]
public struct ShootInfo
{
    public string key;
    public float pX;
    public float pY;
    public float pZ;
    public float dX;
    public float dY;
    public float dZ;
}

[Serializable]
public struct RestartInfo
{
    public float x;
    public float z;
}
