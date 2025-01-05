using System.Collections.Generic;
using PDollarGestureRecognizer;
using UnityEngine;

namespace HarryPoter.Core
{
    public class WandDebugger : MonoBehaviour
    {
        public bool IsOn => _isOn;
        
        [Header("Refs")]
        [SerializeField] private LineRenderer _lineRenderer;

        [Space]
        [Header("Configs")]
        [SerializeField] private bool _isOn;
        
        public void DebugDrawing(List<Point> points, Vector3 pointToDebug)
        {
            _lineRenderer.positionCount = 0;
            _lineRenderer.positionCount = points.Count;

            for (int i = 0; i < points.Count; i++)
            {
                _lineRenderer.SetPosition(i, new Vector3(points[i].X, points[i].Y, 0) + pointToDebug);
            }
        }

        public void ResetDebugDrawing()
        {
            _lineRenderer.positionCount = 0;
        }

    }
}