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
        _player.GetMoveInfo(out Vector3 position);
        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            { "x", position.x},
            { "y", position.z}
        };
        MultiplayerManager.Instance.SendMessage("move", data);
    }
}
