using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [field: SerializeField] public float speed { get; protected set; } = 5f;
    public Vector3 Velocity { get; protected set; }
}
