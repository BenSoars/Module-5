using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Controller : MonoBehaviour
{
    //Kurtis Watson
    public Transform m_camera;
    public Rigidbody m_rb;

    public float m_camRotSpeed;
    public float m_camMinY;
    public float m_camMaxY;
    public float m_camSmoothSpeed;

    public float m_flySpeed;
    public float m_maxSpeed;
    public float m_verticalSpeed;

    float m_playerRotX;
    float m_camRotY;
    Vector3 m_directionIntentX;
    Vector3 m_directionIntentY;

    private Player_Controller r_playerContoller;

    private void Start()
    {
        r_playerContoller = GameObject.FindObjectOfType<Player_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (r_playerContoller.m_isPlayerActive == false)
        {
            f_lookAround();
            f_moveAround();
        }

        if (!Input.anyKey)
        {
            m_rb.velocity = Vector3.zero;
        }
    }

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

    void f_moveAround()
    {
        m_directionIntentX = m_camera.right;
        m_directionIntentX.y = 0;
        //Normalize makes the numbers more 'usable' for the engine.
        m_directionIntentX.Normalize();

        m_directionIntentY = m_camera.forward;
        m_directionIntentY.y = 0;
        m_directionIntentY.Normalize();

        m_rb.velocity = m_directionIntentY * Input.GetAxis("Vertical") * m_flySpeed + m_directionIntentX * Input.GetAxis("Horizontal") * m_flySpeed + Vector3.up * m_rb.velocity.y;
        m_rb.velocity = Vector3.ClampMagnitude(m_rb.velocity, m_maxSpeed);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(Vector3.down * (m_verticalSpeed / 100));
        }
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * (m_verticalSpeed / 100));
        }
    }  
}
