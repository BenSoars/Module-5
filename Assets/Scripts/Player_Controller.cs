using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    //Kurtis Watson
    private Animator m_animator;
    public Transform m_camera;
    public Rigidbody m_rb;

    public Transform m_shotPoint;

    private Ability_Melee r_abilityMelee;

    public float m_camRotSpeed;
    public float m_camMinY;
    public float m_camMaxY;
    public float m_camSmoothSpeed;

    public float m_walkSpeed;
    public float m_sprintSpeed; 
    public float m_maxSpeed;
    public float m_jumpHeight;

    public int m_enemiesKilled;

    public float m_playerHealth;

    public float m_extraGravity;

    public float m_playerRotX;
    float m_camRotY;
    Vector3 m_directionIntentX;
    Vector3 m_directionIntentY;
    float m_speed; 

    // Ladder Values \\     
    public bool m_grounded;
    public bool m_canPlayerMove;
    public bool m_isLadder;
    public bool m_isUsingLadder;
    public bool m_topOfLadder;
    public bool m_isPlayerInvisible;

    public Transform desiredPos;

    public bool m_isSprinting;
    public bool m_isCrouching;

    public Rigidbody m_grenade;

    public bool m_isPlayerActive;

    public float m_defenceValue = 1;

    private void Start()
    {
        m_isPlayerActive = true;
        m_canPlayerMove = true;

        m_animator = GetComponent<Animator>();
        r_abilityMelee = GameObject.FindObjectOfType<Ability_Melee>();
    }

    // Update is called once per frame
    //Kurtis Watson
    void Update()
    {
        f_drone();

        if (m_playerHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        if (m_isPlayerActive == true)
        {
            // Ben Soars
            if (Input.GetKeyDown("g"))
            {
                Rigidbody thrownObject = Instantiate(m_grenade, m_shotPoint.transform.position, m_shotPoint.rotation); // create grenade
                thrownObject.AddForce(m_shotPoint.forward * 100); // push forwards
                thrownObject.AddForce(m_shotPoint.up * 50); // throw slightly upwards
            }

            //Kurtis Watson
            if (Input.GetKeyDown("e"))
            {
                r_abilityMelee.f_melee();
            }
            
            f_climb();
            f_lookAround();
            f_moveAround();
            f_strongerGravity();
            f_groundCheck();

            if (m_grounded == true && Input.GetButtonDown("Jump"))
            {
                f_playerJump();
            }
            gameObject.GetComponentInChildren<Camera>().enabled = true;
        }
        else gameObject.GetComponentInChildren<Camera>().enabled = false;
    }

    //Kurtis Watson
    void f_lookAround()
    {
        Cursor.visible = false; //Remove cursor from the screen.
        Cursor.lockState = CursorLockMode.Locked; //Locks the cursor to the screen to prevent leaving the window.

        m_playerRotX += Input.GetAxis("Mouse X") * m_camRotSpeed; //Rotates player FPS view along X axis based on mouse movement.
        m_camRotY += Input.GetAxis("Mouse Y") * m_camRotSpeed; //Rotates the camera in Y axis so that the player object doesn't rotate upwards.

        m_camRotY = Mathf.Clamp(m_camRotY, m_camMinY, m_camMaxY); //Limit how far on the Y axis the player can look.

        Quaternion m_camTargetRotation = Quaternion.Euler(-m_camRotY, 0, 0); 
        Quaternion m_targetRotation = Quaternion.Euler(0, m_playerRotX, 0);

        transform.rotation = Quaternion.Lerp(transform.rotation, m_targetRotation, Time.deltaTime * m_camSmoothSpeed);

        m_camera.localRotation = Quaternion.Lerp(m_camera.localRotation, m_camTargetRotation, Time.deltaTime * m_camSmoothSpeed);
    }

    //Kurtis Watson
    void f_moveAround()
    {
        m_directionIntentX = m_camera.right;
        m_directionIntentX.y = 0;
        //Normalize makes the numbers more 'usable' for the engine.
        m_directionIntentX.Normalize();

        m_directionIntentY = m_camera.forward;
        m_directionIntentY.y = 0;
        m_directionIntentY.Normalize();

        if (m_canPlayerMove == true)
        {
            m_rb.velocity = m_directionIntentY * Input.GetAxis("Vertical") * m_speed + m_directionIntentX * Input.GetAxis("Horizontal") * m_speed + Vector3.up * m_rb.velocity.y;
            m_rb.velocity = Vector3.ClampMagnitude(m_rb.velocity, m_maxSpeed);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                m_isSprinting = true;
                m_speed = m_sprintSpeed;
            }
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                m_isSprinting = false;
                m_speed = m_walkSpeed;
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                m_isCrouching = true;
            }
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                m_isCrouching = false;
            }
            m_animator.SetBool("Crouch", m_isCrouching);
        }
    }

    //Kurtis Watson
    void f_strongerGravity()
    {
        if (m_canPlayerMove == true)
        {
            m_rb.AddForce(Vector3.down * m_extraGravity);
        }
    }

    //Kurtis Watson
    void f_groundCheck()
    {
        RaycastHit m_groundHit;
        m_grounded = Physics.Raycast(transform.position, -transform.up, out m_groundHit, 1.25f); //Automatically set bool value to true if an object is hit; else, returns false.
    }

    //Kurtis Watson
    void f_playerJump()
    {
        m_rb.AddForce(Vector3.up * m_jumpHeight, ForceMode.Impulse);
    }

    //Kurtis Watson
    void f_drone()
    {
        if (Input.GetKeyDown("c"))
        {
            m_isPlayerActive = !m_isPlayerActive;
        }
    }

    //Kurtis Watson
    void f_climb()
    {
        RaycastHit m_ladderHit;

        if (Input.GetKeyDown("f") && m_isUsingLadder == true)
        {
            m_rb.useGravity = true;
            m_isUsingLadder = false;
            m_canPlayerMove = true;
        }
        
        else if (Physics.Raycast(m_camera.transform.position, m_camera.transform.forward, out m_ladderHit, 2f, 1<<10)) //Shoots a raycast forward of the players position at a distance of '2f'.
        {
            if (m_ladderHit.collider != null && m_ladderHit.collider.gameObject.layer == 10)
            {
                if (Input.GetKeyDown("f") && m_isUsingLadder == false)
                {
                    m_isUsingLadder = true;
                    m_rb.useGravity = false;
                    m_canPlayerMove = false;
                    m_rb.velocity = Vector3.zero;
                    if (m_topOfLadder == true)
                    {
                        desiredPos = m_ladderHit.collider.gameObject.transform.Find("Climb Point Top");
                        this.transform.position = desiredPos.transform.position;
                    }
                    if(m_topOfLadder == false)
                    {
                        desiredPos = m_ladderHit.collider.gameObject.transform.Find("Climb Point Bottom");
                        this.transform.position = desiredPos.transform.position;
                    }

                }   
            }
        }

        float m_upwardsSpeed = Input.GetAxis("Vertical") / 20;
        if (m_isUsingLadder == true)
        {
            transform.Translate(0, m_upwardsSpeed, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Top Stop Point")
        {
            m_rb.useGravity = true;
            m_isUsingLadder = false;
            m_canPlayerMove = true;
        }
        if (other.gameObject.name == "Top of Ladder")
        {
            m_topOfLadder = true;
        }      
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Top of Ladder")
        {
            m_topOfLadder = false;
        }
    }
}
