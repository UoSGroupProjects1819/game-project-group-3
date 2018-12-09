using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodStates : MonoBehaviour {

	public enum WoodState { Dropped, Held };

    public WoodState currentState;

    private void Start()
    {
        currentState = WoodState.Dropped;
    }
}
