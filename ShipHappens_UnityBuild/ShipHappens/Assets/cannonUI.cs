using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cannonUI : MonoBehaviour
{
    public Image cannonball, gunpowder, torch, cannonballBackground, gunpowderBackground, torchBackground;
    public Sprite cannonballSprite, gunpowderSprite;

    public CannonState cannonState;

	// Use this for initialization
	void Start ()
    {
        cannonState = this.GetComponentInParent<CannonState>();
        cannonball.GetComponent<Image>().sprite = cannonballSprite;
        gunpowder.GetComponent<Image>().sprite = gunpowderSprite;
        Reset();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Reset();

		switch(cannonState.currentState)
        {
            case CannonState.CannonStates.cEmpty:
                cannonballBackground.GetComponent<Image>().color = Color.red;
                gunpowderBackground.GetComponent<Image>().color = Color.red;
                break;
            case CannonState.CannonStates.cCannonBall:
                cannonballBackground.GetComponent<Image>().color = Color.green;
                gunpowderBackground.GetComponent<Image>().color = Color.red;
                break;
            case CannonState.CannonStates.cGunpowder:
                cannonballBackground.GetComponent<Image>().color = Color.red;
                gunpowderBackground.GetComponent<Image>().color = Color.green;
                break;
            case CannonState.CannonStates.cFullyLoaded:
                torchBackground.enabled = true;
                cannonballBackground.enabled = false;
                gunpowderBackground.enabled = false;
                torch.enabled = true;
                cannonball.enabled = false;
                gunpowder.enabled = false;
                torchBackground.GetComponent<Image>().color = Color.red;
                break;

        }
	}

    public void Reset()
    {
        torchBackground.enabled = false;
        cannonballBackground.enabled = true;
        gunpowderBackground.enabled = true;

        torch.enabled = false;
        cannonball.enabled = true;
        gunpowder.enabled = true;

        cannonballBackground.GetComponent<Image>().color = Color.red;
        gunpowderBackground.GetComponent<Image>().color = Color.red;
    }
}
