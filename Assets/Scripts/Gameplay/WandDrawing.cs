using System.Collections.Generic;
using HarryPoter.Core;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using PDollarGestureRecognizer;
using UnityEngine;

public class WandDrawing : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float _sensitive;
    [SerializeField] private float _noActiveTimeToFinish;
    
    [Space]
    [Header("Refs")]
    [SerializeField] private Transform _wandEnd;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private HandGrabInteractable _grabInteractable;
    [SerializeField] private Wand _wand;

    private WandService _wandService;
    
    private Vector3 _prevWandEndPos;
    private float _lastAddedPointTime;
    private bool _isFinished = true;
    private List<Point> _linePoints = new List<Point>();

    private void Awake()
    {
        _prevWandEndPos = _wandEnd.position;

        _wandService = Engine.GetService<WandService>();
    }

    private void Update()
    {
        if (_wand.IsBusy)
        {
            return;
        }
        
        if ((_wandEnd.position - _prevWandEndPos).sqrMagnitude > _sensitive & _grabInteractable.State == InteractableState.Select)
        {
            _isFinished = false;
            AddNewPoint(_wandEnd.position);
        }
        
        if (_lastAddedPointTime + _noActiveTimeToFinish < Time.time & !_isFinished)
        {
            _isFinished = true;
            FinishDrawing();
        }
    }

    private void FinishDrawing()
    {
        _wandService.Recognize(_linePoints);
        _linePoints.Clear();
        _lineRenderer.positionCount = 0;
    }

    private void AddNewPoint(Vector3 newPoint)
    {
        _lastAddedPointTime = Time.time;
        
        _linePoints.Add(ConvertVector3ToPoint(newPoint));
        
        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPosition(_linePoints.Count-1, newPoint);

        _prevWandEndPos = newPoint;
    }

    private Point ConvertVector3ToPoint(Vector3 vector3)
    {
        return new Point(vector3.x, vector3.y, 1);
    }
}
