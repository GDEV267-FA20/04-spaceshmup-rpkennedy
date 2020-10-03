using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float showDamageDuration = 0.1f;

    [Header("All you m8:")]
    public float speed;
    public float fireRate;
    public float health;
    public int score;

    [Header("No worries m8:")]
    public Color[] originalColors;
    public Material[] materials;
    public bool showingDamage;
    public float damageDoneTime;
    public bool notifiedOfDestruction;

    protected BoundsCheck bndCheck;

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
        showingDamage = false;
        notifiedOfDestruction = false;
        bndCheck = GetComponent<BoundsCheck>();
        materials = Utils.GetAllMaterials(gameObject);
        originalColors = new Color[materials.Length];

        for(int i=0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }
    }

    void Update()
    {
        Move();

        if (showingDamage && Time.time > damageDoneTime)
        {
            HideDamage();
        }

        if (bndCheck != null && bndCheck.ofFDown)
        {
            if(pos.y < bndCheck.camHeight - bndCheck.radius)
            {
                Destroy(gameObject);
            }
        }
    }

    public virtual void Move()
    {
        Vector3 temp = pos;
        temp.y -= speed * Time.deltaTime;
        pos = temp;
    }

    void OnCollisionEnter(Collision coll)
    {                           

        GameObject otherGO = coll.gameObject;

        switch (otherGO.tag)
        {
            case "ProjHero":                                      
                Projectile p = otherGO.GetComponent<Projectile>();

                if (!bndCheck.isOnScreen)
                {                      
                    Destroy(otherGO);
                    break;
                }

                health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                ShowDamage();

                if (health <= 0)
                {                    
                    Destroy(this.gameObject);
                }

                Destroy(otherGO);                      
                break;

            default:
                print("Enemy hit by non-ProjectileHero: " + otherGO.name); 
                break;
        }
    }

    void ShowDamage()
    {
        foreach (Material m in materials)
        {
            m.color = Color.red;
        }
        showingDamage = true;
        damageDoneTime = Time.time + showDamageDuration;
    }

    void HideDamage()
    {
        for(int i=0; i<materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
        showingDamage = false;
    }
}
