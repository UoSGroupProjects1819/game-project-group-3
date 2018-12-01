using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodTimer : MonoBehaviour
{
    public Image countdownImg;
    public float fillCountdown = 1f;
    public float fillSpeed;

    public bool onCooldown;

    private void Start()
    {
        countdownImg.fillAmount = 0f;
    }

    void Update()
    {
        if (onCooldown)
        {
            fillCountdown = fillCountdown - fillSpeed * Time.deltaTime;
            countdownImg.fillAmount = fillCountdown;

            if (fillCountdown <= 0)
            {
                onCooldown = false;
            }
        }
    }
}
