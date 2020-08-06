using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Kurtis Watson
public class Wave_System : MonoBehaviour
{
    //Kurtis Watson
    [Header("Script References")] //Required script references.
    private User_Interface r_userInterface;
    private Prototype_Classes m_prototypeClasses;
    private Notes_System r_notesSystem; 

    [Header("Wave Properties")]
    [Space(2)]
    public List<GameObject> wisps = new List<GameObject>();
    public float fogMath; //Used to work out how much fog to decrease by based on enemies.
    public int intermissionTime; //Intermission time value.
    private GameObject[] m_wispPoint; //Spawn points.
    private int m_random; //Used to randomly choose where the enemy wisps should move towards.
    public bool newWave; //Check for new wave.
    private bool m_timeMet;
    private Text m_enemyCount; //Displayed on screen.


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
    private void Start()
    {
        m_wispPoint = GameObject.FindGameObjectsWithTag("WispPoint"); //Find all wisp points.
        r_userInterface = FindObjectOfType<User_Interface>();
        m_prototypeClasses = FindObjectOfType<Prototype_Classes>();
        r_notesSystem = FindObjectOfType<Notes_System>();
        m_enemyCount = GameObject.Find("EnemyCount").GetComponent<Text>();
    }

    //Kurtis Watson
    private void Update()
    {
        f_updateUI();

        if (newWave == true)
        {
            r_userInterface.f_waveTimer();
            StartCoroutine(f_spawnWisps()); // spawn wisps
        }
    }

    //Kurtis Watson
    IEnumerator f_spawnWisps()
    {
        newWave = false;
        yield return new WaitForSeconds(intermissionTime); //Wait however long the intermission time is.
        m_startedWaves = true; //Update UI values.
        f_sortOutEnemys(); //Spawn enemies of different types.
        for (int k = 0; k < enemyArray.Count; k++)
        {
            for (int i = 0; i < enemyArray[k]; i++)
            {
                m_random = Random.Range(0, 4);  //Generate random number for spawn location.
                GameObject spawned = Instantiate(wisps[k], m_wispPoint[m_random].transform.position, Quaternion.identity); //Spawn wisps at a random point.
                spawnedEnemies.Add(spawned); //Add enemy spawned to enemies spawned list.
            }
        }
        enemiesLeft = spawnedEnemies.Count; //Set the count for the enemies left.
        m_prototypeClasses.fogStrength = 0.2f; //Fog strength.
        m_prototypeClasses.currentFog = 0.2f; //Reset current fog.
        m_prototypeClasses.stonePower[m_prototypeClasses.chosenBuff] -= enemiesLeft * 2; //Decrease the chosen enemy buff by two times the amount of enemies.
        fogMath = m_prototypeClasses.fogStrength / enemiesLeft; //Calculate the amount of fog to decrease each enemy kill.
        curRound += 1; //Increase current round by one.
       
        m_timeMet = false;
    }

    //Kurtis Watson
    void f_updateUI()
    {
        m_enemyCount.text = ("" + enemiesLeft); //Display enemies left on the runtime UI.
    }

    //Kurtis Watson
    void FixedUpdate()
    {              
        if (spawnedEnemies.Count <= 0 && enemiesLeft == 0 && m_timeMet == false)
        {
            r_notesSystem.spawnNote = true;
            m_startedWaves = false; //Begin the wave (required for a different script).
            m_prototypeClasses.canSelect = true; //Allow the player to choose a new Starstone.
            m_prototypeClasses.activeStone[m_prototypeClasses.classState] = false; //Disable the current stone so that it can be chosen again.
            m_timeMet = true; // Time has been met
            r_userInterface.f_waveTimer(); //Update the round time limit.
            f_spawnWisps(); //Spawn the next round of wisps.
        }
        else
        {
            //Ben Soars
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
