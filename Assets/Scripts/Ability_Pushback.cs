using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Kurtis Watson
public class Ability_Pushback : MonoBehaviour
{    
    [Header("Object Components")]
    private SphereCollider m_sphereCollider;
    private Rigidbody m_rb;

    [Header("Ability Properties")]
    [Space(2)]
    [Tooltip("YES")]
    public float abilitySpeed; //Values to change the strength of the pushback.
    public float upliftForce;
    public float knockbackForce;
    public float damageRadius;

    public void Start()
    {
        m_sphereCollider = gameObject.GetComponent<SphereCollider>();
        m_rb = GetComponent<Rigidbody>();
        m_rb.AddForce(transform.forward * abilitySpeed);
    }

    public void Update()
    {
        damageRadius = m_sphereCollider.radius;

        Collider[] o_colliders = Physics.OverlapSphere(transform.position, damageRadius); //Check objects that enter the trigger.
        foreach (Collider o_hit in o_colliders)
        {
            Rigidbody o_rb = o_hit.GetComponent<Rigidbody>(); //Get the collided objects rigidbody to add forces.

            if(o_rb != null && o_hit.gameObject.layer != 9 && o_hit.gameObject != this.gameObject) //Check if collider is not the 'Player'.
            {
                o_rb.gameObject.GetComponent<Enemy_Controller>().m_isStunned = true; //Stop enemy from coming towards player when pushing away.
                o_rb.AddExplosionForce(knockbackForce, transform.position, damageRadius, upliftForce); //Add force to objects triggered rigidbody's.
            }
        }
    }
}
