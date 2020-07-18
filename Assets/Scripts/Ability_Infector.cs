using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Infector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")) //Check if collided object is an enemy >
        {            
            other.gameObject.GetComponent<Enemy_Controller>().m_isEnemyInfected = true; //If true, infect the collided enemy.
            Destroy(gameObject);
        }
    }
}
