using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Prototype_Classes : MonoBehaviour
{
    //Kurtis Watson
    private Player_Controller r_playerController;
    private Gun_Generic r_gunGeneric;
    public Prototype_Weapon r_prototypeWeapon;
    private Wave_System r_waveSystem;

    public Transform m_shotPoint;

    public GameObject m_pushBack;

    public int m_classState;

    private float m_defaultDefence;
    private float m_defaultHealth;
    private float m_defaultDamageCooldown;
    private float m_defaultBulletDamage;

    public float[] m_stonePower;
    public bool[] m_activeStone;

    public bool newValue;
    private bool m_stonePowerSet;
    public bool m_canSelect;

    public int m_chosenBuff;

    public float m_fogStrength;
    public float m_currentFog;


    void Start()
    {
        if(m_stonePowerSet == false)
        {
            m_stonePowerSet = true;
            for (int i = 0; i < m_activeStone.Length; i++)
            {
                m_stonePower[i] = Random.Range(30, 80);
            }
        }

        m_currentFog = m_fogStrength;
        m_canSelect = true;

        r_playerController = FindObjectOfType<Player_Controller>();
        r_gunGeneric = FindObjectOfType<Gun_Generic>();
        r_waveSystem = FindObjectOfType<Wave_System>();

        m_defaultDefence = r_playerController.m_defenceValue;
        m_defaultHealth = r_playerController.m_playerHealth;
        m_defaultDamageCooldown = r_prototypeWeapon.m_damageCoolDown;
        m_defaultBulletDamage = r_gunGeneric.m_bulletDamage;
    }

    void Update()
    {
        f_startstoneSelect();
        f_enemyBuff();
        f_chargeStones();
        f_ability();

        if (Input.GetKeyDown("p"))
        {
            m_classState = 0;
        }
    }

    void f_defaultSettings() //Reset to 0
    {
        for (int i = 0; i < m_activeStone.Length; i++)
        {
            m_activeStone[m_classState] = false;
        }
        m_currentFog = m_fogStrength;

        RenderSettings.fog = false;
        r_playerController.m_defenceValue = m_defaultDefence;
        r_prototypeWeapon.m_damageCoolDown = m_defaultDamageCooldown;
        r_gunGeneric.m_bulletDamage = m_defaultBulletDamage;
    }

    void f_startstoneSelect()
    {
        RaycastHit m_stoneSelect;

        if (Physics.Raycast(m_shotPoint.position, m_shotPoint.forward, out m_stoneSelect, 3f, 1 << 11) && Input.GetKeyDown("f") && m_canSelect == true)
        {
            r_waveSystem.m_newWave = true;
            m_canSelect = false;
            f_defaultSettings();
            switch (m_stoneSelect.collider.gameObject.name)
            {
                case ("Starstone 1"): //Yellow
                    m_classState = 0;
                    r_playerController.m_defenceValue = 0.75f;
                    break;
                case ("Starstone 2"): //White
                    m_classState = 1;
                    r_playerController.m_playerHealth = m_defaultHealth * 1.3f;
                    break;
                case ("Starstone 3"): //Pink
                    m_classState = 2;
                    break;
                case ("Starstone 4"): //Blue                   
                    m_classState = 3;
                    r_prototypeWeapon.m_damageCoolDown = m_defaultDamageCooldown / 2;
                    break;                 
            }            

            m_activeStone[m_classState] = true;

            float max = int.MinValue;
            for (int i = 0; i < m_activeStone.Length; i++)
            {                
                if (m_activeStone[i] == false && m_stonePower[i] > max)
                {
                    max = m_stonePower[i];
                    m_chosenBuff = i;
                }
            }
        }         
    }

    void f_enemyBuff()
    {
        switch (m_chosenBuff)
        {
            case 0:
                
                break;
            case 1:
                r_gunGeneric.m_bulletDamage = r_gunGeneric.m_bulletDamage * 0.75f;
                m_fogStrength = Mathf.Lerp(m_fogStrength, m_currentFog, Time.deltaTime * 2); //Smooth fog adjustment.
                RenderSettings.fogDensity = m_fogStrength;
                RenderSettings.fog = true;
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }

    void f_ability()
    {
        if (Input.GetKeyDown("q"))
        {
            switch (m_classState)
            {
                case 0:
                    r_playerController.m_isPlayerInvisible = true;
                    Invoke("f_resetInvisible", 10);
                    break;
                case 1:
                    if (m_stonePower[1] >= 15)
                    {
                        FindObjectOfType<Ability_Handler>().f_spawnTornado();
                        m_stonePower[1] -= 15;
                    }
                    break;
                case 2:
                    if (m_stonePower[2] >= 15)
                    {
                        Instantiate(m_pushBack, m_shotPoint.transform.position, m_shotPoint.rotation); // 'm_shotPoint.rotation' makes the position of firing relative to where the player is looking based on camera rotation.
                        m_stonePower[2] -= 15;
                    }
                        break;
                case 3:
                    //Ring of blue fire that enemies can't come into. -10% power.
                    break;
            }
        }

        if (Input.GetKeyDown("v"))
        {
            switch (m_classState)
            {
                case 0:
                    if (m_stonePower[0] >= 20)
                    {
                        FindObjectOfType<Ability_Handler>().f_spawnWall();
                        m_stonePower[0] -= 20;
                    }
                    break;
                case 1:
                    if (m_stonePower[1] >= 25)
                    {
                        FindObjectOfType<Ability_Handler>().f_spawnStorm();
                        m_stonePower[1] -= 25;
                    }
                    break;
                case 2:
                    if (m_stonePower[2] >= 15)
                    {
                        FindObjectOfType<Ability_Handler>().f_spawnKnives();
                        m_stonePower[2] -= 15;
                    }
                    //Knives.
                    break;
                case 3:
                    if (m_stonePower[3] >= 25)
                    {
                        FindObjectOfType<Ability_Handler>().f_spawnInfector();
                        m_stonePower[3] -= 25;
                    }
                    break;
            }           
        }
    }

    void f_chargeStones()
    {
        if (m_canSelect == false)
        {
            for (int i = 0; i < m_activeStone.Length; i++)
            {
                if (m_activeStone[i] != true && m_stonePower[i] < 100)
                {
                    m_stonePower[i] += 0.002f;
                }
                //Debug.Log("Star stone: " + i + "    Power: " + m_stonePower[i]);
            }
        }
    }

    void f_resetInvisible()
    {
        r_playerController.m_isPlayerInvisible = false;
    }
}
