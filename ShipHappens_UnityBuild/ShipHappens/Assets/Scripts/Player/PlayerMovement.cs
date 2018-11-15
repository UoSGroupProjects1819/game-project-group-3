using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody rb;

    public float speed = 5f;

    // Use this for initialization
    void Start () {
        rb = this.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float moveHorizontal = -Input.GetAxis("Horizontal");
        float moveVertical = -Input.GetAxis("Vertical");


        Vector3 move = new Vector3(moveHorizontal * speed, 0, moveVertical * speed);
        rb.MovePosition(this.transform.position + move * Time.deltaTime);

        if (move != Vector3.zero)
        {
            transform.forward = -move;
        }
    }
}
