using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField] private Health _health;
    [SerializeField] private Transform _head;
    [SerializeField] private float _rotationSpeed = 15f;
    [SerializeField] private float _timeChangeLocalScale = 15f;

    public Vector3 TargetPosition { get; private set; } = Vector3.zero;
    private float _velocityMagnitude = 0f;
    private Vector3 _localEulerAnglesX;
    private Vector3 _localEulerAnglesY;
    private Vector3 _targetLocalScale;

    private void Start()
    {
        TargetPosition = transform.position;

        _targetLocalScale = transform.localScale;
    }

    private void Update()
    {
        if (_velocityMagnitude > Mathf.Epsilon)
        {
            float maxDistance = _velocityMagnitude * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, maxDistance);
        }
        else
        {
            transform.position = TargetPosition;
        }

        _head.localRotation = Quaternion.Lerp(_head.localRotation, Quaternion.Euler(_localEulerAnglesX), Time.deltaTime * _rotationSpeed);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(_localEulerAnglesY), Time.deltaTime * _rotationSpeed);
        transform.localScale = Vector3.Lerp(transform.localScale, _targetLocalScale, Time.deltaTime * _timeChangeLocalScale);

    }

    public void SetSpeed(float value) => speed = value;

    public void SetMaxHP(int value)
    {
        maxHealth = value;
        _health.SetMax(value);
        _health.SetCurrent(value);
    } 

    public void SetMovement(in Vector3 position, in Vector3 velocity, in float averageInterval)
    {
        TargetPosition = position + (velocity * averageInterval);
        _velocityMagnitude = velocity.magnitude;
        Velocity = velocity;
    }

    public void ApplyDamage(int damage)
    {
        _health.ApplyDamage(damage);
    }

    public void SetRotateX(float value)
    {
        _localEulerAnglesX = new Vector3(value, 0, 0);
    }

    public void SetRotateY(float value)
    {
        _localEulerAnglesY = new Vector3(0, value, 0);
    }

    public void SetScaleY(float scaleY)
    {
        _targetLocalScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }
}