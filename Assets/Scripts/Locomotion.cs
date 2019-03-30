using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Locomotion : MonoBehaviour
{
    public float speed = 5.0f;

    [Header("Jump")]
    public float jumpStrength = 10f;
    public float gravityMultiplier = 2f;
    public float startingYAngle = 90f;
    [SerializeField] float m_GroundCheckDistance = 0.1f;

    [Header("Crouch")]
    public float baseCapsuleHeight = 1.6f;
    public float crouchedCapsuleHeight = 0.8f, baseCapsuleCenter = 0.8f, crouchedCapsuleCenter = 0.4f, crouchDuration = 0.65f, crouchCooldown = 0.75f;
    bool canCrouch = true;

    [HideInInspector]
    public bool m_IsGrounded, m_IsGroundOneWay = false;
    Vector3 m_GroundNormal, m_StartingPosition;

    Vector3 moveDir = Vector3.zero;

    Animator anim;
    Rigidbody rigidbody;
    CapsuleCollider collider;

    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        transform.eulerAngles = new Vector3(0, startingYAngle, 0);

        collider.height = baseCapsuleHeight;
        anim.SetFloat("Forward", 1f);
    }

    // Update is called once per frame
    void Update()
    {

        //float h = CrossPlatformInputManager.GetAxis("Horizontal");
        //moveDir.x = h * 1f * speed;
        CheckGroundStatus();
        
        if(gameObject.transform.position.x < m_StartingPosition.x)
        {
            gameObject.transform.position.Set(m_StartingPosition.x, gameObject.transform.position.y, 0f);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (m_IsGrounded)
            {
                Jump(jumpStrength);
            }
            
        }

        if (Input.GetButtonDown("Crouch") && canCrouch)
        {
            if (m_IsGrounded)
            {
                anim.SetBool("Crouch", true);
                collider.height = crouchedCapsuleHeight;
                collider.center = new Vector3(0f, crouchedCapsuleCenter,0f);
                canCrouch = false;
                StartCoroutine("CrouchCountdown");
            }

        }

        if (!m_IsGrounded)
        {
            Vector3 extraGravityForce = (Physics.gravity * gravityMultiplier) - Physics.gravity;
            rigidbody.AddForce(extraGravityForce);
        }
        moveDir.y = rigidbody.velocity.y;
        moveDir.z = rigidbody.velocity.z;
        rigidbody.velocity = moveDir;

    }

    void CrouchUp()
    {
        anim.SetBool("Crouch", false);
        collider.height = baseCapsuleHeight;
        collider.center = new Vector3(0f, baseCapsuleCenter, 0f);
    }

    public void Jump(float strength)
    {
        rigidbody.velocity = new Vector3(0, 0, 0);
        rigidbody.AddForce(0f, strength, 0f);
    }

    public void SetStartingPosition(Vector3 position)
    {
        m_StartingPosition = position;
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        Vector3 offset = new Vector3(0.5f, 0f, 0f);
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f) - offset, Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            m_GroundNormal = hitInfo.normal;
            m_IsGrounded = true;
            anim.SetBool("OnGround", true);
            if (hitInfo.collider.tag.Equals("OneWayPlatform"))
            {
                m_IsGroundOneWay = true;
            }
            else
            {
                m_IsGroundOneWay = false;
            }
            //m_Animator.applyRootMotion = true;
        }
        else
        {
            m_IsGrounded = false;
            m_GroundNormal = Vector3.up;
            anim.SetBool("OnGround", false);
            anim.SetFloat("Jump", rigidbody.velocity.y);
            //m_Animator.applyRootMotion = false;
        }
    }

    private IEnumerator CrouchCountdown()
    {
        float normalizedTime = 0;
        while (normalizedTime <= crouchDuration)
        {
            normalizedTime += Time.deltaTime;
            yield return null;
        }
        CrouchUp();
        while(normalizedTime <= crouchCooldown)
        {
            normalizedTime += Time.deltaTime;
            yield return null;
        }
        canCrouch = true;
    }

    void OnEnable()
    {
        anim.SetFloat("Forward", 1f);
        rigidbody.velocity = new Vector3(0, 0, 0);
    }

}
