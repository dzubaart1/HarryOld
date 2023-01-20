using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_Box : MonoBehaviour
{
    public GameObject[] positions;
    public ParticleSystem particleSystem;
    public Color colorSuccess;
    private List<GameObject> list;

    private void Start()
    {
        /*number = gameObject.name.Split('_')[1];
        isBusy = false;*/
        list = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Glass"))
        {
            Debug.Log($"Вошел" + other.gameObject.name.Split('_')[1]);
            if (!list.Contains(other.gameObject))
            {
                other.gameObject.GetComponent<Oculus.Interaction.HandPosing.HandGrabInteractable>().enabled = false;
                other.gameObject.transform.position = positions[int.Parse(other.gameObject.name.Split('_')[1]) - 1].transform.position;
                other.transform.rotation = Quaternion.identity;
                list.Add(other.gameObject);
                if (gameObject.name.Contains(other.gameObject.name.Split("_")[1]))
                {
                    gameObject.transform.parent.GetComponent<Golovolomka>().addSuccessfulItem();
                    other.gameObject.GetComponent<Oculus.Interaction.HandPosing.HandGrabInteractable>().enabled = false;
                    other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    ParticleSystem particle = Instantiate(particleSystem, positions[int.Parse(other.gameObject.name.Split('_')[1]) - 1].transform.position, Quaternion.identity);
                    particle.startColor = colorSuccess;
                    particle.Play();
                    Debug.Log($"Добавил 1");
                }
                else other.gameObject.GetComponent<Oculus.Interaction.HandPosing.HandGrabInteractable>().enabled = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Glass")
        {
            Debug.Log($"Вышел" + other.gameObject.name.Split('_')[1]);
            if (gameObject.name.Contains(other.gameObject.name.Split("_")[1]))
            {
                gameObject.transform.parent.GetComponent<Golovolomka>().removeSuccessfulItem();
            }
            StartCoroutine(stop(other));
        }
    }

    IEnumerator stop(Collider other)
    {
        yield return new WaitForSeconds(2f);
        if (other && other.tag == "Glass")
        {
            list.Remove(other.gameObject);
        }
    }
}
