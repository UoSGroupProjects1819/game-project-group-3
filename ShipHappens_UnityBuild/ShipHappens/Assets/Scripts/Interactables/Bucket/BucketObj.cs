﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketObj : InteractableObjs
{
    #region References
    private PlayerStates playerStates;
    private BucketStates bucketStates;
    private PlayerController playerController;
    [SerializeField] private FloodController floodController;
    private Rigidbody rigid;

    [SerializeField] private float timer;
    private const float BAIL_TIMER = 5f;
    private bool bailing = false;

    private void Awake()
    {
        floodController = FindObjectOfType<FloodController>();
        bucketStates = GetComponent<BucketStates>();
        rigid = GetComponent<Rigidbody>();
        timer = BAIL_TIMER;
    }
    #endregion

    public override void Interact(GameObject player)
    {
        if (playerController == null) { playerController = player.GetComponent<PlayerController>(); }
        if (playerStates == null) { playerStates = player.GetComponent<PlayerStates>(); }

        // Pick up bucket
        if (bucketStates.currentState == BucketStates.BucketState.Dropped && playerStates.playerState == PlayerStates.PlayerState.pEmpty)
        {
            SetPosition(ref player);
            bucketStates.currentState = BucketStates.BucketState.Held;
            playerStates.playerState = PlayerStates.PlayerState.pBucket;
            SetPickedUpObjectComponents(ref playerStates, ref rigid, gameObject);
        }
    }

    private void Update()
    {
        if (bucketStates.currentState == BucketStates.BucketState.Bailing)
        {
            Debug.Log("Update");
            BailWater();
        }

        if(bucketStates.currentState == BucketStates.BucketState.Held)
        {
            timer = BAIL_TIMER;
        }
    }

    public override void DropItem()
    {
        if (bucketStates.currentState == BucketStates.BucketState.Held)
        {
            transform.parent = null;
            bucketStates.currentState = BucketStates.BucketState.Dropped;
            ResetComponents(ref playerStates, ref rigid);
        }
    }

    public void BailWater()
    {
        timer -= Time.deltaTime;
        Debug.Log("Bail Timer");
        if (timer <= 0)
        {
            floodController.BailWater();
            bucketStates.currentState = BucketStates.BucketState.Held;
            timer = BAIL_TIMER;
        }
    }
}
