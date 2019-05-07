using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionScreenMainGame : MonoBehaviour
{
    public RectTransform PressAToContinue;
    public LevelManager levelManagerScript;


    public float timer;
    public float initialTimer;

    private void Start()
    {
        timer = initialTimer;
        PressAToContinue.gameObject.SetActive(false);
    }

    void Update()
    {
        timer -= 1 * Time.deltaTime;

        if (timer < 0)
        {
            PressAToContinue.gameObject.SetActive(true);


            if (Input.GetKeyDown("joystick button 0"))
            {
                levelManagerScript.FadeToLevel(6);
            }
        }
    }
}
