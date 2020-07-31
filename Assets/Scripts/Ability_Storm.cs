using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Kurtis Watson
public class Ability_Storm : MonoBehaviour
{
    [Header("Ability Values")]
    [Space(2)]
    [Tooltip("Set the damage of the rain per second.")]
    public float damagePerSecond = 5; //Damage count to enemy.
    private void Start()
    {
        GetComponent<BoxCollider>().enabled = false; //Disable the storm damage box collider on first instantiation.
        GetComponent<ParticleSystem>().Stop(); //Disable the particle system from animating.
        Invoke("f_startRain", 2); //Begin the rain after 2 seconds so that the cloud is at maximum height before it rains.
    }
    
    void f_startRain() //Start raining.
    {
        GetComponent<BoxCollider>().enabled = true; //Enable damage box.
        GetComponent<ParticleSystem>().Play(); //Start raining the particles.
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")) //If the trigger has an emnemy inside of it.
        {
            GameObject enemyHit = other.gameObject; //Save the gameobject >
            enemyHit.GetComponent<Enemy_Controller>().m_enemyHealth -= 2f * Time.deltaTime; // and remove the health of the enemy.
        }
    }
}
