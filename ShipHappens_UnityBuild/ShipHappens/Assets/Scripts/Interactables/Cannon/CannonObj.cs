﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonObj : InteractableObjs
{
    #region References
    // Cannon References
    private CannonState cannonState;
    private ParticleSystem cannonFire;

    // Player References
    private PlayerStates playerStates;
    private PlayerController playerController;

    // Misc References
    private Animator animator;
    private GameObject target;  // Enemy in range?
   
    // TIMER
    private string taskName;
    private const float CANNONBALL_TIMER = 0f, GUNPOWDER_TIMER = 0f;
    private const string CANNONBALL_TASK = "Cannonball", GUNPOWDER_TASK = "Gunpowder";
    [SerializeField] private float timer;
    #endregion

    void Start()
    {
        cannonState = GetComponent<CannonState>();
        cannonFire = GetComponentInChildren<ParticleSystem>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // If the enemy is in range of the cannon and it is fully loaded, indicate to the player they can fire
        if (cannonState.currentState == CannonState.CannonStates.cFullyLoaded && target != null)
        {
            animator.SetBool("InRange", true);
        }

        // If the cannon has been interacted with by the player holding a cannonball or gunpowder, begin the timer
        if(cannonState.currentState == CannonState.CannonStates.cPreLoaded)
        {
            Timer(taskName);
        }
    }

    public override void Interact(GameObject player)
    {
        // Performance safety checks
        if(playerStates == null) { playerStates = player.GetComponent<PlayerStates>(); }
        if(playerController == null) { playerController = player.GetComponent<PlayerController>(); }

        switch (playerStates.playerState)
        {
            case PlayerStates.PlayerState.pCannonball:
                // Check if a cannonball is already loaded, if a cannonball is present don't allow the player to place another in
                if(cannonState.currentState == CannonState.CannonStates.cCannonBall) { return; }

                // Assign the previous state in preperation for the timer
                if(cannonState.previousState == CannonState.CannonStates.cIdle) { cannonState.previousState = cannonState.currentState; }

                timer = CANNONBALL_TIMER;
                taskName = CANNONBALL_TASK;

                playerController.interacting = true;
                cannonState.currentState = CannonState.CannonStates.cPreLoaded;
                break;

            case PlayerStates.PlayerState.pGunpowder:
                // Check if gunpowder is already loaded, if gunpowder is present don't allow the player to place another in
                if (cannonState.currentState == CannonState.CannonStates.cGunpowder) { return; }

                // Assign the previous state in preperation for the timer
                if (cannonState.previousState == CannonState.CannonStates.cIdle) { cannonState.previousState = cannonState.currentState; }

                timer = GUNPOWDER_TIMER;
                taskName = GUNPOWDER_TASK;

                playerController.interacting = true;
                cannonState.currentState = CannonState.CannonStates.cPreLoaded;
                break;

            case PlayerStates.PlayerState.pTorch:
                // Destroy enemy
                if (cannonState.currentState == CannonState.CannonStates.cFullyLoaded && animator.GetBool("InRange") == true)
                {
                    cannonState.currentState = CannonState.CannonStates.cEmpty;
                    Destroy(target); // TODO NEEDS TO BE ADJUSTED FOR OBJECTPOOLING
                    target = null;
                    animator.SetBool("InRange", false);
                    cannonFire.Play();
                    break;
                }
                // Fire into the ocean 
                else if (cannonState.currentState == CannonState.CannonStates.cFullyLoaded)
                {
                    cannonState.currentState = CannonState.CannonStates.cEmpty;
                    cannonFire.Play();
                    break;
                }
                // Cannon isn't loaded
                break;

            default:
                // If the player is in any other state, just break out
                break;
        }
    }

    private void CompleteAction(PlayerStates player)
    {
        player.playerState = PlayerStates.PlayerState.pEmpty;
        GameObject interactable = player.GetComponentInChildren<InteractableObjs>().gameObject;
        Destroy(interactable); // TODO NEEDS TO BE ADJUSTED FOR OBJECTPOOLING
    }

    // Timer to load in the cannonball or gunpowder
    private void Timer(string task)
    {
        // Decrease the timer
        timer -= Time.deltaTime;

        // If the player interacts until the time runs 
        if (timer <= 0)
        {
            // Stop the player from interacting and remove the references to the player
            CompleteAction(playerStates);
            playerController.interacting = false;
            NullPlayer();

            // Reset the cannon states
            cannonState.currentState = cannonState.previousState;
            cannonState.previousState = CannonState.CannonStates.cIdle;

            // Depending on which task the player performed, update the cannon state
            switch (taskName)
            {
                case CANNONBALL_TASK:
                    cannonState.UpdateState(CannonState.CannonStates.cCannonBall);
                    break;
                case GUNPOWDER_TASK:
                    cannonState.UpdateState(CannonState.CannonStates.cGunpowder);
                    break;
            }

            // Empty out the taskName ready for the next timer
            taskName = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Update the cannon's target if the enemy is within range
        if (other.CompareTag("PirateFlag"))
        {
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the enemy is no longer in range, turn off the animation and empty out the target
        if(other.CompareTag("PirateFlag"))
        {
            animator.SetBool("InRange", false);
            target = null;
        }

        // If a player stops interacting, there should be a controller present
        if(playerController != null)
        {
            // If the player cancels the task before the timer runs out reset the components
            if (other.CompareTag("Player") && playerController.interacting == true)
            {
                cannonState.currentState = cannonState.previousState;
                cannonState.previousState = CannonState.CannonStates.cIdle;
                timer = 1000; // Set timer high so doesn't class as completed

                NullPlayer();
                taskName = null;
            }

        }
    }

    // Remove player references
    private void NullPlayer()
    {
        playerController = null;
        playerStates = null;
    }
}