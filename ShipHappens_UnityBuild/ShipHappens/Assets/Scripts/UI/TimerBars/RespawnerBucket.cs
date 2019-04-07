using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnerBucket : MonoBehaviour
{
    public GameObject bucket;
    BucketStates bucketStates;
    public GameObject spawnPoint;
    public GameObject sea;
    private Rigidbody rb;

    public float timer;
    public float initialTimer;
    public Image clockbase;
    public Image clock;
    public Text text;


    void Start()
    {
        clockbase.enabled = false;
        clock.enabled = false;
        text.enabled = false;
        bucketStates = bucket.GetComponent<BucketStates>();
        rb = bucket.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (bucket.transform.position.y < sea.transform.position.y)
        {
            bucket.SetActive(false);
            bucket.transform.parent = null;
            timer = initialTimer;
            StartCoroutine(DelayRespawn());
        }
    }

    IEnumerator DelayRespawn()
    {
        yield return new WaitForSeconds(1f);

        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        RespawnTimer();
    }

    void RespawnTimer()
    {
        clockbase.enabled = true;
        clock.enabled = true;
        text.enabled = true;

        timer -= Time.deltaTime;
        text.text = timer.ToString("F0");
        clock.fillAmount = timer / initialTimer;

        if (timer <= 0)
        {
            clockbase.enabled = false;
            clock.enabled = false;
            text.enabled = false;
            Respawn();
        }
    }

    void Respawn()
    {
        bucketStates.currentState = BucketStates.BucketState.Dropped;
        bucket.transform.position = spawnPoint.transform.position;
        bucket.transform.rotation = spawnPoint.transform.rotation;
        bucket.SetActive(true);
    }
}
