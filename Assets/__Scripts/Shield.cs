using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("All you m8")]
    public float rotationsPerSecond;

    private int levelShown = 0;
    Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;    
    }

    void Update()
    {
        int currLevel = Mathf.FloorToInt(HeroContr.S.shieldLevel);

        if (levelShown != currLevel)
        {
            levelShown = currLevel;
            mat.mainTextureOffset = new Vector2(0.2f * levelShown, 0);
        }

        float rZ = -(rotationsPerSecond * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }
}
