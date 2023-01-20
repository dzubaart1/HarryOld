using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startAnimatePowder : MonoBehaviour
{

    public GameObject sphere;
    public void startAnimate()
    {
        Animator animator = GetComponent<Animator>();
        animator.enabled = true;
        sphere.GetComponent<ActivateSphere>().activate();
        Destroy(gameObject,2);
    }
}
