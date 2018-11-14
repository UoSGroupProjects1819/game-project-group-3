using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public PlayerStates playerState;



    

    // Use this for initialization
    void Start() {
        playerState = this.GetComponent<PlayerStates>();
        
       
    }

    private void Update()
    {
        DropItem();
    }


    void FixedUpdate()
    {
        
    }

    private void OnTriggerStay(Collider col)
    {
        Interactable other = col.gameObject.GetComponent<Interactable>();

        if (other != null)
        {
            if (Input.GetKey(KeyCode.I))
            {
                other.Action(this.gameObject);
            }
        }
    }

    private void DropItem()
    {
        if(playerState.itemHeld == null)
        { return; }

        if (playerState.itemHeld != null && Input.GetKey(KeyCode.U))
        {
            Interactable other = this.GetComponentInChildren<Interactable>();

            if (other != null)
            {
                other.DropItem();
            }
        }
    }
}
