using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateSpawner : Event
{
    public MultiObjectPool objectPooler;

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
        Debug.Log("6666666666666");
        CNui.nextAvailableBubbleContents = CNui.ImgEnemy;
        CNui.playNextAvailableBubble = true;
        Debug.Log("77777777777777");


        int random = Random.Range(0, 4);

        //Instantiate(pirateFlag, shipSpawners[random].transform.position, shipSpawners[random].transform.rotation);
        objectPooler.SpawnFromPool("Enemy", shipSpawners[random].transform.position, shipSpawners[random].transform.rotation);
        Debug.Log("8888888888888");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PirateFlag")
        {
            Debug.Log("111111111");
            enemyAttack.SpawnAttackPrefab();
            Debug.Log("222222222222");
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
            Debug.Log("333333333333");
            EventManager.GetInstance().RemoveTask("Enemy");
            Debug.Log("4444444444444");
        }
    }
}
