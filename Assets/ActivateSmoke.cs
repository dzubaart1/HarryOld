using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSmoke : MonoBehaviour
{

    public GameObject smoke;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "OVRCameraRig")
        {
            smoke.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "OVRCameraRig")
        {
            smoke.SetActive(false);
        }
    }
}
