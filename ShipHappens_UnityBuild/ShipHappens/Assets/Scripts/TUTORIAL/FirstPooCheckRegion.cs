using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPooCheckRegion : MonoBehaviour
{
    public TutorialManager tutorialManager;
    public bool notPood;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "poo" && notPood)
        {
            tutorialManager.stage++;
            notPood = false;
        }
    }
}
