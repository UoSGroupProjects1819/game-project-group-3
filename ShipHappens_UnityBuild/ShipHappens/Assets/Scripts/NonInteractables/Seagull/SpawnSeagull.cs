using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSeagull : MonoBehaviour
{
    public GameObject shipCentre;
    public GameObject seagull;
    public GameObject pooPrefab;
    public float speed;
    public float radius;
    public float maxDistance;


    //spawn around centre position
    //rotate towards centre
    //move forward

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            HatchThatEgg();
        }
    }

    void HatchThatEgg()
    {
        Vector3 center = shipCentre.transform.position;
        Vector3 pos = RandomCircle(center, radius);
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
        Instantiate(seagull, pos, rot);
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x - radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z - radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

    //if ((randomPos.position - centrePoint.position).sqrMagnitude <= radius* radius )
    //{ // retry }
}
