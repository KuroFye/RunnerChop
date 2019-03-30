using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceDamagePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if(!GameManager.instance.playerInvulnerable)
                other.gameObject.SetActive(false);            
        }
    }
}
