using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

 //Kurtis Watson
public class Wisp_Controller : MonoBehaviour
{
    [Header("Wisp Components")]
    private NavMeshAgent m_navAgent;
    private Wave_System m_waveSystem;
    private GameObject[] wispPoint;
    public Transform m_desiredLocation;
    public bool m_enemySpawn;
    public int m_type; // the enemy type it will spawn

    // Start is called before the first frame update
    void Start()
    {      
        m_navAgent = gameObject.GetComponent<NavMeshAgent>();  //Get navmesh component.
        m_waveSystem = GameObject.FindObjectOfType<Wave_System>();

        m_navAgent.speed = Random.Range(7, 16);

        wispPoint = GameObject.FindGameObjectsWithTag("WispPoint"); //Find all objects with the 'WispPoint' tag and store them in the array.

        if (m_enemySpawn == true)
        {
            int m_randomWispPoints = Random.Range(0, 4); //Generate a random number.
            m_desiredLocation = wispPoint[m_randomWispPoints].transform; //Set the desired spawn location based on randomly generated postion (random int).
        }
        else if(m_enemySpawn == false)
        {
            int m_randomSpawnPoints = Random.Range(0, m_waveSystem.spawnPoints.Count); 
            m_desiredLocation = m_waveSystem.spawnPoints[m_randomSpawnPoints].transform; //Set random desired destination.
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float m_genDistance = Vector3.Distance(transform.position, m_desiredLocation.position); //Creates a distance check so that the wisp can be destroyed if it is close enough to its desired location.
        float m_mapDistance = Vector3.Distance(this.transform.position, m_desiredLocation.position);

        m_navAgent.SetDestination(m_desiredLocation.transform.position);

        if (m_genDistance < 2f && m_enemySpawn == true) //Check distance between object and desired location.
        {
            Destroy(gameObject); //Destroy object.
        } 
        else if (m_mapDistance < 2f && m_enemySpawn == false)
        {
            Instantiate(m_waveSystem.enemyTypes[m_type], transform.position, Quaternion.identity); //Spawn the enemy type.
            Destroy(gameObject);
        }
    }
}
