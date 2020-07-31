using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using System;

//Kurtis Watson
public class Prototype_Weapon : MonoBehaviour
{
    [Header("Referenced Scripts")]
    [Space(2)]
    private Player_Controller m_playerController;
    private Prototype_Classes m_prototypeClasses;
    private Enemy_Controller m_enemyHit;

    [Header("Weapon Mechanics")]
    [Space(2)]
    private LineRenderer m_lr;
    public Transform shotPoint;
    public GameObject hitDamageText;
    private float laserDamage;
    public float damageCoolDown;
    private float m_currentDamageCoolDown;

    // Use this for initialization
    void Start()
    {
        damageCoolDown = 0.2f;
        m_currentDamageCoolDown = damageCoolDown;

        m_playerController = FindObjectOfType<Player_Controller>();
        m_prototypeClasses = FindObjectOfType<Prototype_Classes>();

        m_lr = GetComponent<LineRenderer>(); //Access line renderer component.
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        f_prototypeWeapon();

        m_currentDamageCoolDown -= Time.deltaTime;
        laserDamage = UnityEngine.Random.Range(5, 10);
    }


    [System.Obsolete]
    void f_prototypeWeapon()
    {
        RaycastHit m_laserHit;
        if (Input.GetKey(KeyCode.Mouse0)) //Check for left mouse.
        {
            m_lr.SetPosition(0, transform.position); //Shoot the laser from the prototype weapon.
            m_lr.enabled = true;
            if (Physics.Raycast(shotPoint.position, shotPoint.forward, out m_laserHit))
            {
                if (m_laserHit.collider)
                {
                    m_lr.SetPosition(1, m_laserHit.point); //Shoot the laser towards the position of the players crosshair.
                }

                if (m_laserHit.collider.gameObject.CompareTag("Enemy") && m_currentDamageCoolDown <= 0) //If the laser hits an enemy and the cooldown has been met then damage the enemy.
                {
                    m_enemyHit = m_laserHit.collider.gameObject.GetComponent<Enemy_Controller>(); //Grab the 'Enemy_Controller' from the enemy hit.
                    switch (m_prototypeClasses.classState)//Based on players starstone choice, the effects of laser hit are different.
                    {
                        case 0: //Yellow
                            laserDamage = laserDamage * 2; //Double damage.
                            break;
                        case 1: //White
                            break;
                        case 2: //Pink
                            m_enemyHit.m_isStunned = true; //Stun enemy.
                            break;
                        case 3: //Blue
                            m_playerController.playerHealth += 1; //Increase player health (absorb the enemy health).
                            break;
                    }
                    m_laserHit.collider.gameObject.GetComponent<Enemy_Controller>().m_enemyHealth -= laserDamage;      
                    GameObject textObject = Instantiate(hitDamageText, m_laserHit.point, Quaternion.identity);
                    textObject.GetComponentInChildren<TextMeshPro>().text = "" + laserDamage;
                    m_currentDamageCoolDown = damageCoolDown;
                }
            }
        }
        else
        {
            m_lr.enabled = false; //Disable line renderer is the left mouse is not clicked.
        }

        switch (m_prototypeClasses.classState) //Set the colour of the line render based on current starstone.
        {
            case 0:
                m_lr.SetColors(Color.yellow, Color.yellow);
                break;
            case 1:
                m_lr.SetColors(Color.white, Color.white);
                break;
            case 2:
                m_lr.SetColors(Color.magenta, Color.magenta);
                break;
            case 3:
                m_lr.SetColors(Color.blue, Color.blue);
                break;
        }
    }
}