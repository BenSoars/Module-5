using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Kurtis Watson
public class Orb_Controller : MonoBehaviour
{
    [Header("Script References")]
    private Prototype_Classes m_prototypeClasses; //Reference prototype classes script.

    [Header("Materials")]
    [Space(2)]
    [Tooltip("Store the different materials the orb can change to based on the chosen enemy buff.")]
    public List<Material> materials = new List<Material>(); //List of materials used.

    private void Start()
    {
        m_prototypeClasses = FindObjectOfType<Prototype_Classes>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<MeshRenderer>().material = materials[m_prototypeClasses.chosenBuff]; //Set the colour of the orb based on the enemy type.
    }
}
