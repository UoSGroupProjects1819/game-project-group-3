﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnerTorch : MonoBehaviour
{
    public GameObject torch;
    TorchStates torchStates;
    public GameObject spawnPoint;
    public GameObject sea;
    private Rigidbody rb;

    public float timer;
    public float initialTimer;
    public Image clockbase;
    public Image clock;
    public Text text;

    public bool isRespawning;


    void Start()
    {
        clockbase.enabled = false;
        clock.enabled = false;
        text.enabled = false;
        torchStates = torch.GetComponent<TorchStates>();
        rb = torch.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (torch.transform.position.y < sea.transform.position.y - 0.5f && isRespawning == false)
        {
            torch.SetActive(false);
            torch.transform.parent = null;
            timer = initialTimer;

            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            isRespawning = true;
        }

        if (isRespawning)
        {
            clockbase.enabled = true;
            clock.enabled = true;
            text.enabled = true;

            timer -= 1 * Time.deltaTime;
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
    }

    void Respawn()
    {
        torchStates.currentState = TorchStates.TorchState.Dropped;
        torch.transform.position = spawnPoint.transform.position;
        torch.transform.rotation = spawnPoint.transform.rotation;
        torch.SetActive(true);
        isRespawning = false;
    }
}
