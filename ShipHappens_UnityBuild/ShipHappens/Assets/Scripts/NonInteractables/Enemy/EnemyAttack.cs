using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject spawnArea;
    private Vector3 spawnAreaCoords;
    private Vector3 randomSpawnPos;

    public GameObject prefab;
    public GameObject cannonballPrefab;
    public Projector shrinkingProjector;

    public float shrinkProjectorStartSize = 8.33f;

    public bool goodSpawn = false;

    private void Start()
    {
        //Vector3 spawnAreaCoords = spawnArea.GetComponent<Renderer>().bounds.size;
        //Debug.Log("X: " + spawnAreaCoords.x);
        //Debug.Log("Y: " + spawnAreaCoords.y);
        //Debug.Log("Z: " + spawnAreaCoords.z);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            TestPosition();
        }

        //shrink projector as cannonball y-axis drops
        shrinkingProjector.orthographicSize = cannonballPrefab.transform.position.y - 8;

        //Mathf.Clamp(shrinkingProjector.orthographicSize, 0, 9);
        if (shrinkingProjector.orthographicSize > 8.33f)
            shrinkingProjector.orthographicSize = 8.33f;

        if (goodSpawn && cannonballPrefab.activeInHierarchy == false)
        {
            //Destroy(shrinkingProjector);
            //Destroy(this.gameObject);
            DestroyImmediate(shrinkingProjector);
            DestroyImmediate(this.gameObject);
        }
    }

    //void RandomSpawnPosition()
    //{
    //    randomSpawnPos.y = 65;
    //    randomSpawnPos.x = Random.Range(-27, 18);
    //    randomSpawnPos.z = Random.Range(-18, 36);
    //}

    void TestPosition()
    {
        while (!goodSpawn)
        {
            randomSpawnPos.x = Random.Range(-27, 18);
            randomSpawnPos.y = 65;
            randomSpawnPos.z = Random.Range(-18, 36);

            Debug.Log(randomSpawnPos.x);

            RaycastHit hit;
            Debug.DrawRay(randomSpawnPos, Vector3.up * -70, Color.white);
            if (Physics.Raycast(randomSpawnPos, Vector3.up * -1, out hit, 70))
            {
                if (hit.transform.tag == "ShipDeck")
                {
                    Vector3 raisedPos = randomSpawnPos;
                    raisedPos.y = 65f;
                    Instantiate(prefab, raisedPos, Quaternion.Euler(90, 0, 0));
                    Instantiate(shrinkingProjector, raisedPos, Quaternion.Euler(90, 0, 0));
                    Instantiate(cannonballPrefab, raisedPos, Quaternion.identity);
                    goodSpawn = true;
                }
            }
        }
    }
}
