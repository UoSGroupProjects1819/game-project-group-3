using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject gameObject;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(gameObject, transform.position, transform.rotation); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
