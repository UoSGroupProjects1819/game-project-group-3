﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodObj : InteractableObjs
{
    private WoodStates woodStates;

    private PlayerStates playerStates;
    private PlayerController playerController;

    private Rigidbody rigid;

    public Projector projector;

    public GameObject hole;

    [SerializeField]
    public float timer;
    private const float REPAIR_TIMER = 4f;

    private void Awake()
    {
        woodStates = GetComponent<WoodStates>();
        rigid = GetComponent<Rigidbody>();
        timer = REPAIR_TIMER;
    }

    public override void Activate(GameObject otherObject)
    {
        if (!otherObject.CompareTag(interactableTag)) return;

        hole = otherObject;
        woodStates.currentState = WoodStates.WoodState.Repairing;
    }

    public override void Deactivate()
    {
        woodStates.currentState = WoodStates.WoodState.Held;
        timer = REPAIR_TIMER;
        projector.orthographicSize = 2.1f;
    }

    public override void Pickup(GameObject player, PlayerController pController = null, PlayerStates pStates = null)
    {
        playerStates = pStates;

        if (playerStates.playerState != PlayerStates.PlayerState.pEmpty)
            return;

        SetPosition(ref player);

        playerStates.playerState = PlayerStates.PlayerState.pWood;
        woodStates.currentState = WoodStates.WoodState.Held;

        playerController = pController;
        playerController.currentObject = this;

        projector = playerController.transform.GetChild(2).transform.GetChild(1).GetComponent<Projector>();
        SetPickedUpObjectComponents(ref playerStates, ref rigid, gameObject);
    }

    private void Update()
    {

        if (woodStates.currentState == WoodStates.WoodState.Repairing)
        {
            //Debug.Log("Wood Timer = " + timer);
            timer -= Time.deltaTime;

            float inverseLerp = Mathf.InverseLerp(REPAIR_TIMER, 0, timer);
            projector.orthographicSize = inverseLerp * 2.15f;

            if (timer <= 0)
            {
                CompleteRepair();
            }
        }
    }

    public override void DropItem()
    {
        if(woodStates.currentState == WoodStates.WoodState.Held)
        {
            transform.parent = null;
            woodStates.currentState = WoodStates.WoodState.Dropped;
            ResetComponents(ref playerStates, ref rigid, playerStates.transform.GetChild(0).GetChild(0), playerController);
        }
    }

    private void CompleteRepair()
    {
        //hole.SetActive(false);
        HoleRadius radius = hole.GetComponent<HoleRadius>();

        radius.holeStates = HoleRadius.HoleStates.Repaired;

        transform.parent = null;
        playerStates.itemHeld = null;

        playerStates.playerState = PlayerStates.PlayerState.pEmpty;
        woodStates.currentState = WoodStates.WoodState.Dropped;
        playerController.currentObject = null;
        gameObject.SetActive(false);
    }

}
