using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Kurtis Watson
public class Ability_Handler : MonoBehaviour
{
    [Header("Script References")]
    [Space(2)]
    private Player_Controller m_playerController;

    [Header("Ability Properties")]
    [Space(2)]
    public int totalKnives;
    public Transform shotPoint; //Where bullets are shot from.
    private RaycastHit m_hitscanCast; //The raycast that determines the direction of the bullet.

    [Header("Ability Objects")]
    [Space(2)]
    public GameObject wall; //Ability Game Objects.
    public GameObject storm;
    public GameObject knife;
    public GameObject tornado;
    public GameObject infector;
   
    private void Start()
    {
        m_playerController = GameObject.FindObjectOfType<Player_Controller>();
    }

    public void f_spawnWall()
    {
        if (Physics.Raycast(shotPoint.position, shotPoint.forward, out m_hitscanCast, Mathf.Infinity)) //Creates a Raycast in direction player is looking.
        {
            GameObject o_wall = Instantiate(wall, new Vector3(m_hitscanCast.point.x, m_hitscanCast.point.y - 2, m_hitscanCast.point.z), Quaternion.LookRotation(Vector3.forward)); //Instantiate a wall that summons at the position of the players crosshair location.
            o_wall.transform.eulerAngles = new Vector3(o_wall.transform.eulerAngles.x, m_playerController.m_playerRotX, o_wall.transform.eulerAngles.z); //Rotate the wall based on angle of player.
        }
    } //Spawn a wall at position of player crosshair.

    public void f_spawnStorm()
    {
        if (Physics.Raycast(shotPoint.position, shotPoint.forward, out m_hitscanCast, Mathf.Infinity)) //Creates a Raycast.
        {
            Instantiate(storm, new Vector3(m_hitscanCast.point.x, m_hitscanCast.point.y - 2, m_hitscanCast.point.z), Quaternion.LookRotation(Vector3.forward));
        }
    } //Spawn a storm at position of where the player is looking.

    public void f_spawnKnives()
    {
        Debug.Log("Shot Point: " + m_hitscanCast.point);

        float m_sideDirection = -40;

        for (int i = 0; i < totalKnives; i++)
        {
            GameObject o_knife = Instantiate(knife, shotPoint.position, Quaternion.identity);
            Rigidbody m_krb = o_knife.GetComponent<Rigidbody>();

            m_krb.AddForce(shotPoint.forward * 100);
            m_krb.AddForce(shotPoint.right * m_sideDirection);

            m_sideDirection += 10;

        }
    } //Spawn knives that shoot towards the enemy.

    public void f_spawnTornado()
    {
        if (Physics.Raycast(shotPoint.position, shotPoint.forward, out m_hitscanCast, Mathf.Infinity)) //Creates a Raycast.
        {
            Instantiate(tornado, new Vector3(m_hitscanCast.point.x, m_hitscanCast.point.y - 2, m_hitscanCast.point.z), Quaternion.LookRotation(Vector3.forward));
        }
    } //Spawn a tornado.

    public void f_spawnInfector()
    {
        if (Physics.Raycast(shotPoint.position, shotPoint.forward, out m_hitscanCast, Mathf.Infinity)) //Creates a Raycast.
        {
            Instantiate(infector, new Vector3(m_hitscanCast.point.x, m_hitscanCast.point.y - 2, m_hitscanCast.point.z), Quaternion.LookRotation(Vector3.forward));
        }
    } //Spawn an infector.
}
