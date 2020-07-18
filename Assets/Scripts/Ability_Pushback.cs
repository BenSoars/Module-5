using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Pushback : MonoBehaviour
{    
    private SphereCollider m_sphereCollider;

    public float m_abilitySpeed; //Values to change the strength of the pushback.
    public float m_upliftForce;
    public float m_knockbackForce;
    public float m_damageRadius;

    private Rigidbody m_rb;

    public void Start()
    {
        m_sphereCollider = gameObject.GetComponent<SphereCollider>();
        m_rb = GetComponent<Rigidbody>();
        m_rb.AddForce(transform.forward * m_abilitySpeed);
    }

    public void Update()
    {
        m_damageRadius = m_sphereCollider.radius;

        Collider[] o_colliders = Physics.OverlapSphere(transform.position, m_damageRadius); //Check objects that enter the trigger.
        foreach (Collider o_hit in o_colliders)
        {
            Rigidbody o_rb = o_hit.GetComponent<Rigidbody>(); //Get the collided objects rigidbody to add forces.

            if(o_rb != null && o_hit.gameObject.layer != 9 && o_hit.gameObject != this.gameObject) //Check if collider is not the 'Player'.
            {
                o_rb.gameObject.GetComponent<Enemy_Controller>().m_isStunned = true; //Stop enemy from coming towards player when pushing away.
                o_rb.AddExplosionForce(m_knockbackForce, transform.position, m_damageRadius, m_upliftForce); //Add force to objects triggered rigidbody's.
            }
        }
    }
}
