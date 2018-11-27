using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagull : MonoBehaviour
{
    public enum SeagullStates { entering, active, exiting };
    public SeagullStates seagullState;

    public GameObject seagull;
    public GameObject pooPrefab;
    public float speed;
    public float radius;
    public float maxDistance;
    public GameObject shipCentre;

    public float distance;

    private Vector3 lastPos;

    void Awake()
    {
        seagullState = SeagullStates.entering;
	}

    void Update()
    {
        switch (seagullState)
        {
            case SeagullStates.entering:
                //add count to game manager
                //UI update
                //Play audio
                seagullState = SeagullStates.active;
                break;
            case SeagullStates.active:
                seagull.transform.Translate(Vector3.forward * speed * Time.deltaTime);

                distance = Vector3.Distance(seagull.transform.position, shipCentre.transform.position);
                if (distance > maxDistance)
                {
                    seagullState = SeagullStates.exiting;
                }
                break;
            case SeagullStates.exiting:
                Destroy(this.gameObject);
                //remove from gamemanager thing
                //remove from object pool
                break;
        }
    }
}
