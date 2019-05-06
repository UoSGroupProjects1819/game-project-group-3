using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPooCheckRegion : MonoBehaviour
{
    public TutorialManager tutorialManager;
    public bool hasPood;


    private void Start()
    {
        hasPood = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "poo" && hasPood == false)
        {
            tutorialManager.stage++;
            hasPood = true;
        }
    }
}
