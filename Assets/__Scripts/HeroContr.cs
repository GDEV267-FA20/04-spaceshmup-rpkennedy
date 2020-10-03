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

    [Header("Idk man")]
    [SerializeField]
    private float _shieldLevel;

    private GameObject lastTriggerGo = null;

    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;

    void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        fireDelegate += TempFire;
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

        if ((Input.GetAxis("Jump") == 1 || Input.GetMouseButtonDown(0)) && fireDelegate != null)
        {           
            fireDelegate();   
        }
    }

    void TempFire()
    {
        GameObject projGO = Instantiate<GameObject>(projPrefab);
        projGO.transform.position = transform.position;
        Rigidbody rb = projGO.GetComponent<Rigidbody>();
        rb.velocity = Vector3.up * projSpeed;

        Projectile proj = projGO.GetComponent<Projectile>();    
        proj.type = WeaponType.blaster;
        float tSpeed = Main.GetWeaponDefinition(proj.type).velocity;
        rb.velocity = Vector3.up * tSpeed;
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
}
