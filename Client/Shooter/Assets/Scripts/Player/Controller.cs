using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCharacter))]
public class Controller : MonoBehaviour
{
    private PlayerCharacter _player;

    private void Start() => _player = GetComponent<PlayerCharacter>();

    private void Update()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");

        _player.SetInput(inputHorizontal, inputVertical);

        SendMove();
    }

    private void SendMove()
    {
        _player.GetMoveInfo(out Vector3 position, out Vector3 velocity);
        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            { "pX", position.x},
            { "pY", position.y},
            { "pZ", position.z},
            { "vX", velocity.x},
            { "vY", velocity.y},
            { "vZ", velocity.z}
        };
        MultiplayerManager.Instance.SendMessage("move", data);
    }
}
