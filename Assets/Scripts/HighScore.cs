using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public void showHighScore(List<int> highScore)
    {
        for (int i = 0; i < Math.Min(highScore.Count,4) ; i++)
        {
            this.transform.Find((i+1).ToString()).GetComponent<TMP_Text>().text = highScore[i].ToString();
        }
    }
}
