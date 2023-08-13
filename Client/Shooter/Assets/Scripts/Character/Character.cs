using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [field: SerializeField] public float speed { get; protected set; } = 5f;
    [field: SerializeField] public int maxHealth { get; protected set; } = 10;
    public Vector3 Velocity { get; protected set; }
}
