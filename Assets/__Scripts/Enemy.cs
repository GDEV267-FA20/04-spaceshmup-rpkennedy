using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("All you m8:")]
    public float speed;
    public float fireRate;
    public float health;
    public int score;

    private BoundsCheck bndCheck;

    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }
    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    void Update()
    {
        Move();

        if (bndCheck != null && bndCheck.ofFDown)
        {
            if(pos.y < bndCheck.camHeight - bndCheck.radius)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject collGO = coll.gameObject;

        if( collGO.tag == "ProjHero")
        {
            Destroy(collGO);
            Destroy(gameObject);
        }
    }

    public virtual void Move()
    {
        Vector3 temp = pos;
        temp.y -= speed * Time.deltaTime;
        pos = temp;
    }
}
