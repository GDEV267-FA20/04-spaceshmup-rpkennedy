using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_4 : Enemy
{
    [Header("Set in Inspector: Enemy_4")]   
    private Vector3 p0, p1;     
    private float timeStart;    
    private float duration = 4;
    public GameObject left;
    public GameObject right;
    public GameObject mid;
    public float newHealth;

    void Start()
    {
        p0 = p1 = pos;            
        InitMovement();

        Transform t;        
    }

    void InitMovement()
    {                                        
        p0 = p1; 
        float widMinRad = bndCheck.camWidth - bndCheck.radius;
        float hgtMinRad = bndCheck.camHeight - bndCheck.radius;

        p1.x = Random.Range(-widMinRad, widMinRad);
        p1.y = Random.Range(-hgtMinRad, hgtMinRad);
        timeStart = Time.time;
    }
          
    public override void Move()
    {                                           
        float u = (Time.time - timeStart) / duration;

        if (u >= 1)
        {
            InitMovement();
            u = 0;
        }

        u = 1 - Mathf.Pow(1 - u, 2);    
        pos = (1 - u) * p0 + u * p1;    
    }
     
    void ShowLocalizedDamage(Material m)
    {                                  
        m.color = Color.red;
        damageDoneTime = Time.time + showDamageDuration;
        showingDamage = true;
    }

    void OnCollisionEnter(Collision coll)
    {                              
        GameObject other = coll.gameObject;
        switch (other.tag)
        {
            case "ProjHero":
                Projectile p = other.GetComponent<Projectile>();

                if (!bndCheck.isOnScreen)
                {
                    Destroy(other);
                    break;
                }
                bool allDestroyed = false;

                newHealth -= Main.GetWeaponDefinition(p.type).damageOnHit;

                if (newHealth <= 9)
                {
                    right.SetActive(false);
                    mid.SetActive(true);
                    left.SetActive(true);
                }
                if (newHealth <= 5)
                {
                    left.SetActive(false);
                    mid.SetActive(true);
                }
                if (newHealth <= 0) allDestroyed = true;


                if (allDestroyed)
                { 
                    Main.S.ShipDestroyed(this);
                    Destroy(this.gameObject);
                }

                Destroy(other);  
                break;
        }
    }
}
