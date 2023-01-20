using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_chess : MonoBehaviour
{
    public GameObject item;
    public GameObject chessBox;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "tower_white_2")
        {
            gameObject.transform.parent.GetComponent<Animator>().enabled = true;
            other.gameObject.transform.rotation = Quaternion.identity;
            other.gameObject.GetComponent<Oculus.Interaction.HandPosing.HandGrabInteractable>().enabled = false;
            Debug.Log("Worked");
            StartCoroutine(openBox());
        }
    }

    IEnumerator openBox()
    {
        yield return new WaitForSeconds(2);
        chessBox.GetComponent<Animator>().enabled = true;
        item.GetComponent<Oculus.Interaction.HandPosing.HandGrabInteractable>().enabled = true;
    }
}
