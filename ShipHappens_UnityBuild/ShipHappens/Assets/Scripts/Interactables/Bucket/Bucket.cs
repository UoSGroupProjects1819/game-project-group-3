using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Interactable
{
    public PlayerStates playerState;
    public BucketStates bucketState;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        bucketState = this.GetComponent<BucketStates>();
        rb = this.GetComponent<Rigidbody>();
	}

    public override void Action(GameObject player)
    {
        if (bucketState.currentState == BucketStates.BucketState.Dropped && playerState.playerState == PlayerStates.PlayerState.pEmpty)
        {
            this.transform.parent = player.transform.GetChild(1).transform.GetChild(0);
            this.transform.localPosition = (this as Interactable).PickPosition;
            this.transform.localEulerAngles = (this as Interactable).PickRotation;

            bucketState.currentState = BucketStates.BucketState.Held;
            playerState = this.transform.GetComponentInParent<PlayerStates>();
            playerState.playerState = PlayerStates.PlayerState.pBucket;

            // Set values for item picked up
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
