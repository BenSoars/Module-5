using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // Ben Soars

    public List<string> noteName = new List<string>(); // the storred note names
    public List<string> noteContents = new List<string>(); // the storred note contents  
    public List<Sprite> noteImage = new List<Sprite>(); // the storred note image
    private bool m_isVisible; // if the notes/inventory are visible
    private int currentNote; // the currently viewed note
    public GameObject notesVisible; // the visible Ui element showing the notes

    public Text m_noteNameDisplay; // the note name text area
    public Text m_noteContentsDisplay; // the note contents text area
    public Image m_noteImageDisplay; // the note image area

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)) // toggle the visibility of the notes
        {
            m_isVisible = !m_isVisible;
        }

        notesVisible.SetActive(m_isVisible); // set the gameobject true or false based on the visible bool

        if (m_isVisible && noteName.Count > 0) // if the player has eny notes and it's toggled to be visible
        {
            // display the current note contents on the note on screen
            m_noteNameDisplay.text = noteName[currentNote];
            m_noteContentsDisplay.text = noteContents[currentNote];
            m_noteImageDisplay.sprite = noteImage[currentNote];

            // switch to next note
            if (Input.GetKeyDown(KeyCode.Z))
            {
                currentNote += 1;
                if (currentNote >= noteName.Count) // if the current note doesn't exsist
                {
                    currentNote = 0; // go back to the start, the first note picked up
                }
            }

            // switch to previous note
            if (Input.GetKeyDown(KeyCode.X))
            {
                currentNote -= 1;
                if (currentNote < 0) // if the current note doesn't exsist
                {
                    currentNote = noteName.Count - 1; // go to the most recently picked up note
                }
            }
        }
    }
}
