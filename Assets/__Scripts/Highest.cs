using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Highest : MonoBehaviour
{
    public int cap;
    public TMP_Text hs;
    public GameObject scoreGO;
    public Score score;

    void Awake()
    {
        TMP_Text hs = GetComponent<TMP_Text>();
        score = scoreGO.GetComponent<Score>();

        if (PlayerPrefs.HasKey("HS") && PlayerPrefs.GetInt("HS") > 1000)
        {
            cap = PlayerPrefs.GetInt("HS");
        }
        else
        {
            cap = 1000;
        }

        PlayerPrefs.SetInt("HS", cap);
    }

    void Update()
    {        
        hs.text = "Highest: " + cap;

        if (score.total > PlayerPrefs.GetInt("HS"))
        {
            PlayerPrefs.SetInt("HS", score.total);
            cap = score.total;
        }
    }
}
