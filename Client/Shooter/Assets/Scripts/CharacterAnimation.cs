using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private CheckFly _checkFly;
    [SerializeField] private Animator _animator;
    [SerializeField] private Character _character;

    private const string Grounded = "Grounded";
    private const string Speed = "Speed";

    private void Update()
    {
        Vector3 localVelocity = _character.transform.InverseTransformVector(_character.Velocity);
        float speed = localVelocity.magnitude / _character.speed;
        float sign = Mathf.Sign(localVelocity.z);

        _animator.SetFloat(Speed, speed * sign);
        _animator.SetBool(Grounded, _checkFly.IsFly == false);
    }
}
