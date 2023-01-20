using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessGrab : MonoBehaviour
{
    public GameObject grab;

    public void activateObject()
    {
        grab.GetComponent<Oculus.Interaction.HandPosing.HandGrabInteractable>().enabled = true;
    }
}
