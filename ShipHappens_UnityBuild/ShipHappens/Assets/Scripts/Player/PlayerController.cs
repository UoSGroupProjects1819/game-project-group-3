using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5;
    private Rigidbody rb;

    public GameObject itemHeld;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        Movement();
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * 150f;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }

    private void OnTriggerStay(Collider col)
    {
        Interactable other = col.gameObject.GetComponent<Interactable>();

        if (other != null)
        {
            if (Input.GetKey(KeyCode.I))
            {
                other.Action(this.gameObject);
            }
        }
    }

    private void DropItem()
    {
        if (itemHeld != null)
        {
            Interactable other = this.GetComponentInChildren<Interactable>();

            if (other != null)
            {

            }
        }
    }
}
