using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb_Controller : MonoBehaviour
{
    private Prototype_Classes r_prototypeClasses;

    public List<Material> m_materials = new List<Material>();

    private void Start()
    {
        r_prototypeClasses = FindObjectOfType<Prototype_Classes>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<MeshRenderer>().material = m_materials[r_prototypeClasses.m_chosenBuff];
    }
}
