using System.Collections;
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
    bool didSlam = false, initialized = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        playerLocomotion = playerRef.GetComponent<Locomotion>();
        cooldown = playerLocomotion.crouchCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            if (!didSlam)
            {
                didSlam = true;                
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
        while(normalizedTime <= cooldown)
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
        cooldown = playerLocomotion.crouchCooldown;
        collider.gameObject.SetActive(false);
    }

}
