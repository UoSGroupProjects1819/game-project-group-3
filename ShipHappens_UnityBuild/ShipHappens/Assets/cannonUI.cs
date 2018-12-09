using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cannonUI : MonoBehaviour
{
    public Image cannonball, gunpowder, torch, cannonballBackground, gunpowderBackground;
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
                torch.enabled = true;
                cannonball.enabled = false;
                gunpowder.enabled = false;
                break;

        }
	}

    private void Reset()
    {
        torch.enabled = false;
        cannonball.enabled = true;
        gunpowder.enabled = true;

        cannonball.GetComponent<Image>().sprite = cannonballSprite;
        gunpowder.GetComponent<Image>().sprite = gunpowderSprite;
    }
}
