using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DpadCannonballTimer : MonoBehaviour
{
    public PlayerController p1Controller;
    public PlayerController p2Controller;

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

                if (p1Controller.rightIsPressed == true)
                {
                    p1Controller.rightIsPressed = false;
                }
                if (p2Controller.rightIsPressed == true)
                {
                    p2Controller.rightIsPressed = false;
                }
            }
        }
        else
        {
            countdownImg.fillAmount = 0;
        }
    }
}
