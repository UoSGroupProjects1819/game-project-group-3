﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomBubbleEnd : MonoBehaviour
{
    private Animator bottomAnimator;
    public CrowsNestUI CNui;

    void Start ()
    {
        bottomAnimator = GetComponent<Animator>();
	}
	

	void StopBottomBubbleAnim()
    {
        CNui.playBottom = false;
        bottomAnimator.SetBool("PlayBottom", false);
        Debug.Log("YO DUDE");
    }
}