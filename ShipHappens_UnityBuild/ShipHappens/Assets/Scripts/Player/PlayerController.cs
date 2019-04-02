using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public enum Direction { up, left, right };
    Direction direction;

    public PlayerStates playerState;
    private PlayerStates.PlayerState tempState;
    private PlayerInput playerInput;

    [HideInInspector] public MopObj mop;
    [HideInInspector] public WoodObj wood;
    [HideInInspector] public BucketStates bucketStates;
    public WoodStates woodStates;

    public bool upIsPressed;
    public bool leftIsPressed;
    public bool rightIsPressed;

    public CrowsNestUI UIManager;

    public bool edge = false;

    public bool interacting = false;
    public bool repaired = false;


    void Start()
    {
        UIManager = FindObjectOfType<CrowsNestUI>();
        playerState = this.GetComponent<PlayerStates>();
        bucketStates = FindObjectOfType<BucketStates>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (playerInput.ButtonIsDown(PlayerInput.Button.B))
        {
            DropItem();
        }

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
            if (Input.GetKeyUp(KeyCode.I) || playerInput.ButtonIsDown(PlayerInput.Button.A))
            {
                other.Interact(gameObject);
            }
        }

        if (col.gameObject.tag == "poo")
        {
            GameObject poo = col.gameObject;
            if (playerState.playerState == PlayerStates.PlayerState.pMop)
            {
                if (Input.GetKey(KeyCode.I) || playerInput.ButtonIsDown(PlayerInput.Button.A))
                {
                    mop.CleanPoo(poo);
                }
            }
        }

        if (col.gameObject.tag == "Hole")
        {
            Debug.Log("Hit a Hole!");
            GameObject hole = col.gameObject;
            if (playerState.playerState == PlayerStates.PlayerState.pWood)
            {
                if (Input.GetKey(KeyCode.I) || playerInput.ButtonIsDown(PlayerInput.Button.A))
                {
                    if (woodStates == null) { woodStates = wood.GetComponent<WoodStates>(); }
                    woodStates.currentState = WoodStates.WoodState.Repairing;
                }

                if(repaired == true)
                {
                    wood.RepairDeck(hole);
                }
            }

          
        }

        if (col.tag == "Edge" && playerState.playerState == PlayerStates.PlayerState.pBucket)
        {
            //playerState.playerState = PlayerStates.PlayerState.pEdge;

            if (playerInput.ButtonIsDown(PlayerInput.Button.A) && bucketStates.currentState == BucketStates.BucketState.Held)
            {
                Debug.Log("Begin Bailing");
                bucketStates.currentState = BucketStates.BucketState.Bailing;
            }
        }

        HunkerDown hunkerDown = col.gameObject.GetComponent<HunkerDown>();

        if (hunkerDown != null)
        {
            if (Input.GetKey(KeyCode.I) || playerInput.ButtonIsDown(PlayerInput.Button.A))
            {
                Debug.Log("Holding");
                hunkerDown.Interact(this.gameObject);
            }
        }

        {
            DpadMenu menu = col.gameObject.GetComponent<DpadMenu>();

            if (Input.GetAxisRaw(playerInput.GetVerticalDPad()) > 0 && upIsPressed == false|| (Input.GetKey(KeyCode.K)) && UIManager.woodTimer.onCooldown == false && playerState.playerState == PlayerStates.PlayerState.pEmpty)
            {
                upIsPressed = true;
                leftIsPressed = false;
                rightIsPressed = false;
                Debug.Log("DpadWood");

                direction = Direction.up;
                menu.CollectedWhenPressed(this, direction);
                return;
            }

            if (Input.GetAxisRaw(playerInput.GetHorizontalDPad()) > 0 && rightIsPressed == false|| (Input.GetKey(KeyCode.L)) && UIManager.barrelTimer.onCooldown == false && playerState.playerState == PlayerStates.PlayerState.pEmpty)
            {
                rightIsPressed = true;
                leftIsPressed = false;
                upIsPressed = false;
                direction = Direction.right;
                menu.CollectedWhenPressed(this, direction);

                Debug.Log("DpadBarrel");
                return;
            }

            if (Input.GetAxisRaw(playerInput.GetHorizontalDPad()) < 0 &&  leftIsPressed == false || (Input.GetKey(KeyCode.J)) && UIManager.cballTimer.onCooldown == false && playerState.playerState == PlayerStates.PlayerState.pEmpty)
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
        if (other.tag == "Edge" && playerState.playerState == PlayerStates.PlayerState.pBucket)
        {
            bucketStates.currentState = BucketStates.BucketState.Held;        
        }

        if(other.tag == "Hole" && playerState.playerState == PlayerStates.PlayerState.pWood)
        {
            woodStates.currentState = WoodStates.WoodState.Held;
        }
    }

    private void DropItem()
    {
        Debug.Log("Drop mehhhhh");
        if (playerState.itemHeld == null) { return; }

        if (playerState.playerState == PlayerStates.PlayerState.pHoldingOn && Input.GetKey(KeyCode.U))
        {
            HunkerDown other = this.GetComponentInParent<HunkerDown>();

            if (other != null)
            {
                other.ReleaseMast(this.gameObject);
            }
        }

        if (playerState.itemHeld != null)
        {
            InteractableObjs other = this.GetComponentInChildren<InteractableObjs>();

            if (playerState.itemHeld.name == "Mast")
            {
                HunkerDown hunker = FindObjectOfType<HunkerDown>();
                hunker.ReleaseMast(gameObject);
            }

            if (other != null)
            {
                Debug.Log("Drop meh");
                other.DropItem();
            }
        }
    }
}
