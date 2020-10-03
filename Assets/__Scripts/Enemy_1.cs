using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    [Header("All you m8:")]
    public float waveFrequency;
    public float waveWidth;
    public float waveRotY;

    private float xPos;
    private float birthTime;

    void Start()
    {
        xPos = pos.x;
        birthTime = Time.time;
    }

    public override void Move()
    {
        Vector3 temp = pos;
        float age = Time.time - birthTime;

        float theta = Mathf.PI * 2 * age / waveFrequency;
        float sin = Mathf.Sin(theta);

        pos = temp;

        Vector3 rot = new Vector3(0, sin * waveRotY, 0);
        this.transform.rotation = Quaternion.Euler(rot);

        base.Move();
    }
    void Update()
    {
        
    }
}
