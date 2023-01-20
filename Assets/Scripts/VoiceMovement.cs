using Facebook.WitAi.Lib;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VoiceMovement : MonoBehaviour
{
    public GameObject fire;
    public GameObject door;
    public GameObject point;
    public GameObject s;
    public GameObject itemBox;
    public TextMeshPro textMesh;
    public void MyRequire(string s)
    {
        textMesh.text = s;
        if (s.ToLower().Contains("всевозможные") && s.ToLower().Contains("волшебные") && s.ToLower().Contains("вредилки")) {
            StartCoroutine(finish());
        }
    }

    IEnumerator finish()
    {
        fire.SetActive(true);
        yield return new WaitForSeconds(2); 
        itemBox.SetActive(false);
        yield return new WaitForSeconds(2);
        fire.SetActive(false);
        Instantiate(s, point.transform.position, Quaternion.identity);
        door.GetComponent<Oculus.Interaction.HandPosing.HandGrabInteractable>().enabled = true;
    }

}
