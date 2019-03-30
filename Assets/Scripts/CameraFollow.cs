using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float xDistanceOffset = 0f, yDistanceOffset = 1.6f, zDistanceOffset = -6f;
    private Transform m_Player;


    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(m_Player.position.x + xDistanceOffset, m_Player.position.y + yDistanceOffset, m_Player.position.z + zDistanceOffset);
    }
}
