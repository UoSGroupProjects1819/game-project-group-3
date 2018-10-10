using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("[Mapped Controls]")]
    public string horizontalInput = "Horizontal_P1";
    public string verticalInput = "Vertical_P1";
    public string Abutton = "Abutton_P1";
    public string Bbutton = "Bbutton_P1";
    public string Ybutton = "Ybutton_P1";

    [Header("[Objects]")]
    public GameObject player;
    public Rigidbody rb;
    public GameObject activityObject;
    public GameObject hazardObject;
    public GameObject world;

    [Header("[number bits]")]
    public float playerSpeed;
    public float interactionDistance;


    void Start ()
    {
		
	}
	

	void Update ()
    {
        //player movement
        Vector3 movement = new Vector3(Input.GetAxis(horizontalInput), 0, Input.GetAxis(verticalInput));

        transform.rotation = Quaternion.LookRotation(movement);

        if ((Input.GetAxis(horizontalInput) != 0 || Input.GetAxis(verticalInput) != 0))
            rb.AddForce(movement * playerSpeed * Time.deltaTime);


        //interaction
        Debug.DrawRay(player.transform.position, transform.forward * interactionDistance);

        RaycastHit action;
        if (Physics.Raycast(player.transform.position, transform.forward, out action, interactionDistance))
        {
            Debug.DrawRay(player.transform.position, transform.forward * interactionDistance, Color.black);

            if (action.transform.name == "ManagementObject")
            {
                activityObject = action.collider.gameObject;
            }
            else
            {
                activityObject = null;
            }

            if (action.transform.name == "HazardObject")
            {
                hazardObject = action.collider.gameObject;
            }
            else
            {
                hazardObject = null;
            }
        }

        if (Input.GetButtonDown(Abutton))
        {
            activityObject.GetComponent<InteractableObjects>().isFine = true;
            activityObject.GetComponent<InteractableObjects>().isBreaking = false;
            activityObject.GetComponent<InteractableObjects>().isBroken = false;
        }

        if (Input.GetButtonDown(Bbutton))
        {
            Debug.Log("B");
            hazardObject.GetComponent<HazardObjects>().countdown = 27f;
            hazardObject.GetComponent<HazardObjects>().isEnabled = false;
            
        }

        if (Input.GetButton(Ybutton))
        {
            world.GetComponent<WorldManager>().badness -= Time.deltaTime * 3.33f;
        }


        rb.angularDrag = world.GetComponent<WorldManager>().badness / 2;
    }
}
