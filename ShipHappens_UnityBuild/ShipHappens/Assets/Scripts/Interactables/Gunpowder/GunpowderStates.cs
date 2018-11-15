using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunpowderStates : MonoBehaviour {

    public enum PowderState { Held, Dropped  }

    public PowderState currentState;

	// Use this for initialization
	void Start () {
        currentState = PowderState.Dropped;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
