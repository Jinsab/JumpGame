using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    public InputField inputScore;

    public void Save()
    {
        PlayerPrefs.SetInt("Score", int.Parse(inputScore.text));
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            inputScore.text = PlayerPrefs.GetInt("Score").ToString();
        }
    }
}
