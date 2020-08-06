using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Kurtis Watson
public class Prototype_Classes : MonoBehaviour
{
    [Header("Script References")]
    [Space(2)]
    private Player_Controller m_playerController; //Reference player controller.
    private Gun_Generic m_gunGeneric;
    [Tooltip("References the prototype weapon attached to player.")]
    public Prototype_Weapon prototypeWeapon;
    private Wave_System m_waveSystem;

    [Header("Default Stats Before Buffs")]
    [Space(2)]
    private float m_defaultDefence; //This section grabs all default values before they are altered.
    private float m_defaultHealth;
    private float m_defaultDamageCooldown;

    [Header("Stone Mechanics")]
    [Space(2)]
    [Tooltip("Array of all the stones current power.")]
    public float[] stonePower;
    [Tooltip("List that stores the current active stones.")]
    public bool[] activeStone;
    public bool newValue;
    private bool m_stonePowerSet;
    [Tooltip("Check for if the player can select a Starstone or not.")]
    public bool canSelect;
    [Tooltip("Current class state (used for prototype classes).")]
    public int classState; //Current weapon state used for switch case.
    [Tooltip("ID stored of the enemies chosen buff.")]
    public int chosenBuff;
    [Tooltip("Position where the laser is shot from.")]
    public Transform shotPoint;
    public float stoneCharge = 0.5f;

    [Header("Environment")]
    [Space(2)]
    [Tooltip("Set the starting intensity of the fog at round start.")]
    public float fogStrength;
    public float currentFog;

    void Start()
    {
        if(m_stonePowerSet == false)
        {
            m_stonePowerSet = true;
            for (int i = 0; i < activeStone.Length; i++)
            {
                stonePower[i] = Random.Range(30, 80);
            }
        }

        currentFog = fogStrength;
        canSelect = true;

        m_playerController = FindObjectOfType<Player_Controller>();
        m_gunGeneric = FindObjectOfType<Gun_Generic>();
        m_waveSystem = FindObjectOfType<Wave_System>();

        m_defaultDefence = m_playerController.m_defenceValue;
        m_defaultHealth = m_playerController.playerHealth;
        m_defaultDamageCooldown = prototypeWeapon.damageCoolDown;
    }

    void Update()
    {
        f_startstoneSelect();
        f_enemyBuff();
        f_chargeStones();
        f_ability();

        if (Input.GetKeyDown("p"))
        {
            classState = 0;
        }
    }

    void f_defaultSettings() //Reset to 0
    {
        for (int i = 0; i < activeStone.Length; i++)
        {
            activeStone[classState] = false;
        }
        currentFog = fogStrength;

        RenderSettings.fog = false;
        m_playerController.m_defenceValue = m_defaultDefence;
        prototypeWeapon.damageCoolDown = m_defaultDamageCooldown;
    }

    void f_startstoneSelect()
    {
        RaycastHit m_stoneSelect;

        if (Physics.Raycast(shotPoint.position, shotPoint.forward, out m_stoneSelect, 3f, 1 << 11) && Input.GetKeyDown("f") && canSelect == true)
        {
            m_waveSystem.newWave = true;
            canSelect = false;
            f_defaultSettings();
            switch (m_stoneSelect.collider.gameObject.name)
            {
                case ("Starstone 1"): //Yellow
                    classState = 0;
                    m_playerController.m_defenceValue = 0.75f;
                    break;
                case ("Starstone 2"): //White
                    classState = 1;
                    m_playerController.playerHealth = m_defaultHealth * 1.3f;
                    break;
                case ("Starstone 3"): //Pink
                    classState = 2;
                    break;
                case ("Starstone 4"): //Blue                   
                    classState = 3;
                    prototypeWeapon.damageCoolDown = m_defaultDamageCooldown / 2;
                    break;                 
            }            

            activeStone[classState] = true;

            float max = int.MinValue;
            for (int i = 0; i < activeStone.Length; i++)
            {                
                if (activeStone[i] == false && stonePower[i] > max)
                {
                    max = stonePower[i];
                    chosenBuff = i;
                }
            }
        }         
    }

    void f_enemyBuff()
    {
        switch (chosenBuff)
        {
            case 0:
                break;
            case 1:
                m_gunGeneric.m_bulletDamage = m_gunGeneric.m_bulletDamage * 0.75f;
                fogStrength = Mathf.Lerp(fogStrength, currentFog, Time.deltaTime * 2); //Smooth fog adjustment.
                RenderSettings.fogDensity = fogStrength;
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
            switch (classState)
            {
                case 0:
                    //Code invisibility.
                    break;
                case 1:
                    if (stonePower[1] >= 15)  //Check if the player has enough starstone energy to use the ability.
                    {
                        FindObjectOfType<Ability_Handler>().f_spawnTornado(); //Spawn a tornado.
                        stonePower[1] -= 15;
                    }
                    break;
                case 2:
                    if (stonePower[2] >= 15)
                    {
                        FindObjectOfType<Ability_Handler>().f_spawnPushback(); //Spawn a pushback object to throw the player away.
                        stonePower[2] -= 15;
                    }
                    break;
                case 3:
                    //Spawn health pad.
                    break;
            }
        }

        if (Input.GetKeyDown("v"))
        {
            switch (classState)
            {
                case 0:
                    if (stonePower[0] >= 20) //Detects if the raycast has hit the ground, if so it allows the player to place.
                    {
                        FindObjectOfType<Ability_Handler>().f_spawnWall(); //Begin Coroutine to execute ability.
                        stonePower[0] -= 20; //Decrease starstone power as it has been 'drained'.
                    }
                    break;
                case 1:
                    if (stonePower[1] >= 25)
                    {
                        FindObjectOfType<Ability_Handler>().f_spawnStorm(); //Spawn a storm.
                        stonePower[1] -= 25;
                    }
                    break;
                case 2:
                    if (stonePower[2] >= 15)
                    {
                        FindObjectOfType<Ability_Handler>().f_spawnKnives(); //Throw a sequence of knives in direction of where the player is looking.
                        stonePower[2] -= 15;
                    }
                    //Knives.
                    break;
                case 3:
                    if (stonePower[3] >= 25)
                    {
                        FindObjectOfType<Ability_Handler>().f_spawnInfector(); //Spawn the infector.
                        stonePower[3] -= 25;
                    }
                    break;
            }           
        }
    }

    void f_chargeStones()
    {
        if (canSelect == false)
        {
            for (int i = 0; i < activeStone.Length; i++)
            {
                if (activeStone[i] != true && stonePower[i] < 100)
                {
                    stonePower[i] += stoneCharge * Time.deltaTime;
                }
                //Debug.Log("Star stone: " + i + "    Power: " + stonePower[i]);
            }
        }
    }

    void f_resetInvisible()
    {
        m_playerController.isPlayerInvisible = false;
    }
}
