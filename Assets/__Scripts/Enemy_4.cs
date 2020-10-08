using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Part
{
    public string name;
    public float phealth;
    public string[] protectedBy;
    
    [HideInInspector]
    public GameObject go;
    public Material mat;
}

public class Enemy_4 : Enemy
{
    [Header("Set in Inspector: Enemy_4")]   
    public Part[] parts;

    private Vector3 p0, p1;     
    private float timeStart;    
    private float duration = 4;
    public GameObject left;
    public GameObject right;
    public GameObject mid;

    void Start()
    {
        p0 = p1 = pos;            
        InitMovement();

        Transform t;
        foreach (Part prt in parts)
        {
            t = transform.Find(prt.name);

            if (t != null)
            {
                prt.go = t.gameObject;
                prt.mat = prt.go.GetComponentInChildren<Renderer>().material;
            }
        }
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

    Part FindPart(string n)
    {                                   
        foreach (Part prt in parts)
        {
            if (prt.name == n) return (prt);
        }
        return (null);
    }
    
    Part FindPart(GameObject go)
    {                               
        foreach (Part prt in parts)
        {
            if (prt.go == go) return (prt);
        }
        return (null);
    }
    
    bool Destroyed(GameObject go)
    {                                 
        return (Destroyed(FindPart(go)));
    }


    bool Destroyed(string n)
    {
        return (Destroyed(FindPart(n)));
    }

    bool Destroyed(Part prt)
    {
        if (prt == null) return (true);   
        return (prt.phealth <= 0);
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

                if (health <= 8)
                {
                    right.SetActive(false);
                    mid.SetActive(true);
                    left.SetActive(true);
                }
                else if (health <= 4)
                {
                    left.SetActive(false);
                    mid.SetActive(true);
                }
                else if (health <= 0) allDestroyed = true;


                /*
                if (coll.collider.gameObject.CompareTag("l"))
                {
                    prtHit = FindPart("Left");

                    if (prtHit.phealth > 0)
                    {
                        prtHit.phealth -= Main.GetWeaponDefinition(p.type).damageOnHit;
                        ShowLocalizedDamage(prtHit.mat);
                    }
                    if (prtHit.phealth <= 0)
                    {
                        prtHit.go.SetActive(false);
                    }
                }
                else if (coll.collider.gameObject.CompareTag("r"))
                {
                    prtHit = FindPart("Right");

                    if (prtHit.phealth > 0)
                    {
                        prtHit.phealth -= Main.GetWeaponDefinition(p.type).damageOnHit;
                        ShowLocalizedDamage(prtHit.mat);
                    }
                    if (prtHit.phealth <= 0)
                    {
                        prtHit.go.SetActive(false);
                    }
                }
                else if (coll.collider.gameObject.CompareTag("m"))
                {
                    prtHit = FindPart("Mid");

                    if(FindPart("Right").phealth > 0 && FindPart("Left").phealth > 0)
                    {
                        if (prtHit.phealth > 0)
                        {
                            prtHit.phealth -= Main.GetWeaponDefinition(p.type).damageOnHit;
                            ShowLocalizedDamage(prtHit.mat);
                        }
                        if (prtHit.phealth <= 0)
                        {
                            prtHit.go.SetActive(false);
                        }
                    }                    
                }                  */
                             

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
