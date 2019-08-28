using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public GameObject player;

    Vector3 StartingPos;
    Quaternion StartingRotate;
    bool isStarted = false;
    static bool isEnded = false;

    public static int stageLevel = 0;

    void Start()
    {
        StartingPos = GameObject.FindGameObjectWithTag("Start").transform.position;
        StartingRotate = GameObject.FindGameObjectWithTag("Start").transform.rotation;
    }

    public void NextStage()
    {
        //Time.timeScale = 0f;
       
        stageLevel++;

        SceneManager.LoadScene(stageLevel+1, LoadSceneMode.Single);
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        SceneManager.LoadScene(stageLevel, LoadSceneMode.Single);
    }
}
