using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DpadMenu : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public RawImage dpad;
    //public Image woodImg;
    //public Image cannonballImg;
    //public Image gunpowderImg;

    public int menuCount;
    public bool inMenu;

    Color dpadSolid;
    Color dpadFaint;
    Color dpadCol;


	void Start ()
    {
        dpadSolid = dpad.color;
        dpadSolid.a = 0.95f;

        dpadFaint = dpad.color;
        dpadFaint.a = 0.55f;
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
            canvasGroup.alpha = Mathf.Lerp(0.95f, 0.55f, 0.1f);
        }
        else
        {
            inMenu = false;
            canvasGroup.alpha = Mathf.Lerp(0.55f, 0.95f, 0.1f);
        }

        Debug.Log("dpad alpha value: " + dpad.color.a);
    }
}
