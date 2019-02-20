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
        floodController = floodWater.GetComponent<FloodController>();
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
        }

        // Collect Water
        if (bucketState.currentState == BucketStates.BucketState.Held && playerState.playerState == PlayerStates.PlayerState.pBucket)
        {
            Debug.Log("Collect Water");
            bucketState.currentState = BucketStates.BucketState.Full;
            // Play animations etc
        }

        // Bail Water
        if (bucketState.currentState == BucketStates.BucketState.Full && playerState.playerState == PlayerStates.PlayerState.pEdge)
        {
            Debug.Log("Bailed the water");
            bucketState.currentState = BucketStates.BucketState.Held;
            // Play animations etc
            floodController.BailWater();
        }
    }

    //private void Update()
    //{
    //    if (Input.GetButtonDown(Abutton) || Input.GetKey(KeyCode.B))
    //    {
    //        bucketPS.Play();
    //        float step = speed * Time.deltaTime;
    //        floodWater.transform.position = Vector3.MoveTowards(floodWater.transform.position, floodWaterStartPos.position, step);
    //    }
    //}

    public override void DropItem()
    {
        if (bucketState.currentState == BucketStates.BucketState.Held || bucketState.currentState == BucketStates.BucketState.Full)
        {
            this.transform.parent = null;
            bucketState.currentState = BucketStates.BucketState.Dropped;

            ResetComponents(ref playerState, rb);
        }
    }
}
