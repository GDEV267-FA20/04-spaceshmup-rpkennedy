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
    public float gameRestartDelay;
    public GameObject projPrefab;
    public float projSpeed;
    public Weapon[] weapons;

    [Header("Idk man")]
    [SerializeField]
    private float _shieldLevel;

    private GameObject lastTriggerGo = null;

    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;

    void Start()
    {
        if (S == null)   S = this;

        ClearWeapons();
        weapons[0].SetType(WeaponType.blaster);
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

        if (Input.GetAxis("Jump") == 1 && fireDelegate != null)
        {           
            fireDelegate();   
        }
    }
    
    void OnTriggerEnter(Collider coll)
    {
        Transform rootT = coll.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        if (go == lastTriggerGo) return;
        
        lastTriggerGo = go;

        if (go.tag == "Enemy")
        {
            shieldLevel--;
            Destroy(go);
        }
        else if (go.tag == "PowerUp") 
        {
            AbsorbPowerUp(go);
        }
        else
        {
            print("Triggered by non-Enemy: " + go.name);      
        }
    }

    public float shieldLevel
    {
        get
        {
            return (_shieldLevel);
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);

            if (value < 0)
            {
                Destroy(this.gameObject);
                Main.S.DelayedRestart(gameRestartDelay);
            }
        }
    }

    public void AbsorbPowerUp(GameObject go)
    {
        PowerUp pu = go.GetComponent<PowerUp>();
        switch (pu.type)
        {
            case WeaponType.shield:                                          
                shieldLevel++;
                break;

            default:                                                     
                if (pu.type == weapons[0].type)
                { 
                    Weapon w = GetEmptyWeaponSlot();

                    if (w != null)
                    { 
                        w.SetType(pu.type);
                    }
                }
                else
                { 
                    ClearWeapons();
                    weapons[0].SetType(pu.type);
                }
                break;
        }
        pu.AbsorbedBy(this.gameObject);
    }

    Weapon GetEmptyWeaponSlot()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].type == WeaponType.none)   return (weapons[i]);
        }
        return (null);
    }

    void ClearWeapons()
    {
        foreach (Weapon w in weapons)
        {
            w.SetType(WeaponType.none);
        }
    }
}
