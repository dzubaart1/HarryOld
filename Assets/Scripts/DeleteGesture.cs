using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteGesture : MonoBehaviour
{
    public ParticleSystem particle;
    void Start()
    {
        StartCoroutine(enumerator());
    }

    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(10f);
        Instantiate(particle, gameObject.transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        Destroy(gameObject, 15f);
    }
}
