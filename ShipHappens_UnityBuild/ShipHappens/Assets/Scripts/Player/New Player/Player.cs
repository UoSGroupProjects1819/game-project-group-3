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
    private bool isInteracting = false;

    private void OnValidate()
    {
        playerStates = GetComponent<PlayerStates>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerInput.ButtonIsDown(PlayerInput.Button.B))
        {
            Debug.Log("Player Dropped item!");
        }
    }
}
