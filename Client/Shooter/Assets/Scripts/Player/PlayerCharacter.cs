using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;

    private Rigidbody _rigidbody;
    private float _inputHorizontal;
    private float _inputVertical;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void  FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        /*Vector3 direction = new Vector3(_inputHorizontal, 0, _inputVertical).normalized;
        transform.position += direction * Time.fixedDeltaTime * _speed;*/

        Vector3 velocity = (transform.forward * _inputVertical + transform.right * _inputHorizontal).normalized * _speed;
        _rigidbody.velocity = velocity;
    }

    public void SetInput(float inputHorizontal, float inputVertical)
    {
        _inputHorizontal = inputHorizontal;
        _inputVertical = inputVertical;
    }

    public void GetMoveInfo(out Vector3 position, out Vector3 velocity)
    {
        position = transform.position;
        velocity = _rigidbody.velocity;
    }
}
