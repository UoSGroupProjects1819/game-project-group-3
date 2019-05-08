using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerInput playerInput;

    [Header("[Movement variables]")]
    public float speed;
    private const float normalSpeed = 22f;
    private const float slowedSpeed = 11f;
    private float slowTimer;

    public bool canMove = true;

    private bool isSlowed { get; set; }

    // Use this for initialization
    void Start ()
    {
        rb = this.GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        speed = normalSpeed;
    }

    private void Update()
    {
        if (isSlowed)
        {
            SlowPlayer();
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (canMove)
        {
            Vector3 move = new Vector3(-playerInput.HorizontalMovement * speed, 0, playerInput.VerticalMovement * speed);
            rb.MovePosition(this.transform.position + move * Time.deltaTime);

            if (move != Vector3.zero)
            {
                transform.forward = -move;
            }
        }
    }

    private void SlowPlayer()
    {
        slowTimer -= Time.deltaTime;

        if(slowTimer <= 0)
        {
            Debug.Log("Player returns to normal speed");
            speed = normalSpeed;
        }
    }

    public void SetSlowTime(int time)
    {
        Debug.Log("Player is slowed by poo");
        slowTimer = time;
        isSlowed = true;
        speed = slowedSpeed;
    }
}
