using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSphere : MonoBehaviour
{
    public void activate()
    {
        gameObject.SetActive(true);
        StartCoroutine(animation());
    }
    IEnumerator animation()
    {
        Color c = gameObject.GetComponent<MeshRenderer>().material.color;
        while (c.a < 1)
        {
            c.a += 0.01f;
            gameObject.GetComponent<MeshRenderer>().material.color = c;
            yield return null;
        }
        yield return new WaitForSeconds(3);
        while (c.a > 0)
        {
            c.a -= 0.01f;
            gameObject.GetComponent<MeshRenderer>().material.color = c;
            yield return null;
        }
        gameObject.SetActive(false) ;
    }
}
