using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Switch : MonoBehaviour
{
    // Ben Soars
    public string m_keyPressed;
    public List<GameObject> m_Weapons = new List<GameObject>();
    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < m_Weapons.Count; i++)
        {
            m_Weapons[i].active = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        m_keyPressed = Input.inputString;
        switch (m_keyPressed)
        {
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

    void f_disableAll()
    {
        for (int i = 0; i < m_Weapons.Count; i++)
        {
            m_Weapons[i].active = false;
        }
    }
    
}
