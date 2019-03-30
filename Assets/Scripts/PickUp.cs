using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int scoreGiven = 10;
    public int livesGiven = 0;
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
            gameObject.SetActive(false);

            if (scoreGiven != 0)
            {
                GameManager.instance.AddScore(scoreGiven);
            }

            if (livesGiven != 0)
            {
                GameManager.instance.AddLives(livesGiven);
            }
        }
    }

}
