using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWorkActive : MonoBehaviour
{

    public bool isOk;
    private void Start()
    {
        isOk = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "OVRCameraRig")
        {
            isOk = true;
        }
    }
}
