using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text_HitDamage : MonoBehaviour
{
    private Player_Controller r_playerController;
    private Gun_Generic r_gunGeneric;
    private Prototype_Weapon r_prototypeWeapon;

    private float m_bulletDamageText;

    private bool m_textUpdated;
    // Start is called before the first frame update
    void Start()
    {
        r_playerController = FindObjectOfType<Player_Controller>();
        r_gunGeneric = FindObjectOfType<Gun_Generic>();
        r_prototypeWeapon = FindObjectOfType<Prototype_Weapon>();       
    }

    // Update is called once per frame
    void Update()
    {     
        float m_distance = Vector3.Distance(transform.position, r_playerController.transform.position);

        if(m_distance < 1)
        {
            Destroy(gameObject);
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, r_playerController.m_playerRotX, transform.eulerAngles.z);
        transform.Translate(Vector3.up * (Time.deltaTime * 1.5f));
        transform.position = Vector3.MoveTowards(transform.position, r_playerController.transform.position, Time.deltaTime * 2);
    }
}
