using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCharacter : Character
{
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _cameraPoint;
    [SerializeField] private float _maxHeadAngle = 90f;
    [SerializeField] private float _minHeadAngle = -90f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private CheckFly _checkFly;
    [SerializeField] private float _jumpDelay = .2f;

    private Rigidbody _rigidbody;
    private float _inputHorizontal;
    private float _inputVertical;
    private float _rotateY;
    private float _currentRotateX;
    private float _jumpTime;

    private void Start()
    {
        _jumpForce = 5f;
        _maxHeadAngle = 90f;
        _minHeadAngle = -90f;
        _rigidbody = GetComponent<Rigidbody>();
        Transform camera = Camera.main.transform;
        camera.parent = _cameraPoint;
        camera.localPosition = Vector3.zero;
        camera.localRotation = Quaternion.identity;
    }

    private void  FixedUpdate()
    {
        Move();
        RotateY();
    }

    private void Move()
    {
        /*Vector3 direction = new Vector3(_inputHorizontal, 0, _inputVertical).normalized;
        transform.position += direction * Time.fixedDeltaTime * _speed;*/

        Vector3 velocity = (transform.forward * _inputVertical + transform.right * _inputHorizontal).normalized * Speed;
        velocity.y = _rigidbody.velocity.y;
        Velocity = velocity;
        _rigidbody.velocity = velocity;
    }

    private void RotateY()
    {
        _rigidbody.angularVelocity = new Vector3(0, _rotateY, 0);
        _rotateY = 0f;
    }

    public void RotateX(float value)
    {
        _currentRotateX = Mathf.Clamp(_currentRotateX + value, _minHeadAngle, _maxHeadAngle);
        _head.localEulerAngles = new Vector3(_currentRotateX, 0, 0);
    }

    public void SetInput(float inputHorizontal, float inputVertical, float rotateY)
    {
        _inputHorizontal = inputHorizontal;
        _inputVertical = inputVertical;
        _rotateY += rotateY;
    }

    public void GetMoveInfo(out Vector3 position, out Vector3 velocity)
    {
        position = transform.position;
        velocity = _rigidbody.velocity;
        Velocity = velocity;
    }

    public void Jump()
    {
        if (_checkFly.IsFly)
            return;

        if (Time.time - _jumpTime < _jumpDelay)
            return;

        _jumpTime = Time.time;
        _rigidbody.AddForce(0, _jumpForce, 0, ForceMode.VelocityChange);
    }
}
