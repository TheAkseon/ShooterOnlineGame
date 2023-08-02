using UnityEngine;

[RequireComponent(typeof(Player))]
public class Controller : MonoBehaviour
{
    private Player _player;

    private void Start() => _player = GetComponent<Player>();

    private void Update()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");

        _player.SetInput(inputHorizontal, inputVertical);
    }
}
