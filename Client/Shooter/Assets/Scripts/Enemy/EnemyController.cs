using Colyseus.Schema;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyCharacter))]
public class EnemyController : MonoBehaviour
{
    private Vector3 _newPosition;
    private Vector3 _oldPosition = Vector3.zero;

    internal void OnChange(List<DataChange> changes)
    {
        foreach (var dataChange in changes)
        {
            switch (dataChange.Field)
            {
                case "x":
                    _oldPosition.x = (float)dataChange.PreviousValue;
                    _newPosition.x = (float)dataChange.Value;
                    break;
                case "y":
                    _oldPosition.z = (float)dataChange.PreviousValue;
                    _newPosition.z = (float)dataChange.Value;
                    break;
                default:
                    Debug.Log("Не обрабатывается изменение поля " + dataChange.Field);
                    break;
            }
        }

        if(_oldPosition != _newPosition)
        {
            _newPosition = Vector3.Lerp(_oldPosition, _newPosition, 5f);
        }
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _newPosition, 10f * Time.deltaTime);
    }
}
