using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooLanding : MonoBehaviour
{
    Seagull seagull;

    public GameObject pooPrefab;
    public Rigidbody rb;
    public Collider col;
    private GameObject belowPoo;

    //private int shipLayer = 9;
    //private LayerMask layerMask = 1 << 9;

    private Vector3 InitialScale;
    public Vector3 FinalScale;
    public Vector3 OverflowScale;

    public float ScalingFactor = 1.7f;
    public float TimeScale = 0.5f;

    public bool isLanded = false;

    public GameObject player;

    private GameObject floodPlane;

    void Start()
    {
        floodPlane = GameObject.Find("FloodWater");

        //for (int i = 0; i < players.Length; i++)
        //{
        //    Physics.IgnoreCollision(GetComponent<Collider>(), players[i].GetComponent<Collider>());
        //}

        //Physics.IgnoreCollision(player.GetComponent<CapsuleCollider>(), GetComponent<SphereCollider>());

        Physics.IgnoreLayerCollision(29, 10);
    }

    void Awake()
    {
        rb = pooPrefab.GetComponent<Rigidbody>();
        col = pooPrefab.GetComponent<SphereCollider>();

        InitialScale = transform.localScale;
    }

    private void Update()
    {
        if (transform.position.y < floodPlane.transform.position.y - 0.65f)
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if (isLanded == false)
        {
            RaycastHit hit;
            Debug.DrawRay(pooPrefab.transform.position, pooPrefab.transform.up * -1, Color.white);
            if (Physics.Raycast(pooPrefab.transform.position, pooPrefab.transform.up * -1, out hit, 2, 1 << 29))
            {
                Debug.DrawRay(pooPrefab.transform.position, pooPrefab.transform.up, Color.red);
                pooPrefab.transform.up = -hit.normal;
                isLanded = true;
            }

            if (Physics.Raycast(pooPrefab.transform.position, pooPrefab.transform.up * -1, out hit, 1))
            {
                if (hit.transform.tag == "poo")
                {
                    //Destroy(this.gameObject);
                    this.gameObject.SetActive(false);
                    belowPoo = hit.transform.gameObject;
                    belowPoo.transform.localScale = OverflowScale;
                    isLanded = true;
                }

                if (hit.transform.tag == "Hole")
                {
                    //Destroy(this.gameObject);
                    this.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ShipDeck")
        {
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            col.isTrigger = true;
            pooPrefab.transform.localScale = FinalScale;
            pooPrefab.transform.SetParent(collision.gameObject.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("PLAYER STEPPED ON POO");
            PlayerMovement movement = other.GetComponent<PlayerMovement>();
            movement.SetSlowTime(3);
        }
    }
}
