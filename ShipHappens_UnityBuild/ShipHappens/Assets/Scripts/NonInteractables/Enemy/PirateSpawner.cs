using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateSpawner : MonoBehaviour
{
    public GameObject pirateFlag;
    public GameObject[] shipSpawners;
    //public GameObject shipCentre;

    //public bool isBow = false;
    //public bool isStern = false;

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            SpawnPirate();
        }
    }


    void SpawnPirate()
    {
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
        Debug.Log("is colliding");
        if (other.gameObject.tag == "PirateFlag")
        {
            Debug.Log("is pirate");
            Destroy(other.gameObject);
        }
    }
}
