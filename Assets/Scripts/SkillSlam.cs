﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlam : MonoBehaviour
{
    public CapsuleCollider collider;
    public float colliderActivationDuration = 0.65f;
    public float slamStrength = -500f;
    float cooldown;
    GameObject playerRef;
    Locomotion playerLocomotion;
    PlayerController playerController;
    bool didSlam = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        playerLocomotion = playerRef.GetComponent<Locomotion>();
        playerController = playerRef.GetComponent<PlayerController>();
        cooldown = playerLocomotion.crouchCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            if (!didSlam && !playerController.skillsOnCooldown)
            {
                didSlam = true;
                playerController.skillsOnCooldown = true;
                playerLocomotion.Jump(slamStrength);
                collider.gameObject.SetActive(true);
                StartCoroutine("Countdown");
            }
            
        }

    }

    private IEnumerator Countdown()
    {
        float normalizedTime = 0;
        while (normalizedTime <= colliderActivationDuration)
        {
            normalizedTime += Time.deltaTime;
            yield return null;
        }
        collider.gameObject.SetActive(false);
        playerController.skillsOnCooldown = false;
        while (normalizedTime <= cooldown)
        {
            normalizedTime += Time.deltaTime;
            yield return null;
        }
        didSlam = false;
    }

    public void RestartSkill()
    {
        StopCoroutine("Countdown");
        didSlam = false;
        collider.gameObject.SetActive(false);
    }

}
