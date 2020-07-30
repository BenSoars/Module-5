using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{

    public float m_damage; // damage to deal on contact
    public Collider m_hurtBox; // the hurtbox
    public Rigidbody m_rb; // the rigidbodys
    public TrailRenderer m_trail;

    public bool m_enemy; // if it's an enemy projectile

    public bool m_sticky; // will stick to objects it hits
    public bool m_faceDirectionOfTravel;

    public GameObject m_hitDamageText;

    void Update()
    {
        if (m_faceDirectionOfTravel == true && m_rb)
        {
            transform.rotation = Quaternion.LookRotation(m_rb.velocity);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && m_enemy == false) // if it's not an enemy projectile and collides with an enemy
        {
            other.GetComponent<Enemy_Controller>().m_enemyHealth -= m_damage; // deal damage to the enemy
            GameObject m_textObject = Instantiate(m_hitDamageText, other.transform.position, Quaternion.identity); // create a damage text number
            m_textObject.GetComponentInChildren<TextMeshPro>().text = "" + m_damage; // display the damage dealt to the text
            stickyProjectile(other); // run the sticky projectile test

        }
        else if (other.gameObject.CompareTag ("Player") && m_enemy == true)
        {
            
            other.GetComponent<Player_Controller>().m_playerHealth -= m_damage; // get the player component and take damage
            stickyProjectile(other); // run the sticky projectile test
        }
    }

    void stickyProjectile(Collider col)
    {
        if (m_sticky) // if the projectile is set to be sticky
        {
            Destroy(m_hurtBox); // destroy the hurtbox so it can't damage any more enemies
            Destroy(m_rb); // destroy the rigidbody so it won't fall while stick
            Destroy(m_trail); // destroy the trail to prevent visual glitches
            transform.SetParent(col.gameObject.transform); // set the prijectile to be parented to the passed object, in this case it's always the object it hits
        }
    }
}
