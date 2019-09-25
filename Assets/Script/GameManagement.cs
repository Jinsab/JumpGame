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
    public StageStatus status;
    public static int stageLevel = 0;

    void Start()
    {
        if (player != null)
        {
            StartingPos = GameObject.FindGameObjectWithTag("Start").transform.position;
            StartingRotate = GameObject.FindGameObjectWithTag("Start").transform.rotation;
        }

        SetStage(status.getStage());
    }

    private void Update()
    {

    }
    public void NextStage()
    {
        //Time.timeScale = 0f;
       
        stageLevel++;

        SceneManager.LoadScene(stageLevel+3, LoadSceneMode.Single);
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Selection()
    {
        SceneManager.LoadScene("Selection");
    }

    public void Restart()
    {
        SceneManager.LoadScene(stageLevel+2, LoadSceneMode.Single);
    }

    public void Option()
    {
        SceneManager.LoadScene("Option");
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
            Application.OpenURL("http://google.com");
        #else
            Application.Quit();
        #endif
    }

    public int GetStage()
    {
        return stageLevel;
    }

    public void SetStage(int stage)
    {
        stageLevel = stage;
    }

}
