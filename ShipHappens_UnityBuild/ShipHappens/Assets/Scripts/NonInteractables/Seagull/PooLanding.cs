﻿using System.Collections;
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

    void Start()
    {
        //for (int i = 0; i < players.Length; i++)
        //{
        //    Physics.IgnoreCollision(GetComponent<Collider>(), players[i].GetComponent<Collider>());
        //}

        //Physics.IgnoreCollision(player.GetComponent<CapsuleCollider>(), GetComponent<SphereCollider>());

        Physics.IgnoreLayerCollision(0, 10);
    }

    void Awake()
    {
        rb = pooPrefab.GetComponent<Rigidbody>();
        col = pooPrefab.GetComponent<SphereCollider>();

        InitialScale = transform.localScale;
    }

    void FixedUpdate()
    {
        if (isLanded == false)
        {
            RaycastHit hit;
            Debug.DrawRay(pooPrefab.transform.position, pooPrefab.transform.up * -1, Color.white);
            if (Physics.Raycast(pooPrefab.transform.position, pooPrefab.transform.up * -1, out hit, 2, 1 << 9))
            {
                Debug.DrawRay(pooPrefab.transform.position, pooPrefab.transform.up, Color.red);
                pooPrefab.transform.up = -hit.normal;
                isLanded = true;
            }

            if (Physics.Raycast(pooPrefab.transform.position, pooPrefab.transform.up * -1, out hit, 1))
            {
                if (hit.transform.tag == "poo")
                {
                    Destroy(this.gameObject);
                    belowPoo = hit.transform.gameObject;
                    belowPoo.transform.localScale = OverflowScale;
                    isLanded = true;
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
            //StartCoroutine("PooGrowth");
            pooPrefab.transform.localScale = FinalScale;
            pooPrefab.transform.SetParent(collision.gameObject.transform);
        }
    }

    //IEnumerator PooGrowth()
    //{
    //    float progress = 0;

    //    while (progress <= 1)
    //    {
    //        pooPrefab.transform.localScale = Vector3.Lerp(InitialScale, FinalScale, progress);
    //        progress += Time.deltaTime * TimeScale;
    //        while (progress <= 1)
    //        yield return null;
    //    }
    //    transform.localScale = FinalScale;
    //}

}
