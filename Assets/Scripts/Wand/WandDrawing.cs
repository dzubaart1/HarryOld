using System;
using System.Collections.Generic;
using PDollarGestureRecognizer;
using UnityEngine;

public class WandDrawing : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float _sensitive = 0.0001f;
    
    [Space]
    [Header("Refs")]
    [SerializeField] private Transform _wandEnd;
    
    [Space]
    [Header("Views")]
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

        if ((_wandEnd.position - _prevWandEndPos).sqrMagnitude > _sensitive & IsDrawing)
        {
            AddNewPoint(_wandEnd.position);
        }
    }
    
    private void AddNewPoint(Vector3 newPoint)
    {
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount-1, newPoint);
        
        LastAddedPointTime = Time.time;
        _prevWandEndPos = newPoint;
    }

    public void StartDrawing(Plane drawingPlane)
    {
        _plane = drawingPlane;
        _prevWandEndPos = _wandEnd.position;
        
        IsDrawing = true;
    }

    public List<Point> FinishDrawing()
    {
        List<Point> res = ConvertLineRenderPositionsToPoints(_lineRenderer); 
        
        MakeReset();
        
        return res;
    }

    public void Reset()
    {
        MakeReset();
    }
    
    private void MakeReset()
    {
        _lineRenderer.positionCount = 0;
        LastAddedPointTime = 0;
        _prevWandEndPos = Vector3.zero;

        IsDrawing = false;
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
