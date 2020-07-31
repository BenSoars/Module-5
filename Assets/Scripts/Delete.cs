using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    // Ben Soars

    public float m_activeTime = 2; // the amount of time the object is active fore before deleting
    // Start is called before the first frame update
    void Start()
    {
        Invoke("f_Delete", m_activeTime); // invoke the destruction of the object with the set time
    }

    void f_Delete()
    {
        Destroy(gameObject); // destroy
    }
}
