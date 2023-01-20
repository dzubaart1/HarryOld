using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandCreating : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameObject;
    public GameObject pos;

    private bool isOne;
    private GameObject currentWand;

    private void Start()
    {
        isOne = false;
    }
    public void createWand()
    {
        if (!isOne)
        {
            currentWand = Instantiate(gameObject, pos.transform.position , Quaternion.identity);
            isOne = true;
        }
        else
        {
            Destroy(currentWand);
            currentWand = Instantiate(gameObject, pos.transform.position, Quaternion.identity);
            isOne = true;
        }
    }
}
