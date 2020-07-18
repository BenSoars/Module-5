using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_Trigger : MonoBehaviour
{

    private Wave_System r_waveSystem;
    // Start is called before the first frame update

    private void Start()
    {
        r_waveSystem = FindObjectOfType<Wave_System>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            r_waveSystem.m_newWave = true;
            Destroy(gameObject);
        }
    }
}
