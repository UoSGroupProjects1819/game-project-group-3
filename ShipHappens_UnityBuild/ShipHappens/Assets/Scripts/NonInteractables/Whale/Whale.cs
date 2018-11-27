using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : MonoBehaviour
{
    public enum WhaleStates { entering, active, exiting };
    public WhaleStates whaleStates;

    public GameObject whale;
    public ParticleSystem whalePS;

	void Start ()
    {
		
	}

    void Update()
    {
        switch (whaleStates)
        {
            case WhaleStates.entering:
                //add count to game manager
                //UI update
                //Play audio
                whaleStates = WhaleStates.active;
                break;
            case WhaleStates.active:
                //play animation
                //on finish animation, play particle system
                //rock the boat for (duration)
                //after duration...
                //whaleStates = WhaleStates.exiting;
                break;
            case WhaleStates.exiting:
                //reset to start
                break;
        }
    }
}
