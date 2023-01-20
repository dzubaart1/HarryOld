using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObject : MonoBehaviour
{

    public ParticleSystem particle;
    // Start is called before the first frame update
    void delete()
    {
        StartCoroutine(enumerator());
    }

    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(10f);
        Instantiate(particle, gameObject.transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        Destroy(gameObject, 5f);
    }
}
