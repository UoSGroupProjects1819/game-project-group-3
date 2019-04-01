using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Interactable
{
    public PlayerStates playerState;
    public BucketStates bucketState;

    PlayerController playerController;

    public GameObject bucket;
    private Rigidbody rb;

    public string Abutton = "A_P1";
    public string Bbutton = "B_P1";

    public GameObject floodWater;
    private FloodController floodController;

    public ParticleSystem bucketPS;

    void Start()
    {
        floodController = FindObjectOfType<FloodController>();
        bucketState = this.GetComponent<BucketStates>();
        rb = this.GetComponent<Rigidbody>();
        bucket = this.gameObject;
    }

    public override void Action(GameObject player)
    {
        playerState = player.GetComponent<PlayerStates>();
        playerController = player.GetComponent<PlayerController>();

        // Pick Bucket Up
        if (bucketState.currentState == BucketStates.BucketState.Dropped && playerState.playerState == PlayerStates.PlayerState.pEmpty)
        {
            SetPosition(ref player);
            bucketState.currentState = BucketStates.BucketState.Held;
            playerState.playerState = PlayerStates.PlayerState.pBucket;
            PickedUpComponents(ref playerState, rb, this.gameObject);
            return;
        }
    }

    public void BailWater()
    {
        Debug.Log("Bailed the water");
        bucketState.currentState = BucketStates.BucketState.Held;
        floodController.BailWater();
    }


    public override void DropItem()
    {
        if (bucketState.currentState == BucketStates.BucketState.Held)
        {
            this.transform.parent = null;
            bucketState.currentState = BucketStates.BucketState.Dropped;

            ResetComponents(ref playerState, rb);
        }
    }
}
