﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodController : MonoBehaviour
{
    public GameObject floodPlane;
    private Vector3 startPosition = new Vector3(-3.863f, 4.0f, 3.35f);
    private Vector3 maxHeight = new Vector3(-3.863f, 12.25f, 3.35f);

    public static int numberOfHoles;
    public float floodRate;
    public float floodRateModifier;

    public float currentLevel;
    public float bailAmount;

    private void Start()
    {
        floodPlane.transform.position = startPosition;
    }

    void Update ()
    {
        floodRate = numberOfHoles * (floodRateModifier * 0.001f) * Time.deltaTime;

        floodPlane.transform.position = new Vector3(startPosition.x, startPosition.y += floodRate, startPosition.z);



        ClampFloodLevel();
    }


    public void BailWater()
    {
        //currentLevel = floodPlane.transform.position.y;
        //float newLevel = currentLevel - bailAmount;

        floodRate = floodRate - bailAmount;
    }

    void ClampFloodLevel()
    {
        //clamp min, max y-axis values
        if (floodPlane.transform.position.y <= 3.95f)
        {
            floodPlane.transform.position = startPosition;
        }
        if (floodPlane.transform.position.y > 12.2f)
        {
            floodPlane.transform.position = maxHeight;
        }
    }
}
 