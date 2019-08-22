using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollecting : MonoBehaviour
{
    bool collecting = false;
    int s;
    ScoreManager Manager;

    void Start()
    {
        Manager = GameObject.Find("Player").GetComponent<ScoreManager>();
        s = Manager.score;
    }

    void Update()
    {
        if (collecting)
        {
            Destroy(gameObject);
            Manager.score++;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            collecting = true;
    }
}
