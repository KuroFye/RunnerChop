﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    Locomotion locomotionRef;
    bool ignoringLayer = false;
    // Start is called before the first frame update
    void Start()
    {
        locomotionRef = GetComponent<Locomotion>();
        Physics.IgnoreLayerCollision(9, 10, true);
        ignoringLayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Input.GetButton("Crouch"))
        {
            if (locomotionRef.m_IsGrounded && locomotionRef.m_IsGroundOneWay)
            {
                Physics.IgnoreLayerCollision(9, 10, true);
                ignoringLayer = true;
            }
        }
        
    }

    
    private void OnTriggerStay(Collider other)
    {
        if (!ignoringLayer)
        {
            if (other.tag.Equals("OneWayPlatformTrigger"))
            {
                Physics.IgnoreLayerCollision(9, 10, true);
                ignoringLayer = true;
            }
        }        
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("OneWayPlatformTrigger"))
        {
            if(other.gameObject.transform.position.y < gameObject.transform.position.y)
            {
                Physics.IgnoreLayerCollision(9, 10, false);
                ignoringLayer = false;
            }
            else
            {
                Physics.IgnoreLayerCollision(9, 10, true);
                ignoringLayer = true;
            }
            
        }
    }
}
