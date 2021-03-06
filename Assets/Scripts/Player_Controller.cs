﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Kurtis Watson
public class Player_Controller : MonoBehaviour
{
    [Header("Referenced Scripts")]
    [Space(2)]
    private Prototype_Classes m_prototypeClasses;

    [Header("Player Properties")]
    [Space(2)]
    public Transform camera;
    public Rigidbody rb;
    [Tooltip("Set the players max health.")]
    public float playerHealth; //Player health.
    [Tooltip("Set the players walk speed.")]
    public float walkSpeed; //Walk speed.
    [Tooltip("Set the players sprint speed.")]
    public float sprintSpeed; //Run speed.
    [Tooltip("Set the max speed of the player.")]
    public float maxSpeed; //Max run speed.
    [Tooltip("Set how high the player can jump.")]
    public float jumpHeight; //How high the player can jump.
    [Tooltip("Set the level of gravity in the game after player jumps.")]
    public float extraGravity; //How much gravity is applied when the player is in the air.
    [Tooltip("Player rotation along X axis.")]
    public float playerRotX; //Player object rotation.
    [Tooltip("Check for if the player is sprinting.")]
    public bool isSprinting; //True/false is the player sprinting.
    [Tooltip("Check for if the player is crouching.")]
    public bool isCrouching;  //True/false is the player crouching.
    [Tooltip("Check to see if the player is able to move.")]
    public bool isPlayerActive; //True/false is the player able to move.
    [Tooltip("Position where the bullets are shot from.")]
    public Transform m_shotPoint; //Bullet shotpoint.
    private Animator m_animator; //Reference animator in game object.
    private float m_speed; //Set the speed.

    [Header("Camera Rotation Properties")]
    [Space(2)]
    [Tooltip("How fast the camera looks around.")]
    public float camRotSpeed; //How fast the camera looks around.
    [Tooltip("Set the maximum Y look rotation.")]
    public float camMinY; //How low the camera can look before it stops.
    [Tooltip("Set the maximum X look rotation.")]
    public float camMaxY; //How high the camera can look before it stops.
    [Tooltip("Set how smoother the camera is (the higher the faster the player looks).")]
    public float camSmoothSpeed; //How smooth the camera around.
    private float m_camRotY;
    private Vector3 m_directionIntentX; //Direction intended to look towards on X axis.
    private Vector3 m_directionIntentY; //Direction intended to look towards on Y axis.
    [Tooltip("Enemies killed during current round.")]
    public int enemiesKilled; //Enemies kill sum per round.

    [Header("Ladder Properties")]
    [Space(2)]
    [Tooltip("Check for if the player is on the ground.")]
    public bool grounded; //Check if the player is grounded.
    [Tooltip("Check if the player can move around.")]
    public bool canPlayerMove; //Can player move.
    [Tooltip("Check if the player is looking at a ladder.")]
    public bool isLadder;
    [Tooltip("Check if the player is on the ladder.")]
    public bool isUsingLadder;
    [Tooltip("Check for if the player is at the top of the ladder.")]
    public bool topOfLadder; //Stopping point for ladder.
    public bool isPlayerInvisible;
    [Tooltip("Set position to climb from on ladder.")]
    public Transform desiredPos; //Teleport location for ladder mechanic.

    private Ability_Melee m_abilityMelee;

    public float m_extraGravity;

    public bool m_isSprinting;
    public bool m_isCrouching;

    public Rigidbody m_grenade;

    public bool m_isPlayerActive;

    public float m_defenceValue = 1;

    private void Start()
    {
        m_isPlayerActive = true;
        canPlayerMove = true;

        m_animator = GetComponent<Animator>();
        m_abilityMelee = GameObject.FindObjectOfType<Ability_Melee>();
    }

    // Update is called once per frame
    //Kurtis Watson
    void Update()
    {
        f_drone();

        if (playerHealth <= 0)
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
                m_abilityMelee.f_melee();
            }
            
            f_climb();
            f_lookAround();
            f_moveAround();
            f_strongerGravity();
            f_groundCheck();

            if (grounded == true && Input.GetButtonDown("Jump"))
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

        playerRotX += Input.GetAxis("Mouse X") * camRotSpeed; //Rotates player FPS view along X axis based on mouse movement.
        m_camRotY += Input.GetAxis("Mouse Y") * camRotSpeed; //Rotates the camera in Y axis so that the player object doesn't rotate upwards.

        m_camRotY = Mathf.Clamp(m_camRotY, camMinY, camMaxY); //Limit how far on the Y axis the player can look.

        Quaternion m_camTargetRotation = Quaternion.Euler(-m_camRotY, 0, 0); 
        Quaternion m_targetRotation = Quaternion.Euler(0, playerRotX, 0);

        transform.rotation = Quaternion.Lerp(transform.rotation, m_targetRotation, Time.deltaTime * camSmoothSpeed);

        camera.localRotation = Quaternion.Lerp(camera.localRotation, m_camTargetRotation, Time.deltaTime * camSmoothSpeed);
    }

    //Kurtis Watson
    void f_moveAround()
    {
        m_directionIntentX = camera.right;
        m_directionIntentX.y = 0;
        //Normalize makes the numbers more 'usable' for the engine.
        m_directionIntentX.Normalize();

        m_directionIntentY = camera.forward;
        m_directionIntentY.y = 0;
        m_directionIntentY.Normalize();

        if (canPlayerMove == true)
        {
            rb.velocity = m_directionIntentY * Input.GetAxis("Vertical") * m_speed + m_directionIntentX * Input.GetAxis("Horizontal") * m_speed + Vector3.up * rb.velocity.y;
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                m_isSprinting = true;
                m_speed = sprintSpeed;
            }
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                m_isSprinting = false;
                m_speed = walkSpeed;
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
        if (canPlayerMove == true)
        {
            rb.AddForce(Vector3.down * m_extraGravity);
        }
    }

    //Kurtis Watson
    void f_groundCheck()
    {
        RaycastHit m_groundHit;
        grounded = Physics.Raycast(transform.position, -transform.up, out m_groundHit, 1.25f); //Automatically set bool value to true if an object is hit; else, returns false.
    }

    //Kurtis Watson
    void f_playerJump()
    {
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
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

        if (Input.GetKeyDown("f") && isUsingLadder == true)
        {
            rb.useGravity = true;
            isUsingLadder = false;
            canPlayerMove = true;
        }
        
        else if (Physics.Raycast(camera.transform.position, camera.transform.forward, out m_ladderHit, 2f, 1<<10)) //Shoots a raycast forward of the players position at a distance of '2f'.
        {
            if (m_ladderHit.collider != null && m_ladderHit.collider.gameObject.layer == 10)
            {
                if (Input.GetKeyDown("f") && isUsingLadder == false)
                {
                    isUsingLadder = true;
                    rb.useGravity = false;
                    canPlayerMove = false;
                    rb.velocity = Vector3.zero;
                    if (topOfLadder == true)
                    {
                        desiredPos = m_ladderHit.collider.gameObject.transform.Find("Climb Point Top");
                        this.transform.position = desiredPos.transform.position;
                    }
                    if(topOfLadder == false)
                    {
                        desiredPos = m_ladderHit.collider.gameObject.transform.Find("Climb Point Bottom");
                        this.transform.position = desiredPos.transform.position;
                    }

                }   
            }
        }

        float m_upwardsSpeed = Input.GetAxis("Vertical") / 20;
        if (isUsingLadder == true)
        {
            transform.Translate(0, m_upwardsSpeed, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Top Stop Point")
        {
            rb.useGravity = true;
            isUsingLadder = false;
            canPlayerMove = true;
        }
        if (other.gameObject.name == "Top of Ladder")
        {
            topOfLadder = true;
        }      
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Top of Ladder")
        {
            topOfLadder = false;
        }
    }
}
