using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonInteractables : MonoBehaviour
{
    public enum NonInteractableState { inactive, entering, active, exiting };

    public NonInteractableState playerState;



    void Start()
    {
        playerState = NonInteractableState.inactive;
    }
}
