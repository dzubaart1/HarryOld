using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magiciance : MonoBehaviour
{
    public GameObject pick;
    public GameObject wand;
    public GameObject sphere;
    public GameObject DetectingTransfer;

    public Material lineMaterrial;
    public Material successMaterial;
    public Material waitingMaterial;

    public ParticleSystem particleSystem1;

    public AudioSource success;

    private GameObject rayReplace, rayBlust, rayOpen;
    private LineRenderer drawLine;
    private bool isReplaceing, isBlust, isOpen;

    private int countTime;

    private RaycastHit prevHit, targetHit;

    private GameObject light;

    private GameObject targetToOpen;

    private Gradient gradientFinding, gradientSuccess;

    private void Start()
    {
        gradientFinding = new Gradient();
        gradientFinding.SetKeys(
            new GradientColorKey[] { new GradientColorKey(new Color(0.8867f, 0.8240f, 0.8814f), 0.0f), new GradientColorKey(new Color(0.3231f, 0.2196f, 0.6431f), 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
        );

        gradientSuccess = new Gradient();
        gradientSuccess.SetKeys(
            new GradientColorKey[] { new GradientColorKey(new Color(0, 0.9245f, 0.3497f), 0.0f), new GradientColorKey(new Color(0.1298f, 0.3301f, 0.0732f), 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
        );
    }
    public void activateSpell(string s)
    {
        Debug.Log(s);
        switch (s.Split(' ')[0])
        {
            case "O":
                isOpen = false;
                rayOpen = new GameObject();
                drawLine = rayOpen.AddComponent<LineRenderer>();
                drawLine.material = new Material(Shader.Find("Sprites/Default"));
                drawLine.colorGradient = gradientFinding;
                drawLine.startWidth = 0.01f;
                drawLine.endWidth = 0.01f;
                drawLine.SetPosition(0, pick.transform.position);
                drawLine.SetPosition(1, pick.transform.right * 10);
                Debug.Log("O");
                DetectingTransfer.GetComponent<DetectingTransfer>().isMovement = false;
                if (rayOpen) Destroy(rayOpen, 5);
                success.Play();
                break;
            case "Infinity":
                isReplaceing = false;
                rayReplace = new GameObject();
                drawLine = rayReplace.AddComponent<LineRenderer>();
                drawLine.material = new Material(Shader.Find("Sprites/Default"));
                drawLine.colorGradient = gradientFinding;
                drawLine.startWidth = 0.01f;
                drawLine.endWidth = 0.01f;
                drawLine.SetPosition(0, pick.transform.position);
                drawLine.SetPosition(1, pick.transform.right * 10);
                Debug.Log("Infinity");
                DetectingTransfer.GetComponent<DetectingTransfer>().isMovement = false;
                if(rayReplace) Destroy(rayReplace, 5);
                success.Play();
                break;
            case "S":
                GameObject firework = wand.transform.Find("/firework").gameObject;
                GameObject isFire = wand.transform.Find("/isFire").gameObject;
                if (isFire.GetComponent<FireWorkActive>().isOk)
                {
                    for (int i = 0; i < firework.transform.childCount; i++)
                    {
                        firework.transform.GetChild(i).gameObject.GetComponent<ParticleSystem>().Play();
                    }
                    Debug.Log("S");
                    success.Play();
                }
                break;
            case "Square":
                if (!light)
                {
                    sphere.transform.position = pick.transform.localPosition;
                    light = Instantiate(sphere, wand.transform);
                    Destroy(light,5);
                    DetectingTransfer.GetComponent<DetectingTransfer>().isMovement = false;
                }
                success.Play();
                break;
            case "Triangle":
                isBlust = false;
                rayBlust = new GameObject();
                drawLine = rayBlust.AddComponent<LineRenderer>();
                drawLine.material = new Material(Shader.Find("Sprites/Default"));
                drawLine.colorGradient = gradientFinding;
                drawLine.startWidth = 0.01f;
                drawLine.endWidth = 0.01f;
                drawLine.SetPosition(0, pick.transform.position);
                drawLine.SetPosition(1, pick.transform.right * 10);
                Debug.Log("Triangle");
                DetectingTransfer.GetComponent<DetectingTransfer>().isMovement = false;
                if (rayBlust) Destroy(rayBlust, 5);
                success.Play();
                break;
        }
    }
    private void Update()
    {
        if (!light && !rayReplace && !rayBlust && !rayOpen)
        {
            DetectingTransfer.GetComponent<DetectingTransfer>().isMovement = true;
        }
        if (rayReplace)
        {
            RaycastHit hit;
            Ray ray = new Ray(pick.transform.position, pick.transform.right * 10);
            Physics.Raycast(ray, out hit, 1000);
            if (Vector3.Distance(pick.transform.position, drawLine.GetPosition(0)) > 0.0015)
            {
                if (hit.collider != null && hit.collider.tag == "Replaceable" && prevHit.collider.name == hit.collider.name)
                {
                    drawLine.colorGradient = gradientSuccess;
                    countTime += 1;
                    if (countTime > 10)
                    {
                        isReplaceing = true;
                        targetHit = hit;
                    }
                }
                else
                {
                    drawLine.colorGradient = gradientFinding;
                    countTime = 0;
                }
                drawLine.SetPosition(0, pick.transform.position);
                if(hit.collider != null) drawLine.SetPosition(1, hit.point);
                else drawLine.SetPosition(1, pick.transform.right * 10);
            }
            if (targetHit.collider)
            {
                if (isReplaceing)
                {
                    targetHit.collider.transform.position = Vector3.MoveTowards(targetHit.collider.transform.position, pick.transform.position, 8f * Time.deltaTime);
                    countTime = 0;
                }
                if (Vector3.Distance(targetHit.collider.transform.position, pick.transform.position) < 0.2)
                {
                    Destroy(rayReplace);
                    isReplaceing = false;
                    targetHit.collider.GetComponent<Rigidbody>().isKinematic = false;
                }
            }
            prevHit = hit;
        }
        if (rayBlust)
        {
            RaycastHit hit;
            Ray ray = new Ray(pick.transform.position, pick.transform.right * 10);
            Physics.Raycast(ray, out hit, 1000);
            if (Vector3.Distance(pick.transform.position, drawLine.GetPosition(0)) > 0.001)
            {
                if (hit.collider != null && prevHit.collider != null && prevHit.collider.name == hit.collider.name)
                {
                    drawLine.colorGradient = gradientSuccess;
                    countTime += 1;
                    if (countTime > 40)
                    {
                        isBlust = true;
                        targetHit = hit;
                    }
                }
                else
                {
                    drawLine.colorGradient = gradientFinding;
                    countTime = 0;
                }
                drawLine.SetPosition(0, pick.transform.position);
                if (hit.collider != null) drawLine.SetPosition(1, hit.point);
                else drawLine.SetPosition(1, pick.transform.right * 10);
            }
            if (isBlust)
            {
                ParticleSystem particles = Instantiate(particleSystem1, pick.transform.position,wand.transform.rotation);
                particles.Play();
                Destroy(rayBlust);
                countTime = 0;
                isBlust = false;
                if (hit.collider.name == "Snitch")
                {
                    hit.collider.gameObject.transform.GetChild(0).gameObject.AddComponent<Rigidbody>();
                    hit.collider.gameObject.transform.GetChild(1).gameObject.AddComponent<Rigidbody>();
                    hit.collider.gameObject.GetComponent<SnitchMoving>().stopMoving();
                }
            }
            prevHit = hit;
        }
        if (rayOpen)
        {
            RaycastHit hit;
            Ray ray = new Ray(pick.transform.position, pick.transform.right * 10);
            Physics.Raycast(ray, out hit, 1000);
            if (Vector3.Distance(pick.transform.position, drawLine.GetPosition(0)) > 0.0015)
            {
                if (hit.collider != null && hit.collider.tag == "Openable" && prevHit.collider.name == hit.collider.name)
                {
                    drawLine.colorGradient = gradientSuccess;
                    countTime += 1;
                    if (countTime > 10)
                    {
                        isOpen = true;
                        targetHit = hit;
                    }
                }
                else
                {
                    drawLine.colorGradient = gradientFinding;
                    countTime = 0;
                }
                drawLine.SetPosition(0, pick.transform.position);
                if (hit.collider != null) drawLine.SetPosition(1, hit.point);
                else drawLine.SetPosition(1, pick.transform.right * 10);
            }
            if (targetHit.collider)
            {
                if (isOpen)
                {
                    targetHit.collider.gameObject.GetComponent<Oculus.Interaction.HandPosing.HandGrabInteractable>().enabled = true;
                    countTime = 0;
                }
            }
            prevHit = hit;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Openable")
        {
            targetToOpen = other.gameObject;
            Debug.Log(targetToOpen.name);
        }
    }
}
