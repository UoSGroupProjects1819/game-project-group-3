﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSeagull : MonoBehaviour
{
    public GameObject shipCentre;
    public GameObject seagull;
    public GameObject pooPrefab;
    public float radiusRange;
    public float minRadius;
    public float maxRadius;
    public Vector3 spawnPosition;

    public GameObject floodPlane;

    public GameObject bottomRampLeft, bottomRampRight, topRampLeft, topRampRight;
    public GameObject leftSide, rightSide;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            SpawnGull();
        }

        CheckWaterOnDeck();
    }

    void SpawnGull()
    {
        spawnPosition = new Vector3(Random.Range(-radiusRange, radiusRange), shipCentre.transform.position.y, Random.Range(-radiusRange, radiusRange));
        float distance = Vector3.Distance(spawnPosition, shipCentre.transform.position);

        if (distance > minRadius && distance < maxRadius)
        {
            GameObject gull = Instantiate(seagull, spawnPosition, Quaternion.identity );
        }
        else
        {
            SpawnGull();
        }
    }

    void CheckWaterOnDeck()
    {
        if (floodPlane.transform.position.y > 10.5f)
        {
            //remove ramps
            bottomRampLeft.SetActive(false);
            bottomRampRight.SetActive(false);
            topRampLeft.SetActive(false);
            topRampRight.SetActive(false);
        }
        else
        {
            bottomRampLeft.SetActive(true);
            bottomRampRight.SetActive(true);
            topRampLeft.SetActive(true);
            topRampRight.SetActive(true);
        }

        if (floodPlane.transform.position.y > 8.5f)
        {
            //remove mids
            leftSide.SetActive(false);
            rightSide.SetActive(false);
        }
        else
        {
            //all active
            leftSide.SetActive(true);
            rightSide.SetActive(true);
        }
    }
}