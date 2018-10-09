using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

    // Link up the correct door to each trigger pad in inspector
    public GameObject door;

    private Animator doorAnim;

    private void Start()
    {
        // Pull up the door's animator
        doorAnim = door.GetComponent<Animator>();
        Debug.Log("Door Anim = " + doorAnim.GetComponent<Animator>());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            doorAnim.SetBool("isOpen", true);
        }
    }
}
