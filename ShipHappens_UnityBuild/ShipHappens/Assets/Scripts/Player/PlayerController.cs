﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public enum Direction { up, left, right };
    Direction direction;
    
    public PlayerStates playerState;
    private PlayerStates.PlayerState tempState;
    public Mop mop;
    public Wood wood;

    [Header("[Mapped Controls]")]
    public string Abutton = "A_P1";
    public string Bbutton = "B_P1";
    public string DpadHorizontal = "Dpad_Left/Right_P1";
    public string DpadVertical = "Dpad_Up/Down_P1";

    public GameManager gameManager;

    public bool edge = false;


    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerState = this.GetComponent<PlayerStates>();
    }

    private void Update()
    {
        DropItem();


/////////////////////////////////////////////////////////////////////////////////////////////////////////weak D-pad test
#region Dpad TEST
        if (Input.GetAxisRaw(DpadVertical) > 0)
        {
            Debug.Log("up dpad");
        }
        else if (Input.GetAxisRaw(DpadVertical) < 0)
        {
            Debug.Log("down dpad");
        }

        if (Input.GetAxisRaw(DpadHorizontal) > 0)
        {
            Debug.Log("right dpad");
        }
        else if (Input.GetAxisRaw(DpadHorizontal) < 0)
        {
            Debug.Log("left dpad");
        }
#endregion
    }

    private void OnTriggerStay(Collider col)
    {
        Debug.Log("Hit");
        Interactable other = col.gameObject.GetComponent<Interactable>();

        if (other != null)
        {          
            if (Input.GetKey(KeyCode.I) || Input.GetButtonDown(Abutton))
            {
                Debug.Log("Action button pressed");
                other.Action(this.gameObject);
            }
        }

        if (col.gameObject.tag == "poo")
        {
            GameObject poo = col.gameObject;
            if(playerState.playerState == PlayerStates.PlayerState.pMop)
            {
                if (Input.GetKey(KeyCode.I) || Input.GetButtonDown(Abutton))
                {
                    mop.Cleaning(poo);
                }

            }
        }

        if (col.gameObject.tag == "Hole")
        {
            Debug.Log("Hit a Hole!");
            GameObject hole = col.gameObject;
            if (playerState.playerState == PlayerStates.PlayerState.pWood)
            {
                if (Input.GetKey(KeyCode.I) || Input.GetButtonDown(Abutton))
                {
                    wood.RepairDeck(hole);
                }
            }
        }

        if (col.tag == "Edge")
        {
            edge = true;
        }

        //if (col.gameObject.tag == "HoldOn")
        //{
        //    if (Input.GetKey(KeyCode.I) || Input.GetButtonDown(Abutton))
        //    {
        //        Debug.Log("Action button pressed");
        //        other.Action(this.gameObject);
        //    }
        //}

        if (col.gameObject.tag == "ShipHold")
        {
            DpadMenu menu = col.gameObject.GetComponent<DpadMenu>();

            if (Input.GetAxisRaw(DpadVertical) > 0 || (Input.GetKey(KeyCode.K)) && gameManager.woodTimer.onCooldown == false && playerState.playerState == PlayerStates.PlayerState.pEmpty)
            {
                Debug.Log("DpadWood");

                direction = Direction.up;
                menu.CollectedWhenPressed(this, direction);
            }

            if (Input.GetAxisRaw(DpadHorizontal) > 0 || (Input.GetKey(KeyCode.L)) && gameManager.barrelTimer.onCooldown == false && playerState.playerState == PlayerStates.PlayerState.pEmpty)
            {
                direction = Direction.right;
                menu.CollectedWhenPressed(this, direction);

                Debug.Log("DpadBarrel");
            }

            if (Input.GetAxisRaw(DpadHorizontal) < 0 || (Input.GetKey(KeyCode.J)) && gameManager.cballTimer.onCooldown == false && playerState.playerState == PlayerStates.PlayerState.pEmpty)
            {
                direction = Direction.left;
                menu.CollectedWhenPressed(this, direction);

                Debug.Log("DpadCannonBall");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Edge")
        {
            edge = false;
        }
    }

    private void DropItem()
    {
        if(playerState.itemHeld == null)
        { return; }

        if (playerState.playerState == PlayerStates.PlayerState.pHoldingOn && Input.GetKey(KeyCode.U))
        {
            HunkerDown other = this.GetComponentInParent<HunkerDown>();

            if (other != null)
            {
                other.ReleaseMast(this.gameObject);
            }
        }

        if (playerState.itemHeld != null && Input.GetKey(KeyCode.U) || Input.GetButtonDown(Bbutton))
        {
            Interactable other = this.GetComponentInChildren<Interactable>();

            if (other != null)
            {
                other.DropItem();
            }
        }
    }
}
