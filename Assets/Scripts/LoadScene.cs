using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public string m_LevelName;
    public float m_delayTime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadNextScene", m_delayTime);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(m_LevelName);
    }
}
