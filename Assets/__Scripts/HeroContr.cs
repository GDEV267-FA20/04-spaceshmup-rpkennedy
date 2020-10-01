using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroContr : MonoBehaviour
{
    static public HeroContr S;

    [Header("All you m8:")]
    public float speed;
    public float rollMult;
    public float pitchMult;
    public float shieldLevel;

    void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.Log("Hero.Awake() - S already exists");
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);
    }
    void FixedUpdate()
    {
        
    }
}
