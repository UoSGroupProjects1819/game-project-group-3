using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrowsNestUI : MonoBehaviour
{
    [Header("Trigger Bubble Animations")]
    public bool playBottom;
    public bool playLeft;
    public bool playRight;
    [Header("Are Bubbles Active")]
    public bool is1active;
    public bool is2active;
    public bool is3active;
    [Header("Next Bubble")]
    public Image nextAvailableBubble;
    public Image nextAvailableBubbleContents;
    [Header("Next Bubble")]
    public bool playNextAvailableBubble;

    [Header("Animators")]
    private Animator leftAnim;
    private Animator rightAnim;
    private Animator bottomAnim;

    [Header("Bubbles")]
    public Image leftBubble, leftBubbleContents;
    public Image bottomBubble, bottomBubbleContents;
    public Image rightBubble, rightBubbleContents;

    [Header("[Hold Timers]")]
    public DpadBarrelTimer barrelTimer;
    public DpadWoodTimer woodTimer;
    public DpadCannonballTimer cballTimer;

    [Header("BubbleContents")]
    public Image ImgChest;
    public Image ImgRocks;
    public Image ImgSeagull;
    public Image ImgWhale;
    public Image ImgEnemy;

    [Header("Tutorial Bubble Contents")]
    public Image ImgPoo;
    public Image ImgCannon;
    public Image ImgFire;
    public Image ImgHole;
    public Image ImgMop;
    public Image ImgDpad;
    public Image ImgWheel;
    public Image ImgBucket;
    public Image ImgTorch;
    public Image ImgWood;
    public Image ImgBarrel;
    public Image ImgCannonBall;

    



    /*
    script checks which bubbles are active, sets the lowest number bubble = nextActiveBubble.

    each <event> must assign the next available bubble contents to corresponding image.
    each <event> smust set bool playNextAvailableBubble to true

    script then plays nextAvailableBubble animation with the assigned content image.

    each bubble has an animation event which will set the animator play bool to false and the 'isNactive' bool to false.
    */


    private void Start()
    {
        leftAnim = leftBubble.GetComponent<Animator>();
        rightAnim = rightBubble.GetComponent<Animator>();
        bottomAnim = bottomBubble.GetComponent<Animator>();
    }

    void Update ()
    {
        CheckActiveBubbles();

        CheckPlayNextAvailableBubble();

        /////////////////////////////////

        if (playBottom)
        {
            bottomAnim.SetBool("PlayBottom", true);
            is1active = true;
        }

        if (playLeft)
        {
            leftAnim.SetBool("PlayLeft", true);
            is2active = true;
        }

        if (playRight)
        {
            rightAnim.SetBool("PlayRight", true);
            is3active = true;
        }
    }

    /// <summary>
    /// Assigns next available bubble.
    /// </summary>
    void CheckActiveBubbles()
    {
        if (is1active)
        {
            if(is2active)
            {
                if (is3active)
                {
                    Debug.LogWarning("All bubbles are active, I won't be displayed!");
                    return;
                }
                else //use bubble 3 (right)
                {
                    nextAvailableBubble = rightBubble;
                    nextAvailableBubbleContents = rightBubbleContents;
                }
            }
            else //use bubble 2 (left)
            {
                nextAvailableBubble = leftBubble;
                nextAvailableBubbleContents = leftBubbleContents;
            }

        }
        else //use bubble 1 (bottom)
        {
            nextAvailableBubble = bottomBubble;
            nextAvailableBubbleContents = bottomBubbleContents;
        }
    }

    /// <summary>
    /// Plays the next available bubble.
    /// </summary>
    void CheckPlayNextAvailableBubble()
    {
        if (playNextAvailableBubble)
        {
            if (nextAvailableBubble == bottomBubble)
            {
                playBottom = true;
                playNextAvailableBubble = false;
            }
            if (nextAvailableBubble == leftBubble)
            {
                playLeft = true;
                playNextAvailableBubble = false;
            }
            if (nextAvailableBubble == rightBubble)
            {
                playRight = true;
                playNextAvailableBubble = false;
            }
        }
        else
            return;
    }
}
