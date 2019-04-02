using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUppercut : MonoBehaviour
{
    public CapsuleCollider collider;
    public float colliderActivationDuration = 0.75f, skillCooldown = 0.5f;
    public float jumpStrength = 500f;
    GameObject playerRef;
    Locomotion playerLocomotion;
    PlayerController playerController;
    bool didUppercut = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        playerLocomotion = playerRef.GetComponent<Locomotion>();
        playerController = playerRef.GetComponent<PlayerController>();
                
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerLocomotion.m_IsGrounded)
        {
            if (Input.GetButtonDown("Jump") && !didUppercut && !playerController.skillsOnCooldown)
            {
                didUppercut = true;
                playerController.skillsOnCooldown = true;
                playerLocomotion.Jump(jumpStrength);
                collider.gameObject.SetActive(true);
                StartCoroutine("Countdown");
            }
        }

        if (didUppercut){
            if (playerLocomotion.m_IsGrounded)
            {
                didUppercut = false;
            }
        }
    }

    private IEnumerator Countdown()
    {
        float normalizedTime = 0;
        while(normalizedTime <= skillCooldown)
        {
            playerController.skillsOnCooldown = false;
            normalizedTime += Time.deltaTime;
            yield return null;
        }

        while (normalizedTime <= colliderActivationDuration)
        {
            normalizedTime += Time.deltaTime;
            yield return null;
        }
        collider.gameObject.SetActive(false);
        
    }

    void OnEnable()
    {
        StopCoroutine("Countdown");
        collider.gameObject.SetActive(false);
        didUppercut = false;
    }
}
