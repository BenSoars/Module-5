using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{

    public string NoteName;
    public string NoteText;
    public Sprite NoteImage;

    private Inventory m_invSystem;

    void Start()
    {
        m_invSystem = GameObject.FindObjectOfType<Inventory>();
    }

    // Start is called before the first frame update
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pickUpNote();
        }
    }


    void pickUpNote()
    {
        m_invSystem.noteName.Add(NoteName);
        m_invSystem.noteContents.Add(NoteText);
        m_invSystem.noteImage.Add(NoteImage);
        Destroy(gameObject);
    }
}
