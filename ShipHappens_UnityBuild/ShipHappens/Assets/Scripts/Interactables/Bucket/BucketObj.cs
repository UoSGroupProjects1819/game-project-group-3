using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketObj : InteractableObjs
{
    #region References
    private PlayerStates playerStates;
    private BucketStates bucketStates;
    private PlayerController playerController;
    [SerializeField] private FloodController floodController;
    private Rigidbody rigid;

    [SerializeField] private float timer;
    private const float BAIL_TIMER = 1f;
    private bool bailing = false;
    public Projector projector;

    public ParticleSystem PS_splash;

    private void Awake()
    {
        floodController = FindObjectOfType<FloodController>();
        bucketStates = GetComponent<BucketStates>();
        rigid = GetComponent<Rigidbody>();
        timer = BAIL_TIMER;
    }
    #endregion

    public override void Pickup(GameObject player, PlayerController pController = null, PlayerStates pStates = null)
    {
        if (playerController == null) { playerController = pController; }
        if (playerStates == null) { playerStates = pStates; }

        if (playerStates.playerState != PlayerStates.PlayerState.pEmpty)
            return; 

        // Pick up bucket
        if (bucketStates.currentState == BucketStates.BucketState.Dropped && playerStates.playerState == PlayerStates.PlayerState.pEmpty)
        {
            SetPosition(ref player);

            playerStates = pStates;

            playerStates.playerState = PlayerStates.PlayerState.pBucket;
            bucketStates.currentState = BucketStates.BucketState.Held;

            playerController = pController;
            playerController.currentObject = this;

            RotateShoulders(player.transform.GetChild(0).GetChild(0), 90);

            //projector = playerController.transform.GetChild(2).transform.GetChild(1).GetComponent<Projector>();
            SetPickedUpObjectComponents(ref playerStates, ref rigid, gameObject);
        }
    }

    public override void Activate(GameObject otherObject)
    {
        if (!otherObject.CompareTag(interactableTag) || bucketStates.currentState != BucketStates.BucketState.Held) return;

        bucketStates.currentState = BucketStates.BucketState.Bailing;
    }

    private void Update()
    {
        if (bucketStates.currentState == BucketStates.BucketState.Bailing)
        {
            BailWater();
        }

        if(bucketStates.currentState == BucketStates.BucketState.Held)
        {
            timer = BAIL_TIMER;
            projector.orthographicSize = 2.1f;
            projector = null;
        }
    }

    public override void DropItem()
    {
        if (bucketStates.currentState == BucketStates.BucketState.Held)
        {
            transform.parent = null;
            bucketStates.currentState = BucketStates.BucketState.Dropped;
            RotateShoulders(playerStates.transform.GetChild(0).GetChild(0), -90);
            ResetComponents(ref playerStates, ref rigid);
        }
    }

    public void BailWater()
    {
        if (floodController.currentPosition.y < floodController.waterOnDeck.y)
        {
            bucketStates.currentState = BucketStates.BucketState.Held;
            Debug.Log("No water to bail");
            return;
        }

        else if (floodController.currentPosition.y >= floodController.waterOnDeck.y)
        {
            timer -= Time.deltaTime;

            float inverseLerp = Mathf.InverseLerp(BAIL_TIMER, 0, timer);

            if (projector == null) { projector = playerController.transform.GetChild(2).transform.GetChild(1).GetComponent<Projector>(); }
            projector.orthographicSize = inverseLerp * 2.15f;

            Debug.Log("Bail Timer");
            if (timer <= 0)
            {
                PS_splash.Play();
                floodController.BailWater();
                bucketStates.currentState = BucketStates.BucketState.Held;
                timer = BAIL_TIMER;
                projector.orthographicSize = 2.1f;
                projector = null;
            }
        }
    }
}
