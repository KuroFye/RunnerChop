using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool skillsOnCooldown = false;

    void Awake()
    {
        GameManager.instance.playerRef = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDisable()
    {
        GameManager.instance.PlayerDeath();
        Debug.Log("player disabled");
    }

    
}
