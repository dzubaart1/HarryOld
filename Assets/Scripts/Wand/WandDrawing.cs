using System;
using System.Collections.Generic;
using PDollarGestureRecognizer;
using UnityEngine;
using Utils;

public class WandDrawing : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float _sensitive = 0.0001f;
    
    [Space]
    [Header("Refs")]
    [SerializeField] private Transform _wandEnd;
    [SerializeField] private LineRenderer _lineRenderer;
    
    public bool IsDrawing { get; private set; }
    public float LastAddedPointTime { get; private set; }
    
    private Plane _plane;
    private Vector3 _prevWandEndPos;

    private void Update()
    {
        if (!IsDrawing)
        {
            return;
        }

        if ((_wandEnd.position - _prevWandEndPos).sqrMagnitude > _sensitive)
        {
            AddNewPoint(_wandEnd.position);
        }
    }

    public void StartDrawing(Plane drawingPlane)
    {
        MakeReset();
        
        _plane = drawingPlane;
        IsDrawing = true;
    }

    public List<Point> FinishDrawing()
    {
        if (_lineRenderer == null || _lineRenderer.positionCount == 0)
        {
            return new List<Point>();
        }
        
        List<Point> res = ConvertLineRenderPositionsToPoints(_lineRenderer); 
        
        IsDrawing = false;
        return res;
    }

    public void Reset()
    {
        MakeReset();
        IsDrawing = false;
    }
    
    private void AddNewPoint(Vector3 newPoint)
    {
        _lineRenderer.SetPosition(_lineRenderer.positionCount++, newPoint);
        
        LastAddedPointTime = Time.time;
        _prevWandEndPos = newPoint;
    }
    
    private void MakeReset()
    {
        _lineRenderer.positionCount = 0;
        LastAddedPointTime = Time.time;
        _prevWandEndPos = _wandEnd.position;
    }
    
    private List<Point> ConvertLineRenderPositionsToPoints(LineRenderer lineRenderer)
    {
        List<Point> res = new List<Point>();
        
        for(int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector2 point = _plane.ProjectToPlane(lineRenderer.GetPosition(i));
            res.Add(new Point(point.x, point.y, 0));
        }
        
        return res;
    }
}
