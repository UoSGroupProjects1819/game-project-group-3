using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public enum Direction { up, left, right };
    Direction direction;

    public PlayerStates playerState;
    private PlayerInput playerInput;

    public InteractableObjs currentObject;

    [HideInInspector] public MopObj mop;
    [HideInInspector] public WoodObj wood;
    [HideInInspector] public BucketStates bucketStates;
    public WoodStates woodStates;
    public MopStates mopStates;

    public InteractableObjs touchedInteractable;
    public GameObject touchedGameObject;

    public bool upIsPressed;
    public bool leftIsPressed;
    public bool rightIsPressed;

    public DpadMenu dPadMenu;

    public bool edge = false;

    public bool interacting = false;


    void Start()
    {
        dPadMenu = FindObjectOfType<DpadMenu>();
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

        if (Input.GetKey(KeyCode.I) || playerInput.ButtonIsDown(PlayerInput.Button.A))
        { 
            if ( touchedInteractable != null && playerState.playerState == PlayerStates.PlayerState.pEmpty)
            {
                touchedInteractable.Pickup(gameObject, this, this.playerState);
                return;
            }

            if(playerState.playerState == PlayerStates.PlayerState.pBucket && touchedGameObject.CompareTag("Edge"))
            {
                if (bucketStates.currentState == BucketStates.BucketState.Held)
                    bucketStates.currentState = BucketStates.BucketState.Bailing;
            }

            if ( currentObject && touchedGameObject)
            {
                currentObject.Activate(touchedGameObject);
            }

            if (touchedGameObject)
            {
                HunkerDown hunkerDown = touchedGameObject.gameObject.GetComponent<HunkerDown>();

                if (hunkerDown != null)
                {
                    Debug.Log("Holding");
                    hunkerDown.Pickup(this.gameObject);
                }
            }
        }    
    }

    private void OnTriggerEnter(Collider col)
    {
        touchedInteractable = col.gameObject.GetComponent<InteractableObjs>();
        touchedGameObject = col.gameObject;
    }

    private void OnTriggerStay(Collider col)
    {
            if (Input.GetAxisRaw(playerInput.GetVerticalDPad()) > 0 && upIsPressed == false && dPadMenu.woodTimer.onCooldown == false && playerState.playerState == PlayerStates.PlayerState.pEmpty)
            {
                upIsPressed = true;
                leftIsPressed = false;
                rightIsPressed = false;
                Debug.Log("DpadWood");

                direction = Direction.up;
                dPadMenu.CollectedWhenPressed(this, direction);
                return;
            }

            if (Input.GetAxisRaw(playerInput.GetHorizontalDPad()) > 0 && rightIsPressed == false && dPadMenu.barrelTimer.onCooldown == false && playerState.playerState == PlayerStates.PlayerState.pEmpty)
            {
                rightIsPressed = true;
                leftIsPressed = false;
                upIsPressed = false;
                direction = Direction.right;
                dPadMenu.CollectedWhenPressed(this, direction);

                Debug.Log("DpadBarrel");
                return;
            }

            if (Input.GetAxisRaw(playerInput.GetHorizontalDPad()) < 0 &&  leftIsPressed == false && dPadMenu.cballTimer.onCooldown == false && playerState.playerState == PlayerStates.PlayerState.pEmpty)
            {
                leftIsPressed = true;
                rightIsPressed = false;
                upIsPressed = false;
                direction = Direction.left;
                dPadMenu.CollectedWhenPressed(this, direction);

                Debug.Log("DpadCannonBall");
                return;
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Edge" && playerState.playerState == PlayerStates.PlayerState.pBucket)
        {
            bucketStates.currentState = BucketStates.BucketState.Held;        
        }

        if (currentObject != null)
            currentObject.Deactivate();

        if (touchedInteractable != null)
            touchedInteractable.Deactivate();

        touchedInteractable = null;
        touchedGameObject = null;
    }

    private void DropItem()
    {
        Debug.Log("Drop mehhhhh");

        if (playerState.playerState == PlayerStates.PlayerState.pHoldingOn)
        {
            HunkerDown other = this.GetComponentInParent<HunkerDown>();

            if (other != null)
            {
                other.ReleaseMast(this.gameObject);
            }
        }

        if (currentObject != null)
        {
            currentObject.DropItem();
        }
    }
}
