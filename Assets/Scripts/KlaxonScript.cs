using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlaxonScript : MonoBehaviour
{

    public ParticleSystem particle;
    public void startAnimate()
    {
        Animator animator = GetComponent<Animator>();
        animator.enabled = true;
        GetComponent<AudioSource>().enabled = true;
        GetComponent<AudioSource>().Play();
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
