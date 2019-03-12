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
    public MopObj mop;
    public Wood wood;
    public BucketStates bucketStates;

    [Header("[Mapped Controls]")]
    public string Abutton = "A_P1";
    public string Bbutton = "B_P1";
    public string DpadHorizontal = "Dpad_Left/Right_P1";
    public string DpadVertical = "Dpad_Up/Down_P1";

    public bool upIsPressed;
    public bool leftIsPressed;
    public bool rightIsPressed;

    public CrowsNestUI UIManager;

    public bool edge = false;

    public bool interacting = false;


    void Start()
    {
        UIManager = FindObjectOfType<CrowsNestUI>();
        playerState = this.GetComponent<PlayerStates>();
        bucketStates = FindObjectOfType<BucketStates>();
    }

    private void Update()
    {
        DropItem();

        //weak D-pad test
        #region Dpad TEST
        //if (Input.GetAxisRaw(DpadVertical) > 0 && !upIsPressed)
        //{
        //    upIsPressed = true;
        //    Debug.Log("up dpad");
        //}
        //else if (Input.GetAxisRaw(DpadVertical) < 0)
        //{
        //    Debug.Log("down dpad");
        //}

        //if (Input.GetAxisRaw(DpadHorizontal) > 0 && !rightIsPressed)
        //{
        //    rightIsPressed = true;
        //    Debug.Log("right dpad");
        //}
        //else if (Input.GetAxisRaw(DpadHorizontal) < 0 && !leftIsPressed)
        //{
        //    leftIsPressed = true;
        //    Debug.Log("left dpad");
        //}
        #endregion
    }

    private void OnTriggerStay(Collider col)
    {
        InteractableObjs other = col.gameObject.GetComponent<InteractableObjs>();

        if (other != null)
        {
            if (Input.GetKeyUp(KeyCode.I) || Input.GetButtonUp(Abutton))
            {
                other.Interact(gameObject);
            }
        }

        if (col.gameObject.tag == "poo")
        {
            GameObject poo = col.gameObject;
            if (playerState.playerState == PlayerStates.PlayerState.pMop)
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

        if (col.tag == "Edge" && playerState.playerState == PlayerStates.PlayerState.pBucket)
        {
            Bucket bucket = GetComponentInChildren<Bucket>();

            if (Input.GetKeyUp(KeyCode.I) || Input.GetButtonDown(Abutton) && bucketStates.currentState == BucketStates.BucketState.Full)
            {
                bucket.BailWater();
            }
        }

        HunkerDown hunkerDown = col.gameObject.GetComponent<HunkerDown>();

        if (hunkerDown != null)
        {
            if (Input.GetKey(KeyCode.I) || Input.GetButtonDown(Abutton))
            {
                Debug.Log("Holding");
                hunkerDown.Action(this.gameObject);
            }
        }

        {
            DpadMenu menu = col.gameObject.GetComponent<DpadMenu>();

            if (Input.GetAxisRaw(DpadVertical) > 0 && upIsPressed == false|| (Input.GetKey(KeyCode.K)) && UIManager.woodTimer.onCooldown == false && playerState.playerState == PlayerStates.PlayerState.pEmpty)
            {
                upIsPressed = true;
                leftIsPressed = false;
                rightIsPressed = false;
                Debug.Log("DpadWood");

                direction = Direction.up;
                menu.CollectedWhenPressed(this, direction);
                return;
            }

            if (Input.GetAxisRaw(DpadHorizontal) > 0 && rightIsPressed == false|| (Input.GetKey(KeyCode.L)) && UIManager.barrelTimer.onCooldown == false && playerState.playerState == PlayerStates.PlayerState.pEmpty)
            {
                rightIsPressed = true;
                leftIsPressed = false;
                upIsPressed = false;
                direction = Direction.right;
                menu.CollectedWhenPressed(this, direction);

                Debug.Log("DpadBarrel");
                return;
            }

            if (Input.GetAxisRaw(DpadHorizontal) < 0 &&  leftIsPressed == false || (Input.GetKey(KeyCode.J)) && UIManager.cballTimer.onCooldown == false && playerState.playerState == PlayerStates.PlayerState.pEmpty)
            {
                leftIsPressed = true;
                rightIsPressed = false;
                upIsPressed = false;
                direction = Direction.left;
                menu.CollectedWhenPressed(this, direction);

                Debug.Log("DpadCannonBall");
                return;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Edge" && playerState.playerState == PlayerStates.PlayerState.pEdge)
        {
            playerState.playerState = PlayerStates.PlayerState.pBucket;
        }
    }

    private void DropItem()
    {
        if (playerState.itemHeld == null) { return; }

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
            InteractableObjs other = this.GetComponentInChildren<InteractableObjs>();

            if (other != null)
            {
                other.DropItem();
            }
        }
    }
}
