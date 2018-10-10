using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public Text badnessText;
    public float badness;

    void Start()
    {
        badness = 0;
    }


    void Update()
    {
        badnessText.text = "Water: " + badness;
    }
}
