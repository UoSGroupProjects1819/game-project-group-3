using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerInput playerInput;

    [Header("[Movement variables]")]
    public float speed = 5f;

    public bool canMove = true;

    // Use this for initialization
    void Start ()
    {
        rb = this.GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (canMove)
        {
            Vector3 move = new Vector3(playerInput.HorizontalMovement * speed, 0, playerInput.VerticalMovement * speed);
            rb.MovePosition(this.transform.position + move * Time.deltaTime);

            if (move != Vector3.zero)
            {
                transform.forward = -move;
            }
        }
    }
}
