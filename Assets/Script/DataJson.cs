using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class Stage
{
    public string StageNumber;
    public int Score;
    public int Star;

    public Stage(string stageNumber, int score, int star)
    {
        StageNumber = stageNumber;
        Score = score;
        Star = star;
    }
}

public class DataJson : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
