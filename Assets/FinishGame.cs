using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    public GameObject fire;
    public GameObject door;
    public GameObject point;
    public GameObject s;
    public GameObject itemBox;
    public GameObject bunnyZone;
    private bool isOk, isOnce;

    private void Start()
    {
        isOk = false;
        isOnce = true;
    }
    public void ActiviatinmgEnd()
    {
        StartCoroutine(finish());
    }
    IEnumerator finish()
    {
        Debug.Log("Try Recognize");
        if (isOk && isOnce)
        {
            Debug.Log("You in a zone");
            yield return new WaitForSeconds(8);
            fire.SetActive(true);
            yield return new WaitForSeconds(2);
            itemBox.SetActive(false);
            yield return new WaitForSeconds(2);
            fire.SetActive(false);
            Instantiate(s, point.transform.position, Quaternion.identity);
            door.GetComponent<Oculus.Interaction.HandPosing.HandGrabInteractable>().enabled = true;
            isOnce = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "OVRCameraRig")
        {
            isOk = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "OVRCameraRig")
        {
            isOk = false;
        }
    }
}
