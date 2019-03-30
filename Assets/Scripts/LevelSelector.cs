using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public int difficultyLevel;
    public Slider difficultySlider;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("RunnerScene");
    }

    public void SetDifficulty()
    {
        difficultyLevel = (int)difficultySlider.value;
        GameManager.instance.SetDifficulty(difficultyLevel);
    }
}
