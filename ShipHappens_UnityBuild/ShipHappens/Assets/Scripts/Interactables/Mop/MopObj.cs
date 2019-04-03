using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopObj : InteractableObjs
{
    private MopStates mopStates;
    private PlayerStates playerStates;
    private PlayerController playerController;
    private Rigidbody rigid;

    public float timer;
    private const float CLEAN_TIMER = 2f;

    public Projector projector;

    private void Awake()
    {
        mopStates = GetComponent<MopStates>();
        rigid = GetComponent<Rigidbody>();
    }

    public override void Interact(GameObject player)
    {
        if(playerStates == null) { playerStates = player.GetComponent<PlayerStates>(); }
        if(playerController == null) { playerController = player.GetComponent<PlayerController>(); }

        if (mopStates.currentState == MopStates.MopState.Dropped && playerStates.playerState == PlayerStates.PlayerState.pEmpty)
        {
            SetPosition(ref player);
            mopStates.currentState = MopStates.MopState.Held;
            playerStates.playerState = PlayerStates.PlayerState.pMop;
            playerController.mop = this;
            SetPickedUpObjectComponents(ref playerStates, ref rigid, gameObject);
        }
    }

    private void Update()
    {
        if (mopStates.currentState == MopStates.MopState.Held) { projector.orthographicSize = 2.1f; projector = null; }

        if (mopStates.currentState == MopStates.MopState.Cleaning)
        {
            //Debug.Log("Wood Timer = " + timer);
            timer -= Time.deltaTime;

            float inverseLerp = Mathf.InverseLerp(CLEAN_TIMER, 0, timer);

            if (projector == null) { projector = playerController.transform.GetChild(2).transform.GetChild(1).GetComponent<Projector>(); }
            projector.orthographicSize = inverseLerp * 2.15f;

            if (timer <= 0)
            {
                playerController.cleaned = true;
            }
        }
        else
        {
            timer = CLEAN_TIMER;
        }
    }

    public override void DropItem()
    {
        if (mopStates.currentState == MopStates.MopState.Held)
        {
            transform.parent = null;
            playerController.mop = null;
            mopStates.currentState = MopStates.MopState.Dropped;
            ResetComponents(ref playerStates, ref rigid);
        }
    }

    public void CleanPoo(GameObject hazardToClean)
    {
        hazardToClean.SetActive(false);
    }
}
