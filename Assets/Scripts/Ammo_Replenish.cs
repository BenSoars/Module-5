using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Replenish : MonoBehaviour
{
    // Ben Soars
    public string GunType = "ALL"; // the type of the gun
    public int AmmoWorth; // the amount of ammo that pack is worth


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // when the player walks over it
        {
            Gun_Generic Gun = FindObjectOfType<Gun_Generic>(); // find the active weapon

            if (Gun) // if the player has a gun equiped
            {
                if (Gun.m_name == GunType || GunType == "ALL") // if the name of the weapon matches the ammo type or the type is universal
                {
                    // increase the gun ammo count
                    Gun.m_currentAmmo += AmmoWorth;
                    // if the ammo is
                    if (Gun.m_currentAmmo > Gun.m_maxAmmo) { Gun.m_currentAmmo = Gun.m_maxAmmo; }

                    Gun.f_updateUI(); // update the UI so it reflects the current amount
                    Destroy(gameObject); // destroy the ammo so it can't be infinate
                }
            }
        }
    }
}
