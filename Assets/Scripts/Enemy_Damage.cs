using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Damage : MonoBehaviour
{
    public float m_damage;
    public BoxCollider m_hurtBox;

    public Player_Controller r_player;
    // Start is called before the first frame update
    
    void OnEnable()
    {
        m_hurtBox.enabled = true;
    }

    void OnTriggerStay (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            r_player = other.gameObject.GetComponent<Player_Controller>();
            r_player.m_playerHealth -= m_damage * r_player.m_defenceValue;
            m_hurtBox.enabled = false;
        }
    }
}
