using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnitchMoving : MonoBehaviour
{
    public GameObject[] positions;
    public GameObject list;
    public GameObject ear;
    private bool iscreateEar;

    private Coroutine coroutine;

    private int i;

    private void Start()
    {
        coroutine = StartCoroutine(Moving());
        iscreateEar = false;
    }

    private IEnumerator Moving()
    {
        int i = 1;
        while (true)
        {
            while (transform.position != positions[i].transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, positions[i].transform.position, 0.9f * Time.deltaTime);
                yield return null;
            }
            i = (i + 1) % 4;
            yield return new WaitForSeconds(5);
            transform.Rotate(new Vector3(0, 1, 0), 90f);
        }
    }

    public void stopMoving()
    {
        StopCoroutine(coroutine);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!iscreateEar)
        {
            Instantiate(ear, gameObject.transform.position, Quaternion.identity);
            list.GetComponent<listController>().addNewItem("ear");
            iscreateEar = true;
        }
    }
}
