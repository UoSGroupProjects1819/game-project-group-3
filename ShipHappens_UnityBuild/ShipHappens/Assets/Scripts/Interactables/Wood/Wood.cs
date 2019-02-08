using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Interactable
{
    WoodStates woodStates;
    public Transform holdingPoint;
    public PlayerStates playerState;
    public PlayerController playerController;

    private Rigidbody rb;



    // Use this for initialization
    void Awake ()
    {
        woodStates = this.GetComponent<WoodStates>();
        rb = this.GetComponent<Rigidbody>();
    }

    public void Spawn(GameObject player)
    {
        Debug.Log(woodStates);
        playerState = player.GetComponent<PlayerStates>();
        playerController = player.GetComponent<PlayerController>();

        SetPosition(ref player);
        woodStates.currentState = WoodStates.WoodState.Held;
        playerState.playerState = PlayerStates.PlayerState.pWood;
        playerController.wood = this;
        PickedUpComponents(ref playerState, rb, this.gameObject); 
    }

    public override void Action(GameObject player)
    {
        playerState = player.GetComponent<PlayerStates>();
        playerController = player.GetComponent<PlayerController>();

        if (woodStates.currentState == WoodStates.WoodState.Dropped && playerState.playerState == PlayerStates.PlayerState.pEmpty)
        {
            SetPosition(ref player);
            woodStates.currentState = WoodStates.WoodState.Held;
            playerState.playerState = PlayerStates.PlayerState.pWood;
            playerController.wood = this;
            PickedUpComponents(ref playerState, rb, this.gameObject);
        }
    }

    public override void DropItem()
    {
        if (woodStates.currentState == WoodStates.WoodState.Held)
        {
            this.transform.parent = null;
            playerController.wood = null;
            playerController = null;
            woodStates.currentState = WoodStates.WoodState.Dropped;
            ResetComponents(ref playerState, rb);
        }
    }

    public void RepairDeck(GameObject hole)
    {
        // PLAY ANIMATION

        Destroy(hole);
        Destroy(this.gameObject);
        playerState.playerState = PlayerStates.PlayerState.pEmpty;
        playerController.wood = null;
        playerState.itemHeld = null;
        playerController.wood = null;
        playerController = null;
    }

}
