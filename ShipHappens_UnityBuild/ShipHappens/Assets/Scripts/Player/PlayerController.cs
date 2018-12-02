using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public PlayerStates playerState;

    [Header("[Mapped Controls]")]
    public string Abutton = "A_P1";
    public string Bbutton = "B_P1";
    public string DpadHorizontal = "Dpad_Left/Right_P1";
    public string DpadVertical = "Dpad_Up/Down_P1";



    // Use this for initialization
    void Start()
    {
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
            Debug.Log("Hit interactable");
            if (Input.GetKey(KeyCode.I) || Input.GetButtonDown(Abutton))
            {
                Debug.Log("Action button pressed");
                other.Action(this.gameObject);
            }
        }

        //if (other.tag == "HoldOn")
        //{
        //    if (Input.GetKey(KeyCode.I) || Input.GetButtonDown(Abutton))
        //    {
        //        Debug.Log("Action button pressed");
        //        other.Action(this.gameObject);
        //    }
        //}

        if (other.tag == "ShipHold")
        {
            if (Input.GetAxisRaw(DpadVertical) > 0 || (Input.GetKey(KeyCode.K)))
            {
                Debug.Log("DpadWood");

                WoodTimer woodscript = other.GetComponent<WoodTimer>();
                if (woodscript.onCooldown == false && playerState.playerState == PlayerStates.PlayerState.pEmpty)
                {
                    woodscript.onCooldown = true;
                    woodscript.currentPlayer = this.gameObject;
                }


            }

            if (Input.GetAxisRaw(DpadHorizontal) > 0 || (Input.GetKey(KeyCode.J)))
            {
                Debug.Log("DpadBarrel");
            }

            if (Input.GetAxisRaw(DpadHorizontal) < 0 || (Input.GetKey(KeyCode.L)))
            {
                Debug.Log("DpadCannonBall");
            }
        }
    }

    private void DropItem()
    {
        if(playerState.itemHeld == null)
        { return; }

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
