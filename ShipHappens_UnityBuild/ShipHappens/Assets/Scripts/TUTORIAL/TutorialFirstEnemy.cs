using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialFirstEnemy : MonoBehaviour
{
    public TutorialManager tm;

    public ParticleSystem explode;

    public float speed;
    private bool shouldMove = true;

    private void Start()
    {
        tm = GameObject.FindWithTag("tm").GetComponent<TutorialManager>();

        if (tm == null)
            Debug.Log("WE DO NOT HAVE TM!");
    }

    void Update()
    {
        if (shouldMove)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void OnColisioEnter(Collision collision)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "TutorialFirstEnemyStopper")
        {
            shouldMove = false;
            tm.bubbleHasPlayed = false;
            tm.stage++;
            Destroy(other.gameObject);
        }
    }
}
