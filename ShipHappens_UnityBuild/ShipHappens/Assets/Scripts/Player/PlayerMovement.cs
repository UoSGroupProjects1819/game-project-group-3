using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private CharacterController controller;
    private bool isGrounded = true;
    private Vector3 velocity;
    public Transform groundChecker;


    public float speed = 5f;
    public float GroundDistance = 0.2f;
    public LayerMask ground;

    // Use this for initialization
    void Start () {
        groundChecker = transform.GetChild(0);
        controller = this.GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, GroundDistance, ground, QueryTriggerInteraction.Ignore);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        float moveHorizontal = -Input.GetAxis("Horizontal");
        float moveVertical = -Input.GetAxis("Vertical");


        Vector3 move = new Vector3(moveHorizontal, 0, moveVertical);
        controller.Move(move * Time.deltaTime * speed);

        if (move != Vector3.zero)
        {
            transform.forward = -move;
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
