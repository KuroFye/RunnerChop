using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillKick : MonoBehaviour
{

    public CapsuleCollider collider;
    public float colliderActivationDuration = 0.65f, cooldown = 0.5f;
    GameObject playerRef;
    Locomotion playerLocomotion;
    PlayerController playerController;
    bool didKick = false;

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
        if (Input.GetButtonDown("Fire1"))
        {
            if (!didKick && !playerController.skillsOnCooldown)
            {
                didKick = true;
                playerController.skillsOnCooldown = true;
                playerRef.GetComponent<Rigidbody>().isKinematic = true;
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
        playerRef.GetComponent<Rigidbody>().isKinematic = false;
        while (normalizedTime <= cooldown)
        {
            normalizedTime += Time.deltaTime;
            yield return null;
        }
        didKick = false;
    }

    public void RestartSkill()
    {
        StopCoroutine("Countdown");
        didKick = false;
        collider.gameObject.SetActive(false);
        playerRef.GetComponent<Rigidbody>().isKinematic = false;
    }
}
