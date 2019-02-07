using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateSpawner : Event
{
    public GameObject pirateFlag;
    public GameObject[] shipSpawners;
    public EnemyAttack enemyAttack;
    public CrowsNestUI CNui;

    //public GameObject shipCentre;

    //public bool isBow = false;
    //public bool isStern = false;

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Spawn();
        }
    }


    public override void Spawn()
    {
        CNui.nextAvailableBubbleContents = CNui.ImgEnemy;
        CNui.playNextAvailableBubble = true;


        int random = Random.Range(0, 4);

        //if (shipSpawners[random].transform.position.z > shipCentre.transform.position.z) //+ is to rear
        //{
        //    isStern = true;
        //}
        //else //- is to front
        //{
        //    isBow = true;
        //}

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
