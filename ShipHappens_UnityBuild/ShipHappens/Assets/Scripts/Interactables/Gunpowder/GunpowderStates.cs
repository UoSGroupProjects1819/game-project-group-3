using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunpowderStates : MonoBehaviour {

    public enum PowderState { Held, Dropped  }

    public PowderState currentState;

	// Use this for initialization
	void Awake () {
        currentState = PowderState.Dropped;
	}
}
