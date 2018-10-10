using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectSwing : MonoBehaviour
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
            _grapple.playerSwing = true;
            _grapple.swingPlayer = other.gameObject;
        }
    }
}
