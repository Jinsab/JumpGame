using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public Text scoreCount;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("score : " + score);
        }

        scoreCount.text = score.ToString();
    }

    public void SetScore(int value) => score += value;

    public int GetScore() => score;
}