using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJoin : MonoBehaviour
{
    public LevelManager levelManagerScript;
    public GameObject player;
    private Vector3 initialPlayerPosition;
    public Image Abutton;
    //public Image Bbutton;

    private float timer;
    public float initialTimer;

    private bool showButton = false;


    void Start()
    {
        Abutton.enabled = false;
        //Bbutton.enabled = false;
        timer = initialTimer;
        initialPlayerPosition = player.transform.position;
    }

    void Update()
    {
        if (!showButton)
        {
            FadeButtonIn();
        }

        TogglePlayerUI();

        if (Input.GetKeyDown("joystick 1 button 7"))
        {
            levelManagerScript.FadeToLevel(0);
        }
    }

    void TogglePlayerUI()
    {
        if (Input.GetKeyDown("joystick 1 button 0") && !player.active)
        {
            Debug.Log("A");
            player.SetActive(true);
            Abutton.gameObject.SetActive(false);
            //Bbutton.gameObject.SetActive(true);
            Debug.Log("A end");
        }

        if (Input.GetKeyDown("joystick 1 button 1") && player.active)
        {
            Debug.Log("B");
            player.transform.position = initialPlayerPosition;
            player.SetActive(false);
            Abutton.gameObject.SetActive(true);
            //Bbutton.gameObject.SetActive(false);
            Debug.Log("B end");
        }
    }

    void FadeButtonIn()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            Abutton.enabled = true;
        }
    }
}
