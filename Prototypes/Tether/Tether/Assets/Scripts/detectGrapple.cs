using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectGrapple : MonoBehaviour
{
    public GameObject player;
    grapple _grapple;

    void Start()
    {
        _grapple = player.GetComponent<grapple>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _grapple.playerHooked = true;
            _grapple.hookedPlayer = other.gameObject;
        }

        if (other.tag == "Collectable")
        {
            _grapple.objectGrabbed = true;
            _grapple.grabbedObject = other.gameObject;
        }
    }
}
