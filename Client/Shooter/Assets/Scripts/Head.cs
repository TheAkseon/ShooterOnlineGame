using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] private EnemyCharacter _enemyCharacter;

    public void ApplyDamage(int damage)
    {
        _enemyCharacter.ApplyDamage(damage);
    }
}
