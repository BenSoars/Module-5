using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

//Kurtis Watson
public class Notes_System : MonoBehaviour
{
    [Header("Note Components")]
    [Space(2)]
    public GameObject note;
    private List<Transform> m_locations = new List<Transform>(); //Locations where note can spawn.
    private Transform m_desiredLocation;
    public bool spawnNote;

    private void Start()
    {
        for (int i = 1; i < 19; i++) //Add all the spawnpoints to a list.
        {
            m_locations.Add(GameObject.Find("NoteLocation_" + i).transform);
        }
    }
    private void Update()
    {
        if (spawnNote == true) //Checks if a note has been called to spawn.
        {
            spawnNote = false; //Stop the loop as only 1 note is needed to spawn.

            int random = Random.Range(0, m_locations.Count); //Generate a random number based on how many spawnpoints there are.
            m_desiredLocation = m_locations[random]; //Set the desired location to the position of the randomly chosen note location.

            Instantiate(note, m_desiredLocation.position, Quaternion.identity); //Spawn the note at desired location.
        }
    }
}
