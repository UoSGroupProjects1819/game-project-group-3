using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    public BoxInteraction boxInteraction;

    string boxState;

    public enum Abilities { Heat, Cool, a3};
    public Abilities abilities;

    private void OnTriggerStay(Collider other)
    {
        boxInteraction = other.GetComponent<BoxInteraction>();

        if (other.tag == "Box")
        {
            boxState = boxInteraction.GetBoxState();

            PlayerComand();

            boxInteraction.SetBoxState(boxState);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Box")
        {
            boxInteraction = null;
        }
    }

    void PlayerComand()
    {
        if (Input.GetButtonDown("Action"))
        {
            switch (abilities)
            {
                case Abilities.Heat:
                    switch (boxState)
                    {
                        case "solid":
                            boxState = "liquid";
                            break;
                        case "liquid":
                            boxState = "gas";
                            break;
                        default:
                            break;
                    }
                    break;
                case Abilities.Cool:
                    switch (boxState)
                    {
                        case "gas":
                            boxState = "liquid";
                            break;
                        case "liquid":
                            boxState = "solid";
                            break;
                        default:
                            break;
                    }
                    break;
            }
            
        }


    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
