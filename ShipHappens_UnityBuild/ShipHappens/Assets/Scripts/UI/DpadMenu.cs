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

    //float startTime;
    //public float fadeSpeed;

    //Color alpha;


	void Start ()
    {
        //alpha = canvasGroup.GetComponent<Renderer>().material.color;
        //startTime = Time.deltaTime;

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

            //float time = (Time.deltaTime - startTime) * fadeSpeed;
            //alpha = Color.Lerp(dpadSolid, dpadFaint, time);

            //canvasGroup.alpha = Mathf.Lerp(0.95f, 0.0f, 1f * Time.deltaTime);
        }
        else
        {
            inMenu = false;

            StartCoroutine("FadeOut");

            //float time = (Time.deltaTime - startTime) * fadeSpeed;
            //alpha = Color.Lerp(dpadFaint, dpadSolid, time);

            //canvasGroup.alpha = Mathf.Lerp(0.0f, 0.95f, 1f * Time.deltaTime);
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