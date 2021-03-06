﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    [Header("All you m8:")]
    public float radius;
    public bool keepOnScreen;

    [Header("No worries m8:")]
    public bool isOnScreen;
    public float camWidth;
    public float camHeight;

    [HideInInspector]
    public bool offRight, offLeft, offUp, ofFDown;

    void Awake()
    {
        isOnScreen = true;
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;        
    }
    
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        isOnScreen = true;
        offRight = offLeft = offUp = ofFDown = false;

        if (pos.x > camWidth - radius)
        {
            pos.x = camWidth - radius;
            isOnScreen = false;
            offRight = true;
        }

        if (pos.x < -camWidth + radius)
        {
            pos.x = -camWidth + radius;
            isOnScreen = false;
            offLeft = true;
        }

        if (pos.y > camHeight - radius)
        {
            pos.y = camHeight - radius;
            isOnScreen = false;
            offUp = true;
        }

        if (pos.y < -camHeight + radius)
        {
            pos.y = -camHeight + radius;
            isOnScreen = false;
            ofFDown = true;
        }

        if(keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;
            offRight = offLeft = offUp = ofFDown = false;
        }        
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
}
