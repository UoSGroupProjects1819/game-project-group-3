using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Interactable
{

    public CannonState cannonState;
    public PlayerStates playerStates;
    public PlayerController controller;

    public ParticleSystem cannonFire;

    Animator anim;

    public bool finishedAction;

    public GameObject target;

    public cannonUI cannonUI;

    private string taskName;

    public const float CANNONBALL_TIMER = 5f, GUNPOWDER_TIMER = 5f;
    public float timer;

    public void Start()
    {
        cannonState = this.GetComponent<CannonState>();
        cannonFire = GetComponentInChildren<ParticleSystem>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (cannonState.currentState == CannonState.CannonStates.cFullyLoaded && target != null)
        {
            anim.SetBool("InRange", true);
        }

        if (cannonState.currentState == CannonState.CannonStates.cPreLoaded)
        {
            Timer(taskName);
        }
    }

    public override void Action(GameObject player)
    {
        // Safety Checks
        if (playerStates == null)
        {
            playerStates = player.GetComponent<PlayerStates>();
        }
        if (controller == null)
        {
            controller = player.GetComponent<PlayerController>();
        }

        // Get the interacting players current state
        switch (playerStates.playerState)
        {
                // Perform action if the player is holding the cannonball
            case PlayerStates.PlayerState.pCannonball:

                // Check if cannonball is already loaded
                if (cannonState.currentState == CannonState.CannonStates.cCannonBall)
                {
                    Debug.LogWarning("Cannonball already loaded");
                    return;
                }

                // Timer
                if (cannonState.previousState == CannonState.CannonStates.cIdle)
                {
                    cannonState.previousState = cannonState.currentState;
                }

                timer = CANNONBALL_TIMER;
                taskName = "Cannonball";

                controller.interacting = true;
                cannonState.currentState = CannonState.CannonStates.cPreLoaded;
                break;
                
                // Perform action if the player is holding the gunpowder
            case PlayerStates.PlayerState.pGunpowder:

                // Check if gunpowder is already loaded
                if (cannonState.currentState == CannonState.CannonStates.cGunpowder)
                {
                    Debug.LogWarning("Gunpowder already loaded");
                    return;
                }

                // Timer
                if (cannonState.previousState == CannonState.CannonStates.cIdle)
                {
                    cannonState.previousState = cannonState.currentState;   // Store the previous state of the cannon
                }
                timer = GUNPOWDER_TIMER;
                taskName = "Gunpowder";

                controller.interacting = true;
                cannonState.currentState = CannonState.CannonStates.cPreLoaded;
                break;

                // Perform action if the player is holding the torch
            case PlayerStates.PlayerState.pTorch:

                // Hits enemy
                if (cannonState.currentState == CannonState.CannonStates.cFullyLoaded && anim.GetBool("InRange") == true)
                {
                    cannonState.currentState = CannonState.CannonStates.cEmpty;
                    Destroy(target);
                    target = null;
                    anim.SetBool("InRange", false);
                    cannonFire.Play();
                }
                // Fires cannonball into the ocean
                else if (cannonState.currentState == CannonState.CannonStates.cFullyLoaded)
                {
                    cannonState.currentState = CannonState.CannonStates.cEmpty;
                    cannonFire.Play();
                }
                // Cannon isn't loaded
                else
                {
                    Debug.LogWarning("The cannon is NOT fully loaded");
                }
                break;
                
                // Perfrom actions if the player is empty
            case PlayerStates.PlayerState.pEmpty:

                Debug.LogWarning("Player isn't holding anything!");
                break;
            // If the player is in any other state then break out of the action
            default:

                Debug.LogWarning("That doesn't go in here!");
                break;
        }
    }

    /// <summary>
    /// Reset the player's current state and remove the object that is loaded into the cannon
    /// </summary>
    /// <param name="player"></param>
    private void OnAction(PlayerStates player)
    {
        player.playerState = PlayerStates.PlayerState.pEmpty;
        GameObject interactable = player.GetComponentInChildren<Interactable>().gameObject;
        Destroy(interactable);
    }

    private void Timer(string task)
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            OnAction(playerStates);
            controller.interacting = false;

            controller = null;
            playerStates = null;

            cannonState.currentState = cannonState.previousState;
            cannonState.previousState = CannonState.CannonStates.cIdle;

            if (task == "Cannonball")
            {
                cannonState.UpdateState(CannonState.CannonStates.cCannonBall);
            }
            else if (task == "Gunpowder")
            {
                cannonState.UpdateState(CannonState.CannonStates.cGunpowder);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PirateFlag")
        {
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PirateFlag")
        {
            anim.SetBool("InRange", false);
            target = null;
        }

        if (controller != null)
        {
            if (other.tag == "Player" && controller.interacting == true)
            {
                cannonState.currentState = cannonState.previousState;
                cannonState.previousState = CannonState.CannonStates.cIdle;
                timer = 1000;

                // Reset components - Make function
                controller = null;
                playerStates = null;
            }
        }
    }
}