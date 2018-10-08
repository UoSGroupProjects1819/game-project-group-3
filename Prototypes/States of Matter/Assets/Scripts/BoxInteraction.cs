using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteraction : MonoBehaviour {

    public string boxState;

    public Material solidMat, liquidMat, gasMat;


    private void Start()
    {
        boxState = "solid";
    }

    public string GetBoxState()
    {
        return boxState;
    }

    public void SetBoxState(string state)
    {
        switch (state)
        {
            case "solid":
                boxState = "solid";
                gameObject.GetComponent<Renderer>().material = solidMat;
                break;
            case "liquid":
                boxState = "liquid";
                gameObject.GetComponent<Renderer>().material = liquidMat;
                break;
            case "gas":
                boxState = "gas";
                gameObject.GetComponent<Renderer>().material = gasMat;
                break;
            default:
                boxState = "broken";
                break;                
        }
    }
}
