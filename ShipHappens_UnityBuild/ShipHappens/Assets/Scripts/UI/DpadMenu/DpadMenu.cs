using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DpadMenu : MonoBehaviour
{
    public MultiObjectPool objectPooler;

    public CanvasGroup canvasGroup;
    public RawImage dpad;

    public int menuCount;
    public bool inMenu;

    PlayerController playerController;
    PlayerStates playerStates;

    public DpadBarrelTimer barrelTimer;
    public DpadWoodTimer woodTimer;
    public DpadCannonballTimer cballTimer;

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
        if(playerStates == null) { playerStates = player.GetComponent<PlayerStates>(); }

        switch (direction)
        {
            case PlayerController.Direction.left:
                cballBool.onCooldown = true;

                GameObject ballPlayerObj = player.gameObject;
                //PlayerStates playerStates = player.GetComponent<PlayerStates>();
                //GameObject newBall = Instantiate(cannonballPrefab);
                GameObject newBall = objectPooler.SpawnFromPool("Cannonballs", transform.position, transform.rotation);
                newBall.GetComponent<CannonballObj>().Pickup(player.gameObject, player, player.playerState);
                playerStates = null;

                //newBall.GetComponent<CannonballObj>().EnableCannonball();
                break;

            case PlayerController.Direction.right:
                barrelBool.onCooldown = true;

                GameObject barrelPlayerObj = player.gameObject;
                //GameObject newBarrel = Instantiate(barrelPrefab);
                GameObject newBarrel = objectPooler.SpawnFromPool("Gunpowder", transform.position, transform.rotation);
                newBarrel.GetComponent<GunpowderObj>().Pickup(player.gameObject, player, player.playerState);
                playerStates = null;

                break;

            case PlayerController.Direction.up:
                woodBool.onCooldown = true;

                GameObject woodPlayerObj = player.gameObject;
                //PlayerStates playerStates = player.GetComponent<PlayerStates>();
                //GameObject newWood = Instantiate(woodPrefab);
                GameObject newWood = objectPooler.SpawnFromPool("Planks", transform.position, transform.rotation);
                newWood.GetComponent<WoodObj>().Pickup(player.gameObject, player, player.playerState);

                //WoodStates woodStates = newWood.GetComponent<WoodStates>();
                //woodStates.currentState = WoodStates.WoodState.Held;
                playerStates = null;

                break;
        }
    }
}