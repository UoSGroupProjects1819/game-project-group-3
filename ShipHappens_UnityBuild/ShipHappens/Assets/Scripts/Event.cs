using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Event : MonoBehaviour
{
    public float duration;
    public float coolDown;

    public abstract void Spawn();
}
