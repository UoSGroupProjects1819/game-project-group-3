using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DpadWoodTimer : MonoBehaviour
{
    public Image countdownImg;
    public float fillCountdown = 1f;
    public float fillSpeed;

    public bool onCooldown;

    void Update()
    {
        if (onCooldown)
        {
            fillCountdown = fillCountdown - fillSpeed * Time.deltaTime;
            countdownImg.fillAmount = fillCountdown;

            if (fillCountdown <= 0)
            {
                onCooldown = false;
                fillCountdown = 1;
            }
        }
        else
        {
            countdownImg.fillAmount = 0;
        }
    }
}
