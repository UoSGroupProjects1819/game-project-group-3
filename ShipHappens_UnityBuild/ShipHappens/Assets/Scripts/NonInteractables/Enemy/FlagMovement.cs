using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagMovement : MonoBehaviour
{
    public float speed;

    private void OnDestroy()
    {
        EventManager.GetInstance().RemoveTask("Enemy");
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
