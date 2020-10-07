using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highest : MonoBehaviour
{
    public int score;

    void Awake()
    {
        if(PlayerPrefs.HasKey("HS") && PlayerPrefs.GetInt("HS") > 1000)
        {
            score = PlayerPrefs.GetInt("HS");
        }
        else
        {
            score = 1000;
        }
    }

    void Update()
    {
        
    }
}
