using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wisp_Controller : MonoBehaviour
{
    //Kurtis Watson
    private NavMeshAgent m_navAgent;
    private Player_Controller r_player;
    private Wave_System r_waveSystem;

    private int m_random;
    private GameObject[] wispPoint;

    public Transform m_desiredLocation;
    public bool m_enemySpawn;

    public int m_type; // the enemy type it will spawn

    // Start is called before the first frame update
    void Start()
    {
        
        m_navAgent = gameObject.GetComponent<NavMeshAgent>();
        r_player = GameObject.FindObjectOfType<Player_Controller>();
        r_waveSystem = GameObject.FindObjectOfType<Wave_System>();

        m_navAgent.speed = Random.Range(7, 16);

        wispPoint = GameObject.FindGameObjectsWithTag("WispPoint");

        if (m_enemySpawn == true)
        {
            int m_randomWispPoints = Random.Range(0, 4);
            m_desiredLocation = wispPoint[m_randomWispPoints].transform;
        }
        else if(m_enemySpawn == false)
        {
            int m_randomSpawnPoints = Random.Range(0, r_waveSystem.spawnPoints.Count);
            m_desiredLocation = r_waveSystem.spawnPoints[m_randomSpawnPoints].transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float m_genDistance = Vector3.Distance(transform.position, m_desiredLocation.position);
        float m_mapDistance = Vector3.Distance(this.transform.position, m_desiredLocation.position);
        //m_navAgent.SetDestination(wispPoint[m_random].transform.position);
        m_navAgent.SetDestination(m_desiredLocation.transform.position);

        if (m_genDistance < 2f && m_enemySpawn == true)
        {
            Destroy(gameObject);
        }
        else if (m_mapDistance < 2f && m_enemySpawn == false)
        {
            Instantiate(r_waveSystem.enemyTypes[m_type], transform.position, Quaternion.identity); //spawn the enemy type
            Destroy(gameObject);
        }
    }
}
