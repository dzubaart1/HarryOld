using System;
using System.Collections.Generic;
using HarryPoter.Core;
using UnityEngine;

public class Snitch : MonoBehaviour, ISpellable
{
    [SerializeField] private List<Transform> _points;
    [SerializeField] private GameObject _earPrefab;

    private const float ACCURACY = 0.1f;
    
    private int _currentPoint;

    private void Start()
    {
        transform.position = _points[_currentPoint].position;
    }

    private void Update()
    {
        if ((transform.position - _points[_currentPoint].position).sqrMagnitude < ACCURACY)
        {
            _currentPoint = (_currentPoint + 1) % _points.Count;
        }

        transform.position = Vector3.MoveTowards(transform.position, _points[_currentPoint].position, Time.deltaTime * 0.1f);
        transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.LookRotation(_points[_currentPoint].position - transform.position), Time.deltaTime*5);
    }

    public void OnOpenSpell()
    {
    }

    public void OnAttackSpell()
    {
        Instantiate(_earPrefab, transform.position, Quaternion.identity);
    }

    public void OnTakeSpell()
    {
    }
}
