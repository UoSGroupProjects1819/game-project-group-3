using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Interactable
{
    public PlayerStates playerState;
    public BucketStates bucketState;

    public GameObject bucket;

    public string Abutton = "A_P1";
    public string Bbutton = "B_P1";

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

    private void Update()
    {
        if (bucketState.currentState == BucketStates.BucketState.Held && rb.velocity.magnitude <= 0.1f)
        {
            if (Input.GetButtonDown(Abutton))
            {
                //floodLevel -= value * time.deltaTime;
            }
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
