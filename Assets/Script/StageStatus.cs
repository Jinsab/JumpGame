using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageStatus : MonoBehaviour
{
    public int stage;
    public int score;

    public void Stage_Choose()
    {
        SceneManager.LoadScene(stage+2, LoadSceneMode.Single);
    }
}
