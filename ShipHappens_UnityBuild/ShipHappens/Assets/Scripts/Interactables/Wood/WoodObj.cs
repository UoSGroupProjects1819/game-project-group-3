using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodObj : InteractableObjs
{
    private WoodStates woodStates;

    private PlayerStates playerStates;
    private PlayerController playerController;

    private Rigidbody rigid;

    private Projector projector;

    public GameObject hole;

    [SerializeField]
    public float timer;
    private const float REPAIR_TIMER = 5f;

    private void Awake()
    {
        woodStates = GetComponent<WoodStates>();
        rigid = GetComponent<Rigidbody>();
        timer = REPAIR_TIMER;
    }

    public void EnableWood(ref PlayerStates states, ref GameObject player)
    {
        SetPosition(ref player);

        states.playerState = PlayerStates.PlayerState.pWood;
        woodStates.currentState = WoodStates.WoodState.Held;
        playerStates = states;

        if(playerController == null) { playerController = player.GetComponent<PlayerController>(); }
        playerController.wood = this;
        playerController.woodStates = woodStates;

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
        playerController.woodStates = woodStates;

        SetPickedUpObjectComponents(ref playerStates, ref rigid, gameObject);
    }

    private void Update()
    {
        if (woodStates.currentState == WoodStates.WoodState.Repairing)
        {
            //Debug.Log("Wood Timer = " + timer);
            timer -= Time.deltaTime;

            float inverseLerp = Mathf.InverseLerp(REPAIR_TIMER, 0, timer);

            if (projector == null) { projector = playerController.transform.GetChild(2).transform.GetChild(1).GetComponent<Projector>(); }
            projector.orthographicSize = inverseLerp * 2.15f;

            if(timer <= 0)
            {
                playerController.repaired = true;
            }
        }
        else if(woodStates.currentState == WoodStates.WoodState.Held)
        {
            timer = REPAIR_TIMER;
        }
    }

    public override void DropItem()
    {
        if(woodStates.currentState == WoodStates.WoodState.Held)
        {
            transform.parent = null;
            playerController.wood = null;
            playerController.woodStates = null;
            playerController = null;
            woodStates.currentState = WoodStates.WoodState.Dropped;
            ResetComponents(ref playerStates, ref rigid);
        }
    }

    public void RepairDeck(GameObject hole)
    {
        Debug.Log("REPAIRRRRR MEHHH");
        hole.SetActive(false);
        gameObject.SetActive(false);
        playerStates.playerState = PlayerStates.PlayerState.pEmpty;
        playerController.wood = null;
        playerStates.itemHeld = null;
        playerController.repaired = false;
        playerController = null;
        hole = null;
        timer = REPAIR_TIMER;
    }

}
