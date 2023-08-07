using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;

    private float _inputHorizontal;
    private float _inputVertical;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = new Vector3(_inputHorizontal, 0, _inputVertical).normalized;
        transform.position += direction * Time.fixedDeltaTime * _speed;
    }

    public void SetInput(float inputHorizontal, float inputVertical)
    {
        _inputHorizontal = inputHorizontal;
        _inputVertical = inputVertical;
    }

    public void GetMoveInfo(out Vector3 position)
    {
        position = transform.position;
    }
}
