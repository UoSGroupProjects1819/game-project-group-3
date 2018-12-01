using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodTimer : MonoBehaviour
{
    public Image countdownImg;
    public float fillCountdown = 1f;
    public float fillSpeed;

    public GameObject currentPlayer;
    public GameObject repairWood;

    public bool onCooldown;
    public bool hasDispensed;

    void Update()
    {
        if (onCooldown)
        {

            fillCountdown = fillCountdown - fillSpeed * Time.deltaTime;
            countdownImg.fillAmount = fillCountdown;

            if (hasDispensed == false)
            {
                GameObject woodObj = Instantiate(repairWood, currentPlayer.transform.position, currentPlayer.transform.rotation);

                var pState = currentPlayer.GetComponent<PlayerStates>();
                pState.playerState = PlayerStates.PlayerState.pWood;

                hasDispensed = true;
            }

            if (fillCountdown <= 0)
            {
                onCooldown = false;
                hasDispensed = false;
                fillCountdown = 1;
            }
        }
    }
}
