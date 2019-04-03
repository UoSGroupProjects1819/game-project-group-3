using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Direction { up, left, right }
    Direction direction;

    public PlayerStates playerStates;
    public PlayerInput playerInput;
    public PlayerMovement playerMovement;

    private bool upIsPressed, leftIsPressed, rightIsPressed;
    private bool edge = false;

    private InteractableObjs interactable = null;
    private bool isInteracting = false;

    private void OnValidate()
    {
        playerStates = GetComponent<PlayerStates>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Action / Pick up Item
        if(playerInput.ButtonIsDown(PlayerInput.Button.A))
        {
            Action();
        }

        // Cancel / Drop Item
        if (playerInput.ButtonIsDown(PlayerInput.Button.B))
        {
            
        }
    }

    private void Action()
    {
        if (interactable != null)
        {
            interactable.Pickup(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        interactable = other.GetComponent<InteractableObjs>();
    }

    private void OnTriggerExit(Collider other)
    {
        interactable = null;
    }
}
