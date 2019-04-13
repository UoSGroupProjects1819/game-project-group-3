﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnerMop : MonoBehaviour
{
    public GameObject mop;
    MopStates mopStates;
    public GameObject spawnPoint;
    public GameObject sea;
    private Rigidbody rb;

    public float timer;
    public float initialTimer;
    public Image clockbase;
    public Image clock;
    public Text text;


    void Start()
    {
        clockbase.enabled = false;
        clock.enabled = false;
        text.enabled = false;
        mopStates = mop.GetComponent<MopStates>();
        rb = mop.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (mop.transform.position.y < sea.transform.position.y)
        {
            mop.SetActive(false);
            mop.transform.parent = null;
            timer = initialTimer;
            StartCoroutine(DelayRespawn());
        }
    }

    IEnumerator DelayRespawn()
    {
        yield return new WaitForSeconds(1f);

        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        RespawnTimer();
    }

    void RespawnTimer()
    {
        clockbase.enabled = true;
        clock.enabled = true;
        text.enabled = true;

        timer -= Time.deltaTime;
        text.text = timer.ToString("F0");
        clock.fillAmount = timer / initialTimer;

        if (timer <= 0)
        {
            clockbase.enabled = false;
            clock.enabled = false;
            text.enabled = false;
            Respawn();
        }
    }

    void Respawn()
    {
        mopStates.currentState = MopStates.MopState.Dropped;
        mop.transform.position = spawnPoint.transform.position;
        mop.transform.rotation = spawnPoint.transform.rotation;
        mop.SetActive(true);
    }
}