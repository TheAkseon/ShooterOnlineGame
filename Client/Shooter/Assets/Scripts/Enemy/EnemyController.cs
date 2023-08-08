using Colyseus.Schema;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyCharacter))]
public class EnemyController : MonoBehaviour
{
    private EnemyCharacter _character;
    private int _receiveTimeIntervalCount = 5;
    private List<float> _receiveTimeInterval = new List<float> { 0, 0, 0, 0, 0};
    private float AverageInterval
    {
        get
        {
            float summ = 0f;

            for (int i = 0; i < _receiveTimeInterval.Count; i++)
            {
                summ += _receiveTimeInterval[i];
            }

            return summ / _receiveTimeInterval.Count;
        }
    }
    private float _lastReceiveTime = 0f;

    private void Start()
    {
        _character = GetComponent<EnemyCharacter>();
    }

    private void SaveReceiveTime()
    {
        float interval = Time.time - _lastReceiveTime;
        _lastReceiveTime = Time.time;

        _receiveTimeInterval.Add(interval);

        if(_receiveTimeInterval.Count > _receiveTimeIntervalCount)
        {
            _receiveTimeInterval.RemoveAt(0);
        }
    }

    internal void OnChange(List<DataChange> changes)
    {
        SaveReceiveTime();

        Vector3 position = _character.TargetPosition;
        Vector3 velocity = Vector3.zero;

        foreach (var dataChange in changes)
        {
            switch (dataChange.Field)
            {
                case "pX":
                    position.x = (float)dataChange.Value;
                    break;
                case "pY":
                    position.y = (float)dataChange.Value;
                    break;
                case "pZ":
                    position.z = (float)dataChange.Value;
                    break;
                case "vX":
                    velocity.x = (float)dataChange.Value;
                    break;
                case "vY":
                    velocity.y = (float)dataChange.Value;
                    break;
                case "vZ":
                    velocity.z = (float)dataChange.Value;
                    break;
                default:
                    Debug.Log("Не обрабатывается изменение поля " + dataChange.Field);
                    break;
            }
        }

        _character.SetMovement(position, velocity, AverageInterval);
    }
}
