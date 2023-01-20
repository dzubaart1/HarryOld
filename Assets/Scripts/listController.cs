using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listController : MonoBehaviour
{
    public List<GameObject> list;
    public GameObject itembox;
    public GameObject tips;
    public GameObject pose;
    private int sum;
    private void Start()
    {
        sum = 0;
    }

    public void addNewItem(string s)
    {
        foreach (GameObject go in list)
        { 
            if (go.name.ToLower() == s)
            {
                go.SetActive(true);
                calcualteSum();
                break;
            }
        }
    }
    private void calcualteSum()
    {
        sum = 0;
        foreach (GameObject go in list)
        {
            if (go.activeSelf) sum++;
        }
        Debug.Log("Sum " + sum);
        CheckEnd();
    }

    private void CheckEnd()
    {
        if (sum == list.Count)
        {
            itembox.SetActive(true);
            pose.SetActive(true);
            tips.SetActive(true);
        }
    }
}
