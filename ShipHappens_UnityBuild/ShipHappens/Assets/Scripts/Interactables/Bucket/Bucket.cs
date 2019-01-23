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
    private Transform floodWaterStartPos;
    public float speed;
    public ParticleSystem bucketPS;



    void Start()
    {
        //floodWaterStartPos = floodWater.transform;
        bucketState = this.GetComponent<BucketStates>();
        rb = this.GetComponent<Rigidbody>();
        bucket = this.gameObject;
    }

    public override void Action(GameObject player)
    {
        playerState = player.GetComponent<PlayerStates>();
        playerController = player.GetComponent<PlayerController>();

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

        }

        // Bail Water
        if (bucketState.currentState == BucketStates.BucketState.Full && )
        {

        }
    }

    //private void OnTriggerStay(Collider col)
    //{
    //    if (col.gameObject.tag == "Edge" && bucketState.currentState == BucketStates.BucketState.Held && rb.velocity.magnitude <= 0.1f && floodWater.transform.position.y > floodWaterStartPos.position.y)
    //    {
    //        Debug.Log("bail 1st check");

    //        if (Input.GetButtonDown(Abutton) || Input.GetKey(KeyCode.B))
    //        {
    //            Debug.Log("bailing!");
    //            bucketPS.Play();
    //            float step = speed * Time.deltaTime;
    //            floodWater.transform.position = Vector3.MoveTowards(floodWater.transform.position, floodWaterStartPos.position, step);
    //        }
    //    }
    //}

    private void Update()
    {
        if (Input.GetButtonDown(Abutton) || Input.GetKey(KeyCode.B))
        {
            bucketPS.Play();
            float step = speed * Time.deltaTime;
            floodWater.transform.position = Vector3.MoveTowards(floodWater.transform.position, floodWaterStartPos.position, step);
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
