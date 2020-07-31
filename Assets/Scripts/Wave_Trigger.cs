using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Kurtis Watson
public class Wave_Trigger : MonoBehaviour
{
    [Header("Script References")]
    private Wave_System m_waveSystem;

    private void Start()
    {
        m_waveSystem = FindObjectOfType<Wave_System>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")) //Detect if the trigger has been entered > 
        {
            m_waveSystem.newWave = true; // > begin the wave.
            Destroy(gameObject); //Remove the trigger after collision.
        }
    }
}
