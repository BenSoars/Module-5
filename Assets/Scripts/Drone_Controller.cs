using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Kurtis Watson
public class Drone_Controller : MonoBehaviour
{

    [Header("Camera Components")]
    public Transform camera;
    public Rigidbody rb;

    [Header("Camera Movement Properties")]
    [Space(2)]
    [Tooltip("Set how fast the camera rotates.")]
    public float camRotSpeed; //How fast the camera looks around.
    [Tooltip("Set how low the player can look before being stopped.")]
    public float camMinY; //How low the camera can look.
    [Tooltip("Set how high the player can look before being stopped.")]
    public float camMaxY; //How high the camera can look.
    [Tooltip("Set how smooth the camera moves (the higher the faster the camera movement).")]
    public float camSmoothSpeed; //How smooth the camera looks around.
    [Tooltip("Set the default speed of the drone.")]
    public float flySpeed; //Drone speed.
    [Tooltip("Set the max speed of the drone.")]
    public float maxSpeed; //Drone max speed.
    [Tooltip("Set how fast the drone moves up and down.")]
    public float verticalSpeed; //Up/down speed.

    float playerRotX; //Set the rotation of the player object.
    float camRotY;
    Vector3 directionIntentX; //Direction intended to look on X axis.
    Vector3 directionIntentY; //Direction to look on Y axis.

    private Player_Controller m_playerController; //Reference the player controller script.

    private void Start()
    {
        m_playerController = GameObject.FindObjectOfType<Player_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_playerController.m_isPlayerActive == false)
        {
            f_lookAround();
            f_moveAround();
        }

        if (!Input.anyKey)
        {
            rb.velocity = Vector3.zero;
        }
    }

    void f_lookAround()
    {
        Cursor.visible = false; //Remove cursor from the screen.
        Cursor.lockState = CursorLockMode.Locked; //Locks the cursor to the screen to prevent leaving the window.

        playerRotX += Input.GetAxis("Mouse X") * camRotSpeed; //Rotates player FPS view along X axis based on mouse movement.
        camRotY += Input.GetAxis("Mouse Y") * camRotSpeed; //Rotates the camera in Y axis so that the player object doesn't rotate upwards.

        camRotY = Mathf.Clamp(camRotY, camMinY, camMaxY); //Limit how far on the Y axis the player can look.

        Quaternion m_camTargetRotation = Quaternion.Euler(-camRotY, 0, 0);
        Quaternion m_targetRotation = Quaternion.Euler(0, playerRotX, 0);

        transform.rotation = Quaternion.Lerp(transform.rotation, m_targetRotation, Time.deltaTime * camSmoothSpeed); //Drone rotation.

        camera.localRotation = Quaternion.Lerp(camera.localRotation, m_camTargetRotation, Time.deltaTime * camSmoothSpeed); //Camera rotation.
    }

    void f_moveAround()
    {
        directionIntentX = camera.right;
        directionIntentX.y = 0;       
        directionIntentX.Normalize(); //Normalize makes the numbers more 'usable' for the engine.

        directionIntentY = camera.forward;
        directionIntentY.y = 0;
        directionIntentY.Normalize();

        rb.velocity = directionIntentY * Input.GetAxis("Vertical") * flySpeed + directionIntentX * Input.GetAxis("Horizontal") * flySpeed + Vector3.up * rb.velocity.y; //Movement controls.
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed); //Clamp the speed of the drone.

        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(Vector3.down * (verticalSpeed / 100)); //Decrease vertical movement.
        }
        if (Input.GetKey(KeyCode.Space)) 
        {
            transform.Translate(Vector3.up * (verticalSpeed / 100)); //Increase vertical movement.
        }
    }  
}
