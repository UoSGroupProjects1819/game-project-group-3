using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapple : MonoBehaviour
{
    [Header("[Mapped Controls]")]
    public string fireInput = "Fire1_P1";
    public string detachInput = "Fire2_P1";
    public string swingInput = "Fire3_P1";

    [Header("Player Objects")]
    public GameObject player;
    public GameObject playerOther;
    public Rigidbody2D otherPlayerrb;
    public Rigidbody2D playerrb;

    [Header("Grapple Tool Objects")]
    public GameObject hook;
    public GameObject swingHook;
    private Rigidbody hookrb;
    private Rigidbody swingrb;
    public GameObject hookHolster;
    public GameObject swingHolster;
    public LineRenderer rope;
    public LineRenderer swingRope;
    public DistanceJoint2D joint;

    [Header("Grapple Interactable Objects")]
    public GameObject hookedPlayer;
    public GameObject swingPlayer;
    public GameObject grabbedObject;

    [Header("Speed Values")]
    public float hookTravelSpeed;
    public float playerTravelSpeed;

    [Header("Grapple State Bools")]
    public bool isFired = false;
    public bool isSwing = false;
    public bool playerHooked = false;
    public bool playerSwing = false;
    public bool objectGrabbed = false;

    public bool firstSwingDistance = false;

    void Start ()
    {
        isSwing = false;
        joint.enabled = false;
        rope = hook.GetComponent<LineRenderer>();
        hookrb = hook.GetComponent<Rigidbody>();
    }
	
	void Update ()
    {
    //launch grapple
        //if not fired, fire.
        if (Input.GetButtonDown(fireInput) && isFired == false && isSwing == false)
            isFired = true;

        if (Input.GetButtonDown(swingInput) && isFired == false && isSwing == false);
            isSwing = true;

        //if is fired, reset position and fire.
        if (Input.GetButtonDown(fireInput) && isFired && isSwing == false)
        {
            RetractHook();
            isFired = true;
        }

        //if is swing, reset position and fire.
        if (Input.GetButtonDown(fireInput) && isSwing && isFired == false)
        {
            RetractSwing();
            isSwing = true;
        }

        //reset hook position.
        if (Input.GetButtonDown(detachInput))
        {
            RetractHook();
            RetractSwing();
        }


        //while fired.
        if (isFired)
        {
            hook.transform.position = Vector3.MoveTowards(hook.transform.position, playerOther.transform.position, hookTravelSpeed * Time.deltaTime);

            //set line renderer vertices
            rope.SetVertexCount(2);
            rope.SetPosition(0, hookHolster.transform.position);
            rope.SetPosition(1, hook.transform.position);
        }


        if (isSwing)
        {
            swingHook.transform.position = Vector3.MoveTowards(swingHook.transform.position, playerOther.transform.position, hookTravelSpeed * Time.deltaTime);

            //set line renderer vertices
            swingRope.SetVertexCount(2);
            swingRope.SetPosition(0, swingHolster.transform.position);
            swingRope.SetPosition(1, swingHook.transform.position);
        }

        //pull player
        if (playerHooked)
        {
            //player.transform.position = Vector3.MoveTowards(player.transform.position, hookedPlayer.transform.position, Time.deltaTime * playerTravelSpeed);
            //hookedPlayer.transform.position = Vector3.MoveTowards(hookedPlayer.transform.position, player.transform.position, Time.deltaTime * playerTravelSpeed);
            hook.transform.position = hookedPlayer.transform.position;

            Vector3 fromPlayertoOtherPlayer = playerOther.transform.position - player.transform.position;
            playerrb.AddRelativeForce((fromPlayertoOtherPlayer / 2) * playerTravelSpeed);
            otherPlayerrb.AddRelativeForce((-fromPlayertoOtherPlayer / 2) * playerTravelSpeed);

            float playerDistance = Vector3.Distance(player.transform.position, hookedPlayer.transform.position);


            if (playerDistance < 1f)
            {
                RetractHook();
                hookedPlayer = null;
                isFired = false;
                rope.SetVertexCount(0);
                hook.transform.position = hookHolster.transform.position;

                //Vector3 zero = new Vector3(0, 0, 0);
                //playerOther.GetComponent<Rigidbody2D>().velocity = zero;

                //Vector3 fromPlayertoOtherPlayer = playerOther.transform.position - player.transform.position;
                //playerrb.AddRelativeForce((-fromPlayertoOtherPlayer * 10) * playerTravelSpeed);
                //playerOther.GetComponent<Rigidbody2D>().AddRelativeForce((fromPlayertoOtherPlayer * 10) * playerTravelSpeed);

                playerHooked = false;
            }
        }


        //pull in object
        if (objectGrabbed)
        {
            grabbedObject.transform.parent = hook.transform;

            hook.transform.position = Vector3.MoveTowards(hook.transform.position, hookHolster.transform.position, Time.deltaTime * playerTravelSpeed);
            float distanceToHolster = Vector3.Distance(hook.transform.position, hookHolster.transform.position);

            if (distanceToHolster < 0.5f)
            {
                Destroy(grabbedObject);
                grabbedObject = null;
                objectGrabbed = false;
                isFired = false;
                rope.SetVertexCount(0);
                hook.transform.position = hookHolster.transform.position;
            }
        }


        //swing player
        if (playerSwing)
        {
            float swingDistanceLimit = 30;

            if (!firstSwingDistance)
            {
                swingDistanceLimit = Vector2.Distance(player.transform.position, otherPlayerrb.transform.position);
                firstSwingDistance = true;
            }

            joint.enabled = true;
            joint.connectedBody = otherPlayerrb;
            joint.distance = swingDistanceLimit;

            float playerDistance = Vector3.Distance(player.transform.position, hookedPlayer.transform.position);

            if (playerDistance < 1f)
            {
                RetractSwing();
                swingPlayer = null;
                isSwing = false;
                swingRope.SetVertexCount(0);
                swingHook.transform.position = swingHolster.transform.position;

                playerSwing = false;
            }
        }
    }

    void RetractHook()
    {
        hook.transform.rotation = hookHolster.transform.rotation;
        hook.transform.position = hookHolster.transform.position;
        hook.transform.parent = hookHolster.transform;
        isFired = false;

        rope.SetVertexCount(0);
    }

    void RetractSwing()
    {
        joint.enabled = false;

        hook.transform.rotation = hookHolster.transform.rotation;
        hook.transform.position = hookHolster.transform.position;
        hook.transform.parent = hookHolster.transform;
        isSwing = false;

        rope.SetVertexCount(0);
        firstSwingDistance = false;
    }
}
