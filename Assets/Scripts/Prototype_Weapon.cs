using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using System;

public class Prototype_Weapon : MonoBehaviour
{
    private Player_Controller r_playerController;
    private Prototype_Classes r_prototypeClasses;
    private Enemy_Controller enemyHit;

    private LineRenderer m_lr;

    public Transform m_shotPoint;

    public GameObject m_hitDamageText;

    public float m_laserDamage;

    public float m_damageCoolDown;
    private float m_currentDamageCoolDown;

    // Use this for initialization
    void Start()
    {
        m_damageCoolDown = 0.2f;
        m_currentDamageCoolDown = m_damageCoolDown;

        r_playerController = FindObjectOfType<Player_Controller>();
        r_prototypeClasses = FindObjectOfType<Prototype_Classes>();
        m_lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        f_prototypeWeapon();

        m_currentDamageCoolDown -= Time.deltaTime;
        m_laserDamage = UnityEngine.Random.Range(5, 10);
    }


    [System.Obsolete]
    void f_prototypeWeapon()
    {
        RaycastHit m_laserHit;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            m_lr.SetPosition(0, transform.position);
            m_lr.enabled = true;
            if (Physics.Raycast(m_shotPoint.position, m_shotPoint.forward, out m_laserHit))
            {
                if (m_laserHit.collider)
                {
                    m_lr.SetPosition(1, m_laserHit.point);
                }

                if (m_laserHit.collider.gameObject.CompareTag("Enemy") && m_currentDamageCoolDown <= 0)
                {
                    enemyHit = m_laserHit.collider.gameObject.GetComponent<Enemy_Controller>();
                    switch (r_prototypeClasses.m_classState)
                    {
                        case 0: //Yellow
                            m_laserDamage = m_laserDamage * 2;
                            break;
                        case 1: //White
                            break;
                        case 2: //Pink
                            enemyHit.m_isStunned = true;
                            break;
                        case 3: //Blue
                            r_playerController.m_playerHealth += 1;
                            break;
                    }
                    m_laserHit.collider.gameObject.GetComponent<Enemy_Controller>().m_enemyHealth -= m_laserDamage;      
                    GameObject textObject = Instantiate(m_hitDamageText, m_laserHit.point, Quaternion.identity);
                    textObject.GetComponentInChildren<TextMeshPro>().text = "" + m_laserDamage;
                    m_currentDamageCoolDown = m_damageCoolDown;
                }
            }
        }
        else
        {
            m_lr.enabled = false;
        }

        switch (r_prototypeClasses.m_classState)
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