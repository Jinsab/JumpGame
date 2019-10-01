using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class Stage
{
    public int StageNumber;
    public int Score;
    public int Star;

    public Stage(int stageNumber, int score, int star)
    {
        StageNumber = stageNumber;
        Score = score;
        Star = star;
    }
}

public class DataJson : MonoBehaviour
{
    public List<Stage> StageList = new List<Stage>();

    void Start()
    {
        //StageList.Add(new Stage(1, 51, 3));
    }

    public void Save()
    {
        Debug.Log("저장하기");

        JsonData StageJson = JsonMapper.ToJson(StageList);

        File.WriteAllText(Application.dataPath + "/Stagedata.json", StageJson.ToString());
    }

    public void Load()
    {
        Debug.Log("불러오기");

        string JsonString = File.ReadAllText(Application.dataPath + "/Resources/ItemData.json");

        Debug.Log(JsonString);

        JsonData StageData = JsonMapper.ToObject(JsonString);

        for (int i = 0; i < StageData.Count; i++)
        {
            Debug.Log(StageData[i]["StageNumber"].ToString());
            Debug.Log(StageData[i]["Score"].ToString());
            Debug.Log(StageData[i]["Star"].ToString());
        }
    }
}
