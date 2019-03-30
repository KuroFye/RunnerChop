using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text scoreText, livesText, boardText;

    void Awake()
    {
        GameManager.instance.SetUIRef(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateLives(int lives)
    {
        livesText.text = "Lives: " + lives.ToString();
    }
}
