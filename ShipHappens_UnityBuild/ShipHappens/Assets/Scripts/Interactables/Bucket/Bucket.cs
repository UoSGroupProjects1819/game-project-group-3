using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Interactable
{
    public PlayerStates playerState;
    public BucketStates bucketState;

    public GameObject bucket;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        bucketState = this.GetComponent<BucketStates>();
        rb = this.GetComponent<Rigidbody>();
        bucket = this.gameObject;
	}

    public override void Action(GameObject player)
    {
        playerState = player.GetComponent<PlayerStates>();

        if (bucketState.currentState == BucketStates.BucketState.Dropped && playerState.playerState == PlayerStates.PlayerState.pEmpty)
        {
            SetPosition(ref player);
            bucketState.currentState = BucketStates.BucketState.Held;           
            playerState.playerState = PlayerStates.PlayerState.pBucket;
            PickedUpComponents(ref playerState, rb, this.gameObject);
        }
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
