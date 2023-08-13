using UnityEngine;
using UnityEngine.Rendering;

public class Health : MonoBehaviour
{
    [SerializeField] private HealthUI _ui;

    private int _max;
    private int _current;

    private void UpdateHP()
    {
        _ui.UpdateHealth(_max, _current);
    }

    public void SetMax(int max)
    {
        _max = max;
        UpdateHP();
    }

    public void SetCurrent(int current)
    {
        _current = current;
        UpdateHP();
    }

    public void ApplyDamage(int damage)
    {
        _current -= damage;
        UpdateHP();
    }
}
