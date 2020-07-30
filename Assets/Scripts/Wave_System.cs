using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wave_System : MonoBehaviour
{
    //Ben Soars
    public List<GameObject> enemyTypes = new List<GameObject>(); // the enemy types
    public List<Transform> spawnPoints = new List<Transform>(); // the amount of enemies per wave

    public List<string> amountOf = new List<string>(); 
    public List<GameObject> spawnedEnemies = new List<GameObject>(); // the spawned enemies
    public int enemiesLeft;  // the amount of enemies remaining

    public int curRound;  // the current round the player is on
    public List<int> enemyArray = new List<int>();  // the current array of enemies
    public bool m_startedWaves; // check for the start of the wave

    //Kurtis Watson

    // component access
    private Player_Controller r_playerController;
    private User_Interface r_userInterface;
    private Prototype_Classes r_prototypeClasses;
    private Notes_System r_notesSystem;

    public List<GameObject> m_wisps = new List<GameObject>();

    private GameObject[] m_wispPoint;
    private int m_random;
    public int m_intermissionTime;

    public bool m_newWave;
    private bool m_timeMet;

    private Text m_enemyCount;
    public float m_fogMath;

    //Kurtis Watson
    private void Start()
    {
        // get access to the various components needed by this script
        m_wispPoint = GameObject.FindGameObjectsWithTag("WispPoint");
        r_playerController = FindObjectOfType<Player_Controller>();
        r_userInterface = FindObjectOfType<User_Interface>();
        r_prototypeClasses = FindObjectOfType<Prototype_Classes>();
        r_notesSystem = FindObjectOfType<Notes_System>();
        m_enemyCount = GameObject.Find("EnemyCount").GetComponent<Text>();
    }

    //Kurtis Watson
    private void Update()
    {
        f_updateUI();

        if (m_newWave == true)
        {
            r_userInterface.f_waveTimer();
            StartCoroutine(f_spawnWisps()); // spawn wisps
        }
    }

    //Kurtis Watson
    IEnumerator f_spawnWisps()
    {
        m_newWave = false;
        yield return new WaitForSeconds(m_intermissionTime);
        m_startedWaves = true; //Update UI values.
        f_sortOutEnemys(); //Spawn enemies of different types.
        for (int k = 0; k < enemyArray.Count; k++)
        {
            for (int i = 0; i < enemyArray[k]; i++)
            {
                m_random = Random.Range(0, 4);
                GameObject spawned = Instantiate(m_wisps[k], m_wispPoint[m_random].transform.position, Quaternion.identity); //Spawn wisps at a random point.
                spawnedEnemies.Add(spawned); //Add enemy spawned to enemies spawned list.
            }
        }
        enemiesLeft = spawnedEnemies.Count; //Set the count for the enemies left.
        r_prototypeClasses.m_fogStrength = 0.2f; //Fog strength.
        r_prototypeClasses.m_currentFog = 0.2f; //Reset current fog.
        r_prototypeClasses.m_stonePower[r_prototypeClasses.m_chosenBuff] -= enemiesLeft * 2; //Decrease the chosen enemy buff by two times the amount of enemies.
        m_fogMath = r_prototypeClasses.m_fogStrength / enemiesLeft; //Calculate the amount of fog to decrease each enemy kill.
        curRound += 1; //Increase current round by one.
       
        m_timeMet = false;
    }

    //Kurtis Watson
    void f_updateUI()
    {
        m_enemyCount.text = ("" + enemiesLeft); //Display enemies left on the runtime UI.
    }

    // Update is called once per frame
    //Ben Soars
    void FixedUpdate()
    {       
        
        if (spawnedEnemies.Count <= 0 && enemiesLeft == 0 && m_timeMet == false)
        {
            r_notesSystem.m_spawnNote = true;
            m_startedWaves = false; //Begin the wave (required for a different script).
            r_prototypeClasses.m_canSelect = true; //Allow the player to choose a new Starstone.
            r_prototypeClasses.m_activeStone[r_prototypeClasses.m_classState] = false; //Disable the current stone so that it can be chosen again.
            m_timeMet = true; // time has been met
            r_userInterface.f_waveTimer(); //Update the round time limit.
            f_spawnWisps(); // spawn more wisps
        }
        else
        {
            // if there are enemies
            for (int i = 0; i < spawnedEnemies.Count; i++) //check the list of enemies that are spawned
            {
                if (spawnedEnemies[i] == null) // if the enemy at that point doesn't exsist
                {
                    spawnedEnemies.RemoveAt(i); // remove eveny from the list
                    break; // break the loop, as the for loop won't work due to an element being removed
                }
            }
        }
    }

    //Ben Soars
    void f_sortOutEnemys()
    {
        string[] varArray = amountOf[curRound].Split('_'); // split the current amount of enemies

        enemyArray.Clear();
        for (int i = 0; i < enemyTypes.Count; i++)  // for loop for all the enemy types
        {
            enemyArray.Add(System.Convert.ToInt32(varArray[i]));// convert the string into a string if it can
        }
         
       
    }
}
