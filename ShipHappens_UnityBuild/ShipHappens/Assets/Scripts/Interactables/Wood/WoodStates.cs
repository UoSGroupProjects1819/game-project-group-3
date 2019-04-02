using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodStates : MonoBehaviour {

	public enum WoodState { Dropped, Held, Repairing };

    public WoodState currentState;

    private void Awake()
    {
        currentState = WoodState.Dropped;
    }
}
