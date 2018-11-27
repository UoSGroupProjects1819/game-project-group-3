using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("[Mapped Controls]")]
    public string horizontalInput = "Horizontal_P1";
    public string verticalInput = "Vertical_P1";

    private Rigidbody rb;

    [Header("[Movement variables]")]
    public float speed = 5f;

    // Use this for initialization
    void Start ()
    {
        rb = this.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float moveHorizontal = -Input.GetAxis(horizontalInput);
        float moveVertical = -Input.GetAxis(verticalInput);


        Vector3 move = new Vector3(moveHorizontal * speed, 0, moveVertical * speed);
        rb.MovePosition(this.transform.position + move * Time.deltaTime);

        if (move != Vector3.zero)
        {
            transform.forward = -move;
        }
    }
}
