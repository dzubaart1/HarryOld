using Facebook.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateVoice : MonoBehaviour
{
    private Wit wit;
    public void StartRecognize()
    {
        wit = GetComponent<Wit>();
        if (!wit.Active) wit.Activate();
    }
}
