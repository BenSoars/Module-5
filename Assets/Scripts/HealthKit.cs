using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    // Ben Soars
    public int m_healthValue; // the amount to heal

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // if player walks into it
        {
            other.GetComponent<Player_Controller>().playerHealth += m_healthValue; //add health to player
            Destroy(gameObject); // destroy self so can't be picked up again
        }
    }
}
