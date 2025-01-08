using System.Collections.Generic;
using PDollarGestureRecognizer;
using UnityEngine;
using Utils;

namespace HarryPoter.Core
{
    public class WandDebugger : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private RectTransform _canvas;
        
        [Space]
        [Header("Configs")]
        [SerializeField] private bool _isOn;
     
        public bool IsOn => _isOn;

        private Plane _currentDrawingPlane;

        private void Update()
        {
            if (!IsOn)
            {
                _canvas.gameObject.SetActive(false);
                return;
            }
            
            GameManager gameManager = GameManager.Instance;
            if (gameManager == null)
            {
                return;
            }

            Player player = gameManager.GetPlayer();
            if (player == null)
            {
                return;
            }
            
            Vector3 planePoint = _currentDrawingPlane.normal * -_currentDrawingPlane.distance;
            
            _canvas.gameObject.SetActive(true);
            _canvas.position = planePoint;
            _canvas.rotation = Quaternion.LookRotation(_currentDrawingPlane.normal, Vector3.up);
        }

        public void DebugDrawing(List<Point> points, Plane drawingPlane)
        {
            MakeReset();
            
            _currentDrawingPlane = drawingPlane;
            _lineRenderer.positionCount = points.Count;

            for (int i = 0; i < points.Count; i++)
            {
                _lineRenderer.SetPosition(i, _currentDrawingPlane.ProjectToWorld(new Vector2(points[i].X, points[i].Y)));
            }
        }

        public void Reset()
        {
            MakeReset();
        }

        private void MakeReset()
        {
            _lineRenderer.positionCount = 0;
        }
    }
}