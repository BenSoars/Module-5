using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Kurtis Watson
public class Ability_Handler : MonoBehaviour
{
    [Header("Script References")]
    [Space(2)]
    private Player_Controller m_playerController; //Reference to player controller.

    [Header("Ability Properties")]
    [Space(2)]
    [Tooltip("Set the amount of knives to spawn when the knife ability is used.")]
    public int totalKnives;
    public Transform shotPoint; //Where bullets are shot from.
    private RaycastHit m_hitscanCast; //The raycast that determines the direction of the bullet.

    [Header("Ability Objects")]
    [Space(2)]
    [Tooltip("The obejct that is spawned when the wall ability is used.")]
    public GameObject wall; //Ability Game Objects.
    [Tooltip("The object that is spawned when the wall ability is activated.")]
    public GameObject storm;
    [Tooltip("The object that is spawned when the knife throwing ability is activated.")]
    public GameObject knife;
    [Tooltip("The object that is spawned when the tornado object is used.")]
    public GameObject tornado;
    [Tooltip("The object that is spawned when the infector is used.")]
    public GameObject infector;
    [Tooltip("The object that is spawned when the pushback ability is used.")]
    public GameObject pushBack;

    private void Start()
    {
        m_playerController = GameObject.FindObjectOfType<Player_Controller>();
    }

    public void f_spawnWall() //Spawn a wall. 
    {
        if (Physics.Raycast(shotPoint.position, shotPoint.forward, out m_hitscanCast, Mathf.Infinity)) //Creates a Raycast in direction player is looking.
        {
            GameObject o_wall = Instantiate(wall, new Vector3(m_hitscanCast.point.x, m_hitscanCast.point.y - 2, m_hitscanCast.point.z), Quaternion.LookRotation(Vector3.forward)); //Instantiate a wall that summons at the position of the players crosshair location.
            o_wall.transform.eulerAngles = new Vector3(o_wall.transform.eulerAngles.x, m_playerController.playerRotX, o_wall.transform.eulerAngles.z); //Rotate the wall based on angle of player.
        }
    }

    public void f_spawnTornado() //Spawn a tornado. 
    {
        if (Physics.Raycast(shotPoint.position, shotPoint.forward, out m_hitscanCast, Mathf.Infinity)) //Creates a Raycast.
        {
            Instantiate(tornado, new Vector3(m_hitscanCast.point.x, m_hitscanCast.point.y - 2, m_hitscanCast.point.z), Quaternion.LookRotation(Vector3.forward)); //Spawns a tornado at position of player crosshair.
        }
    }

    public void f_spawnStorm() //Spawn a storm. 
    {
        if (Physics.Raycast(shotPoint.position, shotPoint.forward, out m_hitscanCast, Mathf.Infinity)) //Creates a Raycast.
        {
            Instantiate(storm, new Vector3(m_hitscanCast.point.x, m_hitscanCast.point.y - 2, m_hitscanCast.point.z), Quaternion.LookRotation(Vector3.forward)); //Intantiate a tornado at crosshair location.
        }
    }

    public void f_spawnPushback() //Push enemies back. 
    {
        Instantiate(pushBack, shotPoint.transform.position, shotPoint.rotation); //Spawn the pushback gameobject that adds force to any enemy that is hit.
    }

    public void f_spawnKnives() //Spawn knives. 
    {
        float m_sideDirection = -40; //Angle of first knife.
        for (int i = 0; i < totalKnives; i++) //Instantiate a set amount of knives.
        {
            GameObject m_knife = Instantiate(knife, shotPoint.position, Quaternion.identity);
            Rigidbody m_krb = m_knife.GetComponent<Rigidbody>(); //Access that specific knife RigidBody and >

            m_krb.AddForce(shotPoint.forward * 100);
            m_krb.AddForce(shotPoint.right * m_sideDirection);

            m_sideDirection += 10; //> change the RigidBody force from the right direction so it 'spreads' correctly.
        }
    }

    public void f_spawnInfector() //Spawn infector. 
    {
        if (Physics.Raycast(shotPoint.position, shotPoint.forward, out m_hitscanCast, Mathf.Infinity)) //Creates a Raycast.
        {
            Instantiate(infector, new Vector3(m_hitscanCast.point.x, m_hitscanCast.point.y - 2, m_hitscanCast.point.z), Quaternion.LookRotation(Vector3.forward)); //Spawns a tornade of position of player crosshair.
        }
    }
}
