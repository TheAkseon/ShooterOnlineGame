using UnityEngine;
using UnityEngine.Rendering;

public class EnemyCharacter : MonoBehaviour
{
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
