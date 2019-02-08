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
    public Sprite nextAvailableBubbleContents;
    //public Sprite nextAvailableBubbleContents;
    public bool playNextAvailableBubble;

    private Animator leftAnim;
    private Animator rightAnim;
    private Animator bottomAnim;

    [Header("Bubbles")]
    public Image leftBubble, leftBubbleContent;
    public Sprite leftBubbleContents;
    public Image bottomBubble, bottomBubbleContent;
    public Sprite bottomBubbleContents;
    public Image rightBubble, rightBubbleContent;
    public Sprite rightBubbleContents;

    [Header("[Hold Timers]")]
    public DpadBarrelTimer barrelTimer;
    public DpadWoodTimer woodTimer;
    public DpadCannonballTimer cballTimer;

    [Header("BubbleContents")]
    public Sprite ImgChest;
    public Sprite ImgRocks;
    public Sprite ImgSeagull;
    public Sprite ImgWhale;
    public Sprite ImgEnemy;

    [Header("Tutorial Bubble Contents")]
    public Sprite ImgPoo;
    public Sprite ImgCannon;
    public Sprite ImgFire;
    public Sprite ImgHole;
    public Sprite ImgMop;
    public Sprite ImgDpad;
    public Sprite ImgWheel;
    public Sprite ImgBucket;
    public Sprite ImgTorch;
    public Sprite ImgWood;
    public Sprite ImgBarrel;
    public Sprite ImgCannonBall;

   

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

        leftBubbleContents = leftBubbleContent.GetComponent<Image>().sprite;
        bottomBubbleContents = bottomBubbleContent.GetComponent<Image>().sprite;
        rightBubbleContents = rightBubbleContent.GetComponent<Image>().sprite;
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
                    //nextAvailableBubbleContents.sprite = rightBubbleContents;
                    //nextAvailableBubble = rightBubble;
                    //rightBubbleContents = nextAvailableBubbleContents;
                }
            }
            else //use bubble 2 (left)
            {
                nextAvailableBubble = leftBubble;
                //nextAvailableBubbleContents.sprite = leftBubbleContents;
                //nextAvailableBubble = leftBubble;
                //leftBubbleContents = nextAvailableBubbleContents;
            }

        }
        else //use bubble 1 (bottom)
        {
            //bottomBubbleContents = nextAvailableBubbleContents;
            nextAvailableBubble = bottomBubble;
            //nextAvailableBubbleContents.sprite = bottomBubbleContents;
            //nextAvailableBubble = bottomBubble;

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
                bottomBubbleContents = nextAvailableBubbleContents;
                playBottom = true;
                //playNextAvailableBubble = false;
            }
            if (nextAvailableBubble == leftBubble)
            {
                leftBubbleContents = nextAvailableBubbleContents;
                playLeft = true;
                //playNextAvailableBubble = false;
            }
            if (nextAvailableBubble == rightBubble)
            {
                rightBubbleContents = nextAvailableBubbleContents;
                playRight = true;
                //playNextAvailableBubble = false;
            }

            playNextAvailableBubble = false;
        }
        else
            return;
    }
}
