using System.Collections;
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

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            SpawnGull();
        }
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
}