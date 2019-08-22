using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    float count = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            count = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            if (Time.time - count > 2)
                Restart();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Stage1");
    }
}
