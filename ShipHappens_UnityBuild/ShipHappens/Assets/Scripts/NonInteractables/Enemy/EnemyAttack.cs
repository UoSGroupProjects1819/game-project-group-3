using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public MultiObjectPool objectPooler;
    public GameObject spawnArea;
    private Vector3 spawnAreaCoords;
    public  Vector3 randomSpawnPos;

    public GameObject prefab;
    public GameObject cannonballPrefab;
    public Projector shrinkingProjector;

    public float shrinkProjectorStartSize = 8.33f;

    bool spawned = false;

    public void SpawnAttackPrefab()
    {
        do
        {
            if (TestPosition())
            {
                //Instantiate(prefab, randomSpawnPos, Quaternion.Euler(90, 0, 0));
                objectPooler.SpawnFromPool("EnemyAttack", randomSpawnPos, Quaternion.Euler(90, 0, 0));
                spawned = true;
            }
        } while (spawned == false);

        spawned = false;
    }

    bool TestPosition()
    {
            randomSpawnPos.x = Random.Range(-27, 18);
            randomSpawnPos.y = 65;
            randomSpawnPos.z = Random.Range(-15.5f, 36);

            RaycastHit hit;
            Debug.DrawRay(randomSpawnPos, Vector3.up * -70, Color.white);
            if (Physics.Raycast(randomSpawnPos, Vector3.up * -1, out hit, 70))
            {
                if (hit.transform.tag == "ShipDeck")
                {
                    return true;
                }
            }
            return false;
    }
}
