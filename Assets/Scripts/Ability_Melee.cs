using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Melee : MonoBehaviour
{
    [Header("Script Properties")]
    private Animator m_animator;
    private bool m_isMelee;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider o_collided)
    {
        if(o_collided.CompareTag("Enemy"))
        {
            o_collided.gameObject.GetComponent<Enemy_Controller>().m_enemyHealth -= 50; //Damage enemy by 50 if collided.
        }
    }

    public void f_melee()
    {
        if (m_isMelee == false)
        {
            m_isMelee = true;
            m_animator.SetBool("Melee", m_isMelee); //Animate player so they can visually see they are attacking.
            Invoke("f_resetAnimation", 0.5f);
        }
    }

    void f_resetAnimation()
    {
        m_isMelee = false;
        m_animator.SetBool("Melee", false);
    }
}
