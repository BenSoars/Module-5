using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Replenish : MonoBehaviour
{
    // Ben Soars
    public string GunType = "ALL";
    public int AmmoWorth;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Gun_Generic Gun = FindObjectOfType<Gun_Generic>();

            if (Gun)
            {
                if (Gun.name == GunType || GunType == "ALL")
                {
                    Gun.m_currentAmmo += AmmoWorth;
                    if (Gun.m_currentAmmo > Gun.m_maxAmmo) { Gun.m_currentAmmo = Gun.m_maxAmmo; }
                    
                    Gun.f_updateUI();
                    Destroy(gameObject);
                }
            }
        }
    }
}
