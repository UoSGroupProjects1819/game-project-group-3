using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCannonball : MonoBehaviour
{
    public GameObject holePrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ShipDeck")
        {
            //when hit deck swap model to hole
            Instantiate(holePrefab, transform.position, transform.rotation);
            holePrefab.GetComponent<HoleRadius>().holeStates = HoleRadius.HoleStates.Impact;
            DestroyImmediate(this.gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            //kill player, respawn player
        }
    }
}
