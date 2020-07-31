using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Kurtis Watson
public class Text_HitDamage : MonoBehaviour
{
    [Header("Script References")]
    private Player_Controller m_playerController;

    void Start()
    {
        m_playerController = FindObjectOfType<Player_Controller>();
    }

    // Update is called once per frame
    void Update()
    {     
        float m_distance = Vector3.Distance(transform.position, m_playerController.transform.position);  //Return a value of the distance between the text object and the player.

        if (m_distance < 1) //This removes the text object if it is too clost to the player.
        {
            Destroy(gameObject);
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, m_playerController.playerRotX, transform.eulerAngles.z); //Rotates towards the players position.
        transform.Translate(Vector3.up * (Time.deltaTime * 1.5f)); //Makes the object rise.
        transform.position = Vector3.MoveTowards(transform.position, m_playerController.transform.position, Time.deltaTime * 2); //Moves the object towards the player.
    }
}
