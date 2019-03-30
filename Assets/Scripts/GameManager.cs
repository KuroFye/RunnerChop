﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public float gameSpeed = -0.05f, gameSpeedIncreaseDelay = 5f, speedMultiplier = 1.01f, gameStartDelay = 4f, playerRestartDelay = 2f, playerRestartInvulnerability = 2f, returnToSelectionDelay = 6f;
    float baseGameSpeed = 0f, scoreMultiplier = 1f;
    [HideInInspector]
    public float totalSpeedMultiplier, originalSpeed;
    [HideInInspector]
    public bool playerInvulnerable = false;
    [HideInInspector]
    public GameObject playerStart;
    [HideInInspector]
    public PlayerController playerRef;


    int score = 0, lives = 4;
    [HideInInspector]
    public int difficulty = 1;


    PlayerUI UIref;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
        baseGameSpeed = gameSpeed;
        gameSpeed = 0f;
        playerRef.gameObject.transform.position = playerStart.transform.position;
        playerRef.gameObject.GetComponent<Locomotion>().SetStartingPosition(playerStart.transform.position);
        playerStart.SetActive(false);

        if(difficulty == 1)
        {
            gameSpeed = -0.05f;
            speedMultiplier = 1.01f;
            scoreMultiplier = 1f;
            gameSpeedIncreaseDelay = 5f;
        }

        if(difficulty == 2)
        {
            gameSpeed = -0.075f;
            speedMultiplier = 1.02f;
            scoreMultiplier = 2f;
            gameSpeedIncreaseDelay = 4f;
        }

        if(difficulty == 3)
        {
            gameSpeed = -0.1f;
            speedMultiplier = 1.03f;
            scoreMultiplier = 3f;
            gameSpeedIncreaseDelay = 3.5f;
        }

        SetLives(4);
        SetScore(0);
        totalSpeedMultiplier = speedMultiplier;
        originalSpeed = gameSpeed;
        StartCoroutine("GameStartCountdown");
    }

    public void SetUIRef(PlayerUI uiref)
    {
        UIref = uiref;
    }

    public void SetScore(int newScore)
    {
        UIref.UpdateScore(newScore);
        score = newScore;
    }

    public void AddScore(int score)
    {
        this.score += (int)(score*scoreMultiplier);
        UIref.UpdateScore(this.score);
    }

    public void SetLives(int newLives)
    {
        UIref.UpdateLives(newLives);
        lives = newLives;
    }

    public void AddLives(int lives)
    {
        this.lives += lives;
        UIref.UpdateLives(this.lives);
    }

    public void SetDifficulty(int level)
    {
        difficulty = level;
    }

    private IEnumerator GameStartCountdown()
    {
        float normalizedTime = 0;
        while (normalizedTime <= gameStartDelay)
        {
            normalizedTime += Time.deltaTime;
            if(normalizedTime <= 1f)
            {
                UIref.boardText.text = "3..";
            }else if(normalizedTime <= 2f)
            {
                UIref.boardText.text = "2..";
            }
            else if (normalizedTime <= 3f)
            {
                UIref.boardText.text = "Ready?";
            }else if (normalizedTime <= 4f)
            {
                UIref.boardText.text = "Go!";
            }
            yield return null;
        }
        UIref.boardText.text = "";
        gameSpeed = baseGameSpeed;
        StartCoroutine("SpeedIncreaseCountdown");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name.Equals("RunnerScene"))
        {
            StartGame();
        }
    }

    public void PlayerDeath()
    {
        lives--;
        if (lives > 0)
        {
            SetLives(lives);
            baseGameSpeed = gameSpeed;
            gameSpeed = 0f;
            playerInvulnerable = true;
            StopCoroutine("SpeedIncreaseCountdown");
            StopCoroutine("PlayerInvulnerabilityCountdown");
            StartCoroutine("PlayerResetCountdown");
        }
        else
        {
            UIref.boardText.text = "Game ended! \nYou scored: "+score.ToString();
            StartCoroutine("ReturnToSelectionCountdown");
        }
    }

    private IEnumerator PlayerResetCountdown()
    {
        float normalizedTime = 0;
        while (normalizedTime <= playerRestartDelay)
        {
            normalizedTime += Time.deltaTime;
            if (normalizedTime <= 1f)
            {
                UIref.boardText.text = "Ready?";
            }
            else if (normalizedTime <= 2f)
            {
                UIref.boardText.text = "Go!";
            }
            yield return null;
        }

        UIref.boardText.text = "";        
        playerRef.gameObject.SetActive(true);
        playerRef.gameObject.transform.position = playerStart.transform.position;
        
        gameSpeed = baseGameSpeed;
        Physics.IgnoreLayerCollision(9, 10, false);
        StartCoroutine("PlayerInvulnerabilityCountdown");
        StartCoroutine("SpeedIncreaseCountdown");
    }

    private IEnumerator PlayerInvulnerabilityCountdown()
    {
        float normalizedTime = 0;
        while (normalizedTime <= playerRestartInvulnerability)
        {
            normalizedTime += Time.deltaTime;
            yield return null;
        }
        playerInvulnerable = false;
    }


    private IEnumerator ReturnToSelectionCountdown()
    {
        float normalizedTime = 0;
        while (normalizedTime <= returnToSelectionDelay)
        {
            normalizedTime += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene("SampleScene");
    }

    private IEnumerator SpeedIncreaseCountdown()
    {
        float normalizedTime = 0;
        while (normalizedTime <= gameSpeedIncreaseDelay)
        {
            normalizedTime += Time.deltaTime;
            yield return null;
        }
        gameSpeed *= speedMultiplier;
        totalSpeedMultiplier = (gameSpeed/originalSpeed);
        StartCoroutine("SpeedIncreaseCountdown");
    }


}
