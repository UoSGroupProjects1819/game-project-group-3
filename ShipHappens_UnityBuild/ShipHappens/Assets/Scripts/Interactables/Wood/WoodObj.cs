using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodObj : InteractableObjs
{
    private WoodStates woodStates;

    private PlayerStates playerStates;
    private PlayerController playerController;

    private Rigidbody rigid;

    [SerializeField]
    public float timer;
    private const float REPAIR_TIMER = 5f;

    private void Awake()
    {
        woodStates = GetComponent<WoodStates>();
        rigid = GetComponent<Rigidbody>();
    }

    public void EnableWood(ref PlayerStates states, ref GameObject player)
    {
        SetPosition(ref player);

        states.playerState = PlayerStates.PlayerState.pWood;
        woodStates.currentState = WoodStates.WoodState.Held;
        playerStates = states;

        if(playerController == null) { playerController = player.GetComponent<PlayerController>(); }
        playerController.wood = this;

        SetPickedUpObjectComponents(ref playerStates, ref rigid, gameObject);
    }

    public override void Interact(GameObject player)
    {
        SetPosition(ref player);

        if(playerStates == null) { playerStates = player.GetComponent<PlayerStates>(); }

        playerStates.playerState = PlayerStates.PlayerState.pWood;
        woodStates.currentState = WoodStates.WoodState.Held;

        if (playerController == null) { playerController = player.GetComponent<PlayerController>(); }
        playerController.wood = this;

        SetPickedUpObjectComponents(ref playerStates, ref rigid, gameObject);
    }

    private void Update()
    {
        if (woodStates.currentState == WoodStates.WoodState.Repairing)
        {
            timer -= Time.deltaTime;
        }
    }

    public override void DropItem()
    {
        if(woodStates.currentState == WoodStates.WoodState.Held)
        {
            transform.parent = null;
            playerController.wood = null;
            playerController = null;
            woodStates.currentState = WoodStates.WoodState.Dropped;
            ResetComponents(ref playerStates, ref rigid);
        }
    }

    public void RepairDeck(GameObject hole)
    {
            hole.SetActive(false);
            gameObject.SetActive(false);
            playerStates.playerState = PlayerStates.PlayerState.pEmpty;
            playerController.wood = null;
            playerStates.itemHeld = null;
            playerController = null;
    }
}
