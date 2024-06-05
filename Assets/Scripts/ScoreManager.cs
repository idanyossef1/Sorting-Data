using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static private ScoreManager instance;

    private void Start()
    {
        instance = this;
    }

    public static void Init()
    {
        
    }

    public static bool IsHighScore (int score)
    {
        return false;
    }

    public static void SetHighScore (int score)
    {
        
    }

}
