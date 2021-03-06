﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSeagull : Event
{
    public MultiObjectPool objectPooler;

    public GameObject shipCentre;
    public GameObject seagull;
    public GameObject pooPrefab;
    public float radiusRange;
    public float minRadius;
    public float maxRadius;
    public Vector3 spawnPosition;

    public GameObject floodPlane;

    public GameObject bottomRampLeft, bottomRampRight, topRampLeft, topRampRight, midLsmall, midLsmallHigh, midRsmall, midRsmallHigh;
    public GameObject leftSide, rightSide;

    public CrowsNestUI CNui;

    public bool isTutorial = false;


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.V))
        {
            Spawn();
        }

        CheckWaterOnDeck();
    }

    public void TutorialSpawn()
    {
        spawnPosition = new Vector3(Random.Range(-radiusRange, radiusRange), shipCentre.transform.position.y, Random.Range(-radiusRange, radiusRange));
        float distance = Vector3.Distance(spawnPosition, shipCentre.transform.position);

        if (distance > minRadius && distance < maxRadius)
        {
            GameObject gull = objectPooler.SpawnFromPool("Seagull", spawnPosition, Quaternion.LookRotation(new Vector3(-spawnPosition.x, 0, -spawnPosition.z)));
            gull.GetComponent<Seagull>().seagullState = Seagull.SeagullStates.entering;
            //GameObject gull = Instantiate(seagull, spawnPosition, Quaternion.identity);
        }
        else
        {
            Spawn();
        }
    }

    public override void Spawn()
    {
        if (!isTutorial)
        {
            CNui.nextAvailableBubbleContents = CNui.ImgSeagull;
            CNui.playNextAvailableBubble = true;
        }
        
        spawnPosition = new Vector3(Random.Range(-radiusRange, radiusRange), shipCentre.transform.position.y, Random.Range(-radiusRange, radiusRange));
        float distance = Vector3.Distance(spawnPosition, shipCentre.transform.position);

        if (distance > minRadius && distance < maxRadius)
        {
            GameObject gull = objectPooler.SpawnFromPool("Seagull", spawnPosition, Quaternion.LookRotation(new Vector3(-spawnPosition.x, 0, -spawnPosition.z)));
            gull.GetComponent<Seagull>().seagullState = Seagull.SeagullStates.entering;
            //GameObject gull = Instantiate(seagull, spawnPosition, Quaternion.identity);
        }
        else
        {
            Spawn();
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
            midLsmall.SetActive(false);
            midLsmallHigh.SetActive(false);
            midRsmall.SetActive(false);
            midRsmallHigh.SetActive(false);
        }
        else
        {
            //all active
            leftSide.SetActive(true);
            rightSide.SetActive(true);
            midLsmall.SetActive(true);
            midLsmallHigh.SetActive(true);
            midRsmall.SetActive(true);
            midRsmallHigh.SetActive(true);
        }
    }
}