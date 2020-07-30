using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Ben Soars
    public string m_LevelName; // the level/scene to load to
    public float m_delayTime; // the time it takes to load to that scene
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadNextScene", m_delayTime); // invoke the loading with the passed time
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(m_LevelName); // load desired scene
    }
}
