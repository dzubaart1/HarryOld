
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Threading;
using PDollarGestureRecognizer;
using UnityEngine.Events;
using System.IO;

public class DetectingTransfer : MonoBehaviour
{
    public Material lineMaterial;
    public GameObject pick;

    [System.Serializable]
    public class UnityStringEvent : UnityEvent<string> { }
    public UnityStringEvent onRecogized;

    public bool isMovement;
    private List<Vector3> pointsToLine;
    private Vector3 pickPosition;
    private GameObject newLine;
    private LineRenderer drawLine;


    private List<Gesture> trainingSet;

    int count;


    public static object locker, locker2;
    void Start()
    {

        //отключить перемещенние
        count =  22;

        locker = new object();
        locker2 = new object();
        trainingSet = new List<Gesture>();
        pointsToLine = new List<Vector3>();
        pickPosition = pick.GetComponent<Transform>().position;

        newLine = new GameObject();
        drawLine = newLine.AddComponent<LineRenderer>();

        drawLine.material = new Material(Shader.Find("Sprites/Default"));
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(new Color(0,0.7865f,1), 0.0f), new GradientColorKey(new Color(0.8f, 0.2f, 1), 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
        );
        drawLine.colorGradient = gradient;
        drawLine.startWidth = 0.01f;
        drawLine.endWidth = 0.01f;
        drawLine.positionCount = pointsToLine.Count;


        drawLine.SetPositions(pointsToLine.ToArray());


        isMovement = false;

        string[] gestureFiles = Directory.GetFiles(Application.persistentDataPath);
        foreach (var file in gestureFiles)
        {
            trainingSet.Add(GestureIO.ReadGestureFromFile(file));
        }
    }

    public void StartUsingWand() {
        isMovement = true;
    }
    public void EndUsingWand()
    {
        isMovement = false;
        pointsToLine.Clear();
        UpdatePosition();
    }

    public void UpdatePosition() {
        drawLine.positionCount = pointsToLine.Count;
        drawLine.SetPositions(pointsToLine.ToArray());
    }

    void Update()
    {
        if (isMovement)
        {
                pickPosition = pick.GetComponent<Transform>().position;
                if (pointsToLine.Count == 0)
                {
                    pointsToLine.Add(pickPosition);
                    UpdatePosition();
                }
                if (Vector3.Distance(pointsToLine[pointsToLine.Count - 1], pickPosition) >= 0.0015)
                {
                    pointsToLine.Add(pickPosition);
                    UpdatePosition();
                }
                
                else if (Vector3.Distance(pointsToLine[pointsToLine.Count - 1], pickPosition) < 0.0015 && Vector3.Distance(pointsToLine[pointsToLine.Count - 1], pickPosition) != 0)
                {

                    if (pointsToLine.Count > 30)
                    {
                        Point[] pointArray = new Point[pointsToLine.Count];
                        for (int i = 0; i < pointsToLine.Count; i++)
                        {
                            Vector2 screenPoint = Camera.main.WorldToScreenPoint(pointsToLine[i]);
                            pointArray[i] = new Point(screenPoint.x, screenPoint.y, 0);
                        }
                        Gesture newGesture = new Gesture(pointArray);

                        Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());
                        if (result.Score > 0.9)
                        {
                            onRecogized.Invoke(result.GestureClass);
                        }
                         
                        /*trainingSet.Add(newGesture);
                        string fileName = Application.persistentDataPath + $"/O_{++count}.xml";
                        Debug.Log(fileName);
                        GestureIO.WriteGesture(pointArray, $"O_{count}", fileName);
                        debugCube.text = $"Detected {count}";*/
                        pointsToLine.Clear();
                        UpdatePosition();
                    }
                }
        }
    }
}
