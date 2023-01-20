using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golovolomka : MonoBehaviour
{
    private int sum;
    public ParticleSystem particleSystem;
    public GameObject[] glasses;
    public GameObject disk;

    private void Start()
    {
        sum = 0;
    }
    public void addSuccessfulItem()
    {
        sum++;
        Debug.Log(sum);
        checkSuccess();
    }
    public void removeSuccessfulItem()
    {
        sum--;
        Debug.Log(sum);
        checkSuccess();
    }
    void checkSuccess()
    {
        if (sum == 4)
        {
            foreach (GameObject item in glasses) Destroy(item);
            gameObject.GetComponent<Animator>().enabled = true;
            Instantiate(particleSystem, gameObject.transform.position, Quaternion.identity).Play();
            disk.GetComponent<Oculus.Interaction.HandPosing.HandGrabInteractable>().enabled = true;
        }
    }
}
