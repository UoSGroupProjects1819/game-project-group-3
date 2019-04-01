using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IShouldPoo : MonoBehaviour
{
    public MultiObjectPool objectPooler;
    public GameObject seagull;
    public GameObject pooPrefab;
    public int countCheck = 0;

    private void OnTriggerEnter(Collider other)
    {
        countCheck++;

        if (other.tag == "PooRegion")
        {
            countCheck++;

            if (countCheck > 3 && countCheck < 5) //change count if range extended
            {
                objectPooler.SpawnFromPool("Poo", seagull.transform.position, seagull.transform.rotation);
                //Instantiate(pooPrefab, seagull.transform.position, seagull.transform.rotation);
            }
        }
    }

    private void OnDisable()
    {
        countCheck = 0;
    }
}
