using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingCannonball : MonoBehaviour
{
    public Transform testSpawnPosition;

    public GameObject cannonballPrefab;
    private GameObject belowBall;
    public Projector projectorPrefab;
    public GameObject holePrefab;

    public ParticleSystem hitPS;
       
    public bool isLanded;


    void Start()
    {

    }

    void FixedUpdate()
    {
        if (isLanded == false)
        {
            RaycastHit hit;
            Debug.DrawRay(cannonballPrefab.transform.position, cannonballPrefab.transform.up * -1, Color.white);
            if (Physics.Raycast(cannonballPrefab.transform.position, cannonballPrefab.transform.up * -1, out hit, 2, 1 << 29))
            {
                Debug.DrawRay(cannonballPrefab.transform.position, cannonballPrefab.transform.up, Color.red);
                cannonballPrefab.transform.up = -hit.normal;
                isLanded = true;
            }

            if (Physics.Raycast(cannonballPrefab.transform.position, cannonballPrefab.transform.up * -1, out hit, 1))
            {
                if (hit.transform.tag == "hole")
                {
                    //move particle system to object
                    Destroy(this.gameObject);
                    //play particle system
                }
            }
        }
    }

    void DropCannonball()
    {
        //pick random point
        Instantiate(cannonballPrefab, testSpawnPosition.position, testSpawnPosition.rotation);

        //shrink projector as cannonball y-axis drops
        projectorPrefab.orthographicSize = cannonballPrefab.transform.position.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 upsideDown = new Vector3 (this.transform.rotation.x - 180, this.transform.rotation.y, this.transform.rotation.z);
        Instantiate(holePrefab, this.transform.position, Quaternion.Euler(upsideDown));
        Destroy(this.gameObject);
    }
}
