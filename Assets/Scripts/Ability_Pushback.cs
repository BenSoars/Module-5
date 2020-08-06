using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Kurtis Watson
public class Ability_Pushback : MonoBehaviour
{    
    [Header("Object Components")]
    private SphereCollider m_sphereCollider; //Get sphere component.
    private Rigidbody m_rb; //Get rigidbody component.

    [Header("Ability Properties")]
    [Space(2)]
    [Tooltip("Set how fast the ability moves forward from the player.")]
    public float abilitySpeed; //Values to change the strength of the pushback.
    [Tooltip("Set how high the player is lifted off the ground.")]
    public float upliftForce; //The upwards velocity.
    [Tooltip("Set how much the player is knocked back on trigger enter.")]
    public float knockbackForce; //The force at which the knockback is applied.
    [Tooltip("Set the radius at which the object effects enemies.")]
    public float damageRadius; //The damage radius.

    public void Start()
    {
        m_sphereCollider = gameObject.GetComponent<SphereCollider>();
        m_rb = GetComponent<Rigidbody>(); // Get the rigidbody of pushback to allow for force to be added.
        m_rb.AddForce(transform.forward * abilitySpeed); //Add force to the object.
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
