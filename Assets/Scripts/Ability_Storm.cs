using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Storm : MonoBehaviour
{
    private void Start()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<ParticleSystem>().Stop();
        Invoke("f_startRain", 2);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameObject enemyHit = other.gameObject;
            enemyHit.GetComponent<Enemy_Controller>().m_enemyHealth -= 2f;
        }
    }

    void f_startRain()
    {
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<ParticleSystem>().Play();
    }
}
