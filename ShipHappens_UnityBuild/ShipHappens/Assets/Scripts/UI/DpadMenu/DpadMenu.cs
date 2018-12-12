using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DpadMenu : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public RawImage dpad;

    public int menuCount;
    public bool inMenu;

    PlayerController playerController;

    Cannonball cannonball;
    public DpadCannonballTimer cballBool;
    public GameObject cannonballPrefab;

    Gunpowder gunpowder;
    public DpadBarrelTimer barrelBool;
    public GameObject barrelPrefab;

    Wood wood;
    public DpadWoodTimer woodBool;
    public GameObject woodPrefab;


    void Start ()
    {
        canvasGroup.alpha = 0;
    }
	
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            menuCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            menuCount--;
        }
    }

    private void Update()
    {
        if (menuCount >= 1)
        {
            inMenu = true;
            StartCoroutine("FadeIn");
        }
        else
        {
            inMenu = false;
            StartCoroutine("FadeOut");
        }
    }

    IEnumerator FadeOut()
    {
        float time = 5f;
        while (!inMenu)
        {
            canvasGroup.alpha -= Time.deltaTime / time;
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        float time = 5f;
        while (inMenu)
        {
            canvasGroup.alpha += Time.deltaTime / time;
            yield return null;
        }
    }

    public void CollectedWhenPressed(PlayerController player, PlayerController.Direction direction)
    {
        switch (direction)
        {
            case PlayerController.Direction.left:
                cballBool.onCooldown = true;

                GameObject ballPlayerObj = player.gameObject;
                GameObject newBall = Instantiate(cannonballPrefab);
                newBall.GetComponent<Cannonball>().Spawn(ballPlayerObj);
                break;

            case PlayerController.Direction.right:
                barrelBool.onCooldown = true;

                GameObject barrelPlayerObj = player.gameObject;
                GameObject newBarrel = Instantiate(barrelPrefab);
                newBarrel.GetComponent<Gunpowder>().Spawn(barrelPlayerObj);
                break;

            case PlayerController.Direction.up:
                woodBool.onCooldown = true;

                GameObject woodPlayerObj = player.gameObject;
                GameObject newWood = Instantiate(woodPrefab);
                newWood.GetComponent<Wood>().Spawn(woodPlayerObj);

                WoodStates woodStates = newWood.GetComponent<WoodStates>();
                woodStates.currentState = WoodStates.WoodState.Held;
                break;
        }
    }
}