using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollecting : MonoBehaviour
{
    bool collecting = false;
    public int score;
    ScoreManager Manager;

    void Start()
    {
        Manager = GameObject.Find("Player").GetComponent<ScoreManager>();
    }

    void Update()
    {
        if (collecting)
        {
            Destroy(gameObject);
            Manager.SetScore(score);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            collecting = true;
    }
}
