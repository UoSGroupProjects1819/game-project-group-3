using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObjects : MonoBehaviour
{
    public GameObject world;

    public GameObject thisObject;
    public Renderer thisRenderer;

    public Color originalColour;
    public Color warningColour;
    public Color brokenColour;

    public Text timerText;
    public float countdown;
    public float lowRand;
    public float highRand;

    public bool isFine;
    public bool isBreaking;
    public bool isBroken;


    void Start()
    {
        countdown = Random.Range(lowRand, highRand);
        originalColour = thisObject.GetComponent<Renderer>().material.color;
        thisRenderer = gameObject.GetComponent<Renderer>();
    }


    void Update()
    {
        countdown -= Time.deltaTime;

        if (isFine)
        {
            thisRenderer.material.color = originalColour;
            countdown = 22;
            timerText.text = "";
        }

        if (countdown < 10)
        {
            isFine = false;
            isBreaking = true;
        }

        if (isBreaking && countdown > 0)
        {
            thisRenderer.material.color = warningColour;
            timerText.text = "" + Mathf.Round(countdown);
        }

        if (countdown <= 3)
        {
            isBreaking = false;
            timerText.text = Mathf.Round(countdown) + "!";
            thisRenderer.material.color = warningColour;

            if (countdown < 0)
            {
                isBreaking = false;
                isBroken = true;
            }
        }

        if (isBroken)
        {
            thisRenderer.material.color = brokenColour;
            timerText.text = "!!!";
            world.GetComponent<WorldManager>().badness += Time.deltaTime;
        }
    }
}
