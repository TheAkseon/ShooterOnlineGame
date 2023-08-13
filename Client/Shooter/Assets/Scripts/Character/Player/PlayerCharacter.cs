using Colyseus.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCharacter : Character
{
    [SerializeField] private Health _health;
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
    private float _targetScaleY = 0.7f;
    private float _durationSit = 0.2f;
    private Vector3 _initialScale;
    private Vector3 _targetScale;

    private void Start()
    {
        _initialScale = transform.localScale;
        _targetScale = new Vector3(transform.localScale.x, _targetScaleY, transform.localScale.z);
        _jumpForce = 5f;
        _maxHeadAngle = 90f;
        _minHeadAngle = -90f;
        _rigidbody = GetComponent<Rigidbody>();
        Transform camera = Camera.main.transform;
        camera.parent = _cameraPoint;
        camera.localPosition = Vector3.zero;
        camera.localRotation = Quaternion.identity;

        _health.SetMax(maxHealth);
        _health.SetCurrent(maxHealth);
    }

    private void FixedUpdate()
    {
        Move();
        RotateY();
    }

    private void Move()
    {
        /*Vector3 direction = new Vector3(_inputHorizontal, 0, _inputVertical).normalized;
        transform.position += direction * Time.fixedDeltaTime * _speed;*/

        Vector3 velocity = (transform.forward * _inputVertical + transform.right * _inputHorizontal).normalized * speed;
        velocity.y = _rigidbody.velocity.y;
        Velocity = velocity;
        _rigidbody.velocity = velocity;
    }

    private void RotateY()
    {
        _rigidbody.angularVelocity = new Vector3(0, _rotateY, 0);
        _rotateY = 0f;
    }

    private IEnumerator SitDownCourotine()
    {
        float startTime = Time.time;

        while (Time.time - startTime < _durationSit)
        {
            float time = (Time.time - startTime) / _durationSit;

            transform.localScale = Vector3.Lerp(_initialScale, _targetScale, time);

            yield return null;
        }

        transform.localScale = _targetScale;
    }

    private IEnumerator GetUpCourutine()
    {
        float startTime = Time.time;

        while (Time.time - startTime < _durationSit)
        {
            float time = (Time.time - startTime) / _durationSit;

            transform.localScale = Vector3.Lerp(transform.localScale, _initialScale, time);

            yield return null;
        }

        transform.localScale = _initialScale;
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

    public void GetMoveInfo(out Vector3 position, out Vector3 velocity, out float rotateX, out float rotateY)
    {
        position = transform.position;
        velocity = _rigidbody.velocity;
        rotateY = transform.eulerAngles.y;
        rotateX = _head.localEulerAngles.x;
        Velocity = velocity;
    }

    public void GetScaleYInfo(out float scaleY)
    {
        scaleY = transform.localScale.y;
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

    public void SitDown()
    {
        StopAllCoroutines();

        StartCoroutine(SitDownCourotine());
    }

    public void GetUp()
    {
        StopAllCoroutines();

        StartCoroutine(GetUpCourutine());
    }

    internal void OnChange(List<DataChange> changes)
    {
        foreach (var dataChange in changes)
        {
            switch (dataChange.Field)
            {
                case "loss":
                    MultiplayerManager.Instance.LossCounter.SetPlayerLoss((byte)dataChange.Value);
                    break;
                case "currentHP":
                    _health.SetCurrent((sbyte)dataChange.Value);
                    break;
                default:
                    Debug.Log("Не обрабатывается изменение поля " + dataChange.Field);
                    break;
            }
        }
    }
}
