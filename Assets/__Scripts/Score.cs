using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public int total;
    public TMP_Text score;

    void Awake()
    {
        score = this.GetComponent<TMP_Text>();
        total = 0;
    }

    void Update()
    {
        score.text = "Score: " + total;
    }

    public void AddScore(int val)
    {
        total += val;
    } 
}
