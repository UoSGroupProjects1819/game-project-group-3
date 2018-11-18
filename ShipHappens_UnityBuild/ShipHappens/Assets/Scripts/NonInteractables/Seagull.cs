using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagull : MonoBehaviour
{
    public enum SeagullStates { entering, active, exiting };
    public SeagullStates seagullState;

    public GameObject seagull;
    public float speed;


    void Start ()
    {
        seagullState = SeagullStates.entering;
	}

    public void UpdateState(SeagullStates cannonState)
    {
        switch (seagullState)
        {
            case SeagullStates.entering:
                //UI
                //Play audio
                break;
            case SeagullStates.active:
                //fly over one of [x] waypointed paths/animations
                //drop poo OR spawn poo on deck
                //poo script
                //seagull.transform.forward * (speed * Time.deltaTime);
                Debug.Log("gullboi active");
                break;
            case SeagullStates.exiting:
                //remove from gamemanager thing
                //destroy gameobject/object pool
                break;
            default:
                break;
        }
    }
}
