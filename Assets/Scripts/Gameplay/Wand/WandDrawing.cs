using System;
using System.Collections.Generic;
using HarryPoter.Core;
using PDollarGestureRecognizer;
using UnityEngine;

public class WandDrawing : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float _sensitive = 0.0001f;
    [SerializeField] private float _noActiveTimeToFinish = 0.5f;
    [SerializeField] private bool _isDebugOn = false;
    
    [Space]
    [Header("Refs")]
    [SerializeField] private Transform _wandEnd;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LineRenderer _testLine;
    [SerializeField] private Wand _wand;

    private WandService _wandService;
    private Transform _player;
    
    private Vector3 _prevWandEndPos;
    private float _lastAddedPointTime;
    private bool _isStarted;
    
    private Plane _plane;

    private void Awake()
    {
        _wandService = Engine.GetService<WandService>();
        _player = Engine.GetService<InputService>().Player.transform;
    }

    private void Update()
    {
        if (_wand.IsBusy | !_wand.GrabInteractable.IsGrabbed)
        {
            return;
        }
        
        if ((_wandEnd.position - _prevWandEndPos).sqrMagnitude > _sensitive)
        {
            if (!_isStarted)
            {
                _isStarted = true;
                StartDrawing();
            }
            
            AddNewPoint(_wandEnd.position);
        }
        
        if (_lastAddedPointTime + _noActiveTimeToFinish < Time.time & _isStarted)
        {
            _isStarted = false;
            FinishDrawing();
        }
    }

    public void Reset()
    {
        _lineRenderer.positionCount = 0;
        _lastAddedPointTime = 0;
    }
    
    private void AddNewPoint(Vector3 newPoint)
    {
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount-1, newPoint);
        
        _lastAddedPointTime = Time.time;
        _prevWandEndPos = newPoint;
    }

    private void StartDrawing()
    {
        _plane = new Plane(_player.forward, _player.position);
        _prevWandEndPos = _wandEnd.position;
        
        Reset();
    }

    private void FinishDrawing()
    {
        List<Point> points = ConvertLineRenderPositionsToPoints(_lineRenderer);
        _wandService.Recognize(points);
        
        Reset();

        if (_isDebugOn)
        {
            FillLine(points, _testLine);
        }
    }

    private void FillLine(List<Point> points, LineRenderer lineRenderer)
    {
        lineRenderer.positionCount = 0;
        lineRenderer.positionCount = points.Count;

        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(points[i].X, points[i].Y, 0) + _player.position + _player.forward);
        }
    }
    
    private List<Point> ConvertLineRenderPositionsToPoints(LineRenderer lineRenderer)
    {
        List<Point> res = new List<Point>();

        Vector3 firstPointOnPlane = _plane.ClosestPointOnPlane(lineRenderer.GetPosition(0));
        
        res.Add(new Point(0,0,0));
        
        for(int i = 1; i < lineRenderer.positionCount; i++)
        {
            res.Add(Vector3ToPoint(_plane.ClosestPointOnPlane(lineRenderer.GetPosition(i)), firstPointOnPlane));
        }
        
        return res;
    }

    private Point Vector3ToPoint(Vector3 vector3, Vector3 firstPoint)
    {
        Vector3 temp = vector3 - firstPoint;
        float[] arr = { temp.x, temp.y, temp.z };
        Array.Sort(arr, new Comparer());
            
        return new Point(arr[0], arr[1], 0);
    }
    
    private class Comparer : IComparer<float>
    {
        public int Compare(float x, float y)
        {
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                return -1;
            }
        
            if(Mathf.Abs(x) > Mathf.Abs(y))
            {
                return 1;
            }

            return 0;
        }
    }
}
