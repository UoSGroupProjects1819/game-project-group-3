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

            case PlayerStates.PlayerState.pRespawn:
                clockbase.enabled = true;
                clock.enabled = true;
                text.enabled = true;
                player.SetActive(false);
                
                timer -= Time.deltaTime;
                text.text = timer.ToString();
                clock.fillAmount = timer / initialTimer;

                if (timer <= 0)
                {
                    clockbase.enabled = false;
                    clock.enabled = false;
                    text.enabled = false;
                    RespawnPlayer();
                    playerStates.playerState = PlayerStates.PlayerState.pEmpty;
                }
                break;
        }



        //if (playerStates.playerState == PlayerStates.PlayerState.pWhaled)
        //{

        //}

        //if (playerStates.playerState == PlayerStates.PlayerState.pRespawn)
        //{

        //}
    }

    IEnumerator DelayRespawn()
    {
        yield return new WaitForSeconds(0.55f);
        playerStates.playerState = PlayerStates.PlayerState.pRespawn;
    }

    void RespawnPlayer()
    {
        player.SetActive(true);
        player.transform.position = spawnPoint.transform.position;
        player.transform.rotation = spawnPoint.transform.rotation;
    }
}
