using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Must be implemented by all interactables
    public virtual void Action(GameObject player)
    {
        Debug.Log("Action pressed");
    }
}
