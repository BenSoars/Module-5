using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Damage : MonoBehaviour
{
    public float m_damage; // the damage the attack deals, is altered from the enemy controller
    public BoxCollider m_hurtBox; // the hurtbox collider

    public Player_Controller r_player;
    // Start is called before the first frame update
    
    void OnEnable()
    {
        m_hurtBox.enabled = true; // re-enable the box collider when it's enabled, as it could be turned off from the last use
    }

    void OnTriggerStay (Collider other) // if it enters a player
    {
        if (other.CompareTag("Player"))
        {
            r_player = other.gameObject.GetComponent<Player_Controller>(); // get player componenet
            r_player.playerHealth -= m_damage * r_player.m_defenceValue; // take health away based on damage and defence
            m_hurtBox.enabled = false; // disable the hurtbox to prevent multiple hits per frames
        }
    }
}
