using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Switch : MonoBehaviour
{
    // Ben Soars
    public string m_keyPressed; // the key that's been pressed 
    public List<GameObject> m_Weapons = new List<GameObject>(); // the weapon gameobjects

    // Start is called before the first frame update
    void Start()
    {
        f_disableAll(); // disable all the weapons
    }

    // Update is called once per frame
    void Update()
    {

        m_keyPressed = Input.inputString; // get the input button of the player
        switch (m_keyPressed) // switch based on the input pressed
        {
            // switch out weapon based on what button was pressed 
            case ("1"):
                f_disableAll();
                m_Weapons[0].active = true;
                break;
            case ("2"):
                f_disableAll();
                m_Weapons[1].active = true;
                break;
            case ("3"):
                f_disableAll();
                m_Weapons[2].active = true;
                break;

        }
    }

    void f_disableAll() // turn all weapons off
    {
        for (int i = 0; i < m_Weapons.Count; i++)
        {
            m_Weapons[i].active = false; // set all to false
        }
    }
    
}
