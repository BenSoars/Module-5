using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    // Ben Soars

    public float m_activeTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("f_Delete", m_activeTime);
    }

    void f_Delete()
    {
        Destroy(gameObject);
    }
}
