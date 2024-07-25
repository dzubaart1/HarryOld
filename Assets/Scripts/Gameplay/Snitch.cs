using System.Collections.Generic;
using UnityEngine;

public class Snitch : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;
    [SerializeField] private GameObject _earPrefab;

    private const float ACCURACY = 0.1f;
    
    private int _currentPoint;

    private void Update()
    {
        if ((transform.position - _points[_currentPoint].position).sqrMagnitude < ACCURACY)
        {
            _currentPoint = (_currentPoint + 1) % _points.Count;
        }

        transform.position = Vector3.Lerp(transform.position, _points[_currentPoint].position, Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.LookRotation(_points[_currentPoint].position - transform.position), Time.deltaTime);
    }
}
