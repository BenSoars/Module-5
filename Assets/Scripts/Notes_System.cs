using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Notes_System : MonoBehaviour
{
    public List<Transform> m_locations = new List<Transform>();

    private Transform m_desiredLocation;

    private Wave_System m_waveSystem;

    public GameObject m_note;

    public bool m_spawnNote;

    private void Start()
    {
        m_waveSystem = FindObjectOfType<Wave_System>();

       
    }
    private void Update()
    {


        if (m_spawnNote == true)
        {
            m_spawnNote = false;
            int random = Random.Range(0, m_locations.Count);

            m_desiredLocation = m_locations[random];

            Instantiate(m_note, m_desiredLocation.position, Quaternion.identity);
        }
    }
}
