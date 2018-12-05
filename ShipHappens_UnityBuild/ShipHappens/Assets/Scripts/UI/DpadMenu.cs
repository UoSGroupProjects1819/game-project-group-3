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

    Color dpadSolid;
    Color dpadFaint;
    Color dpadCol;

	void Start ()
    {
        canvasGroup.alpha = 0;

        dpadSolid = dpad.color;
        dpadSolid.a = 0.95f;

        dpadFaint = dpad.color;
        dpadFaint.a = 0.0f;
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

        Debug.Log("dpad alpha value: " + dpad.color.a);
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
}