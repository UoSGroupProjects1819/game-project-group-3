using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateSpawner : Event
{
    public GameObject pirateFlag;
    public GameObject[] shipSpawners;
    public EnemyAttack enemyAttack;
    public CrowsNestUI CNui;



    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            Spawn();
        }
    }


    public override void Spawn()
    {
        CNui.nextAvailableBubbleContents = CNui.ImgEnemy;
        CNui.playNextAvailableBubble = true;


        int random = Random.Range(0, 4);

        Instantiate(pirateFlag, shipSpawners[random].transform.position, shipSpawners[random].transform.rotation);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PirateFlag")
        {
            enemyAttack.SpawnAttackPrefab();
            Destroy(other.gameObject);
        }
    }
}
