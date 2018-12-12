using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballStates : MonoBehaviour
{

    public enum CannonballState { Dropped, Held }

    public CannonballState currentState;

    private void Awake()
    {
        currentState = CannonballState.Dropped;
    }
}
