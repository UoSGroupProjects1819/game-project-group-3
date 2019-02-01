using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject cannonballPrefab;
    public Projector projectorPrefab;
    public Projector shrinkingProjector;
    public GameObject holePrefab;


    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            SpawnCannonball();
        }

        //shrink projector as cannonball y-axis drops
        shrinkingProjector.orthographicSize = cannonballPrefab.transform.position.y - 8;
    }

    private void FixedUpdate()
    {
        //
    }

    void SpawnCannonball()
    {
        Vector3 randomPos; //pick random point

        //raycast down from randomPos
        //if hit ship deck, instantiate cannonball
        //otherwise reposition (either use one large rect or legit random)

        //spawn prefab
        //Instantiate(cannonballPrefab, randomPos, Quaternion.identity);

        //once hole has replaced cannonball on impact, give hole a sphere trigger collider that will set it to flip if cannonball enters trigger
        //...maybe do this on the repaired holes.
    }

    private void OnCollisionEnter(Collision collision)
    {
        //when hit deck swap model to hole
        Instantiate(holePrefab, cannonballPrefab.transform.position, cannonballPrefab.transform.rotation);
        Destroy(projectorPrefab);
        //increment static hole count
    }
}
