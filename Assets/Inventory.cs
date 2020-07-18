using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public List<string> noteName = new List<string>();
    public List<string> noteContents = new List<string>();
    public List<Sprite> noteImage = new List<Sprite>();
    private bool m_isVisible;
    private int currentNote;
    public GameObject notesVisible;

    public Text m_noteNameDisplay;
    public Text m_noteContentsDisplay;
    public Image m_noteImageDisplay;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_isVisible = !m_isVisible;
        }

        notesVisible.SetActive(m_isVisible);

        if (m_isVisible && noteName.Count > 0)
        {
            m_noteNameDisplay.text = noteName[currentNote];
            m_noteContentsDisplay.text = noteContents[currentNote];
            m_noteImageDisplay.sprite = noteImage[currentNote];


            if (Input.GetKeyDown(KeyCode.Z))
            {
                currentNote += 1;
                if (currentNote >= noteName.Count)
                {
                    currentNote = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                currentNote -= 1;
                if (currentNote < 0)
                {
                    currentNote = noteName.Count - 1;
                }
            }
        }
    }
}
