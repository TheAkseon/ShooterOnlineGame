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
    }
}
