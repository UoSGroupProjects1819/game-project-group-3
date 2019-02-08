using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Respawner : MonoBehaviour
{
    public GameObject player;
    PlayerStates playerStates;
    public GameObject spawnPoint;

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
        playerStates = player.GetComponent<PlayerStates>();
    }


    void Update()
    {
        switch (playerStates.playerState)
        {
            case PlayerStates.PlayerState.pWhaled:
                timer = initialTimer;
                StartCoroutine(DelayRespawn());
                break;

            case PlayerStates.PlayerState.pDead:
                timer = initialTimer;
                player.SetActive(false);
                playerStates.playerState = PlayerStates.PlayerState.pRespawn;
                break;

            case PlayerStates.PlayerState.pRespawn:
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
                    RespawnPlayer();
                }
                break;
        }
    }

    IEnumerator DelayRespawn()
    {
        yield return new WaitForSeconds(1f);

        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        playerStates.playerState = PlayerStates.PlayerState.pRespawn;
    }

    void RespawnPlayer()
    {
        playerStates.playerState = PlayerStates.PlayerState.pEmpty;
        player.transform.position = spawnPoint.transform.position;
        player.transform.rotation = spawnPoint.transform.rotation;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.SetActive(true);
    }
}
