using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text scoreText, livesText, boardText, homePercentage;
    public Slider homeSlider;

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


    /// <summary>
    /// Sets the value from 1 to 100 in integers.
    /// </summary>
    /// <param name="value"></param>
    public void UpdateHomePercentage(int value)
    {        
        homePercentage.text = value.ToString() + "%";
        homeSlider.value = value / 100f;
    }

    /// <summary>
    /// Sets the value from 0.0f to 1.0f
    /// </summary>
    /// <param name="value"></param>
    public void UpdateHomePercentage(float value)
    {
        float aux1 = value * 100;
        int aux2 = (int)aux1;
        homePercentage.text = aux2.ToString() + "%";
        homeSlider.value = value;
    }

    /// <summary>
    /// Reduces the value from 1 to 100
    /// </summary>
    /// <param name="value"></param>
    public void ReduceHomePercentage(int value)
    {
        int aux = (int)homeSlider.value;
        aux += value;
        homePercentage.text = aux.ToString() + "%";
        homeSlider.value = aux / 100f;
    }

    /// <summary>
    /// Reduces the value from 0f to 1f
    /// </summary>
    /// <param name="value"></param>
    public void ReduceHomePercentage(float value)
    {
        int aux = (int)(value+ homeSlider.value);
        aux *= 100;
        homePercentage.text = aux.ToString() + "%";
        homeSlider.value += value;
    }
}
