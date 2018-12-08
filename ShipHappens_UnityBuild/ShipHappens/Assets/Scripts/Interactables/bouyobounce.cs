using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouyobounce : MonoBehaviour
{
    public GameObject[] things;

	void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            for (int i = 0; i < things.Length; i++)
            {
                float x = Random.RandomRange(-1f, 1f);
                float y = Random.RandomRange(0.2f, 1.2f);
                float z = Random.RandomRange(0.1f, 0.4f);

                things[i].GetComponent<Rigidbody>().AddForce(new Vector3(x, y, z), ForceMode.Impulse);
            }
        }
    }
}
