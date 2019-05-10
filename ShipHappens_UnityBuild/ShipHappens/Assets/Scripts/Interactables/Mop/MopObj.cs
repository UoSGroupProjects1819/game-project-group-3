using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopObj : InteractableObjs
{
    private MopStates mopStates;
    private PlayerStates playerStates;
    private PlayerController playerController;
    private Rigidbody rigid;

    private GameObject hazard;

    public float timer;
    private const float CLEAN_TIMER = 2f;

    public Projector projector;

    private void Awake()
    {
        mopStates = GetComponent<MopStates>();
        rigid = GetComponent<Rigidbody>();
        timer = CLEAN_TIMER;
    }

    public override void Deactivate()
    {
        Debug.Log("Deactivated poo");
        mopStates.currentState = MopStates.MopState.Held;
    }

    public override void Activate(GameObject otherObject)
    {
        if (!otherObject.CompareTag(interactableTag) || mopStates.currentState != MopStates.MopState.Held) return;

        mopStates.currentState = MopStates.MopState.Cleaning;

        hazard = otherObject;
    }

    public override void Pickup(GameObject player, PlayerController pController = null, PlayerStates pStates = null)
    {
        playerStates = pStates;

        if (playerStates.playerState != PlayerStates.PlayerState.pEmpty)
            return;

        SetPosition(ref player);

        playerStates.playerState = PlayerStates.PlayerState.pMop;
        mopStates.currentState = MopStates.MopState.Held;

        playerController = pController;
        playerController.currentObject = this;

        SetPickedUpObjectComponents(ref playerStates, ref rigid, gameObject);
        RotateShoulders(player.transform.GetChild(0).GetChild(0), playerStates);
        projector = playerController.transform.GetChild(2).transform.GetChild(1).GetComponent<Projector>();
        
    }

    private void Update()
    {
            if (mopStates.currentState == MopStates.MopState.Cleaning)
            {
                //Debug.Log("Wood Timer = " + timer);
                timer -= Time.deltaTime;

                float inverseLerp = Mathf.InverseLerp(CLEAN_TIMER, 0, timer);
                projector.orthographicSize = inverseLerp * 2.15f;

                if (timer <= 0)
                {
                    CleanPoo();
                }
            }
            else if (timer != CLEAN_TIMER)
        {
            ResetValues();
        }
    }

    // Reset the values when not repairing
    private void ResetValues()
    {
        timer = CLEAN_TIMER;
        projector.orthographicSize = 2.1f;
    }

    public override void DropItem()
    {
        //if (mopStates.currentState == MopStates.MopState.Held)
        //{
            transform.parent = null;
            playerController.mop = null;
            mopStates.currentState = MopStates.MopState.Dropped;

            ResetComponents(ref playerStates, ref rigid, playerStates.transform.GetChild(0).GetChild(0), playerController);
        //}
    }

    private void CleanPoo()
    {
        hazard.SetActive(false);
        mopStates.currentState = MopStates.MopState.Held;
    }
}
