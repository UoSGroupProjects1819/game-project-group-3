using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCannonball : MonoBehaviour
{
    public MultiObjectPool objectPooler;
    public GameObject holePrefab;

    private void Awake()
    {
        objectPooler = FindObjectOfType<MultiObjectPool>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ShipDeck")
        {
            //when hit deck swap model to hole
            //Instantiate(holePrefab, transform.position, transform.rotation);
            objectPooler.SpawnFromPool("Holes", transform.position, transform.rotation);
            holePrefab.GetComponent<HoleRadius>().holeStates = HoleRadius.HoleStates.Impact;
            //Destroy(this.transform.parent.gameObject);
            this.transform.parent.gameObject.SetActive(false);
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Player")
        {
            PlayerStates playerStates = collision.gameObject.GetComponent<PlayerStates>();
            playerStates.itemHeld.SetActive(false);
            playerStates.itemHeld = null;
            playerStates.playerState = PlayerStates.PlayerState.pDead;
        }
    }
}
