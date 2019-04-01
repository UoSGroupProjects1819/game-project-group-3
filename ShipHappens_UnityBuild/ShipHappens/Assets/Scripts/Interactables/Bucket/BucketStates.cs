using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketStates : MonoBehaviour {

    public enum BucketState { Dropped, Held }

    public BucketState currentState;

    private void Start()
    {
        currentState = BucketState.Dropped;
    }
}
