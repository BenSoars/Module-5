using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Kurtis Watson
public class Portal : MonoBehaviour
{
    [Header("Portal Components")]
    private Player_Controller m_playerController;
    [Tooltip("Set the destination of where the player is teleported to.")]
    public Transform desiredLocation;

    private void Start()
    {
        m_playerController = FindObjectOfType<Player_Controller>();       
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            m_playerController.transform.position = desiredLocation.position; //Teleport player to desired location position.
        }
    }
}
