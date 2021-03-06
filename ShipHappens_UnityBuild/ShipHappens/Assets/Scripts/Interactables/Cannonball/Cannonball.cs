﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : Interactable
{
    CannonballStates cannonballState;
    public PlayerStates playerState;
    private Rigidbody rb;

    public static GameObject cannonballPrefab;
    public static GameObject gunpowderPrefab;

    private bool spawning = false;

    public void Spawn(GameObject player)
    {
        playerState = player.GetComponent<PlayerStates>();

        SetPosition(ref player);
        cannonballState.currentState = CannonballStates.CannonballState.Held;
        playerState.playerState = PlayerStates.PlayerState.pCannonball;
        PickedUpComponents(ref playerState, rb, this.gameObject);
    }

    private void Awake()
    {
        cannonballState = this.GetComponent<CannonballStates>();
        rb = this.GetComponent<Rigidbody>();
    }

    public override void Action(GameObject player)
    {
        playerState = player.GetComponent<PlayerStates>();

        if (cannonballState.currentState == CannonballStates.CannonballState.Dropped && playerState.playerState == PlayerStates.PlayerState.pEmpty)
        {
            SetPosition(ref player);
            cannonballState.currentState = CannonballStates.CannonballState.Held;
            playerState.playerState = PlayerStates.PlayerState.pCannonball;
            PickedUpComponents(ref playerState, rb, this.gameObject);
        }
    }

    public override void DropItem()
    {
        if (cannonballState.currentState == CannonballStates.CannonballState.Held)
        {
            this.transform.parent = null;
            cannonballState.currentState = CannonballStates.CannonballState.Dropped;

            ResetComponents(ref playerState, rb);
        }
    }

    public IEnumerator Timer()
    {
        float timer = 5;

        yield return timer;
    }
}
