using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollecting : MonoBehaviour
{
    bool collecting = false;

    // Update is called once per frame
    void Update()
    {
        if (collecting)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        collecting = true;
    }
}
