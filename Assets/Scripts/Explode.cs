using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    // Ben Soars

    // used for grenades
    public bool m_ExplodeOnContact; // enable if the grenade will explode on contact with any object
    public float m_Timer; // the time before it explodes

    public GameObject Explosion; // the explosion effect gameobject


    // Update is called once per frame
    void Update()
    {
        Invoke("f_createExplosion", m_Timer); // call the explosion effect after, after a passed time
    }

    void OnCollisionEnter(Collision other) // if it collides with another collider
    {
        if (m_ExplodeOnContact) // if it can explode on contact with
        {
            f_createExplosion(); // instnatly create the explosion
        }
    }

    void f_createExplosion() // spawn the explosion effect and destroy self
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
