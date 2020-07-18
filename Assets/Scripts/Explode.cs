using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    // Ben Soars

    public bool m_ExplodeOnContact;
    public float m_Timer;

    public GameObject Explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("f_createExplosion", m_Timer);
    }

    void OnCollisionEnter(Collision other)
    {
        if (m_ExplodeOnContact)
        {
            f_createExplosion();
        }
    }

    void f_createExplosion()
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
