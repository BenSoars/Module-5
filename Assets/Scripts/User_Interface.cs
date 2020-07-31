using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Kurtis Watson
public class User_Interface : MonoBehaviour
{
    [Header("Script References")]
    private Wave_System r_waveSystem;
    private Player_Controller m_playerController;
    private Prototype_Classes m_prototypeClasses;

    [Header("Text Mesh Pro UGUI")]
    [Space(2)]
    [Tooltip("Current time left before round end.")]
    public TMPro.TextMeshProUGUI currentTimeText;
    [Tooltip("Current player health.")]
    public TMPro.TextMeshProUGUI currentHealth;
    [Tooltip("Current Starstone charge.")]
    public TMPro.TextMeshProUGUI currentStoneCharge;

    [Header("Runtime Components")]
    private float m_targetTime;
    private int m_currentSecond;
    private int m_currentMinute;

    [Header("In-game Individual Canvas")] //This is to display the starstone power in game.
    public TMPro.TextMeshPro SS1;
    public TMPro.TextMeshPro SS2;
    public TMPro.TextMeshPro SS3;
    public TMPro.TextMeshPro SS4;

    public List<int> waveTimes = new List<int>();

    // Start is called before the first frame update

    private void Start()
    {
        r_waveSystem = FindObjectOfType<Wave_System>();
        m_playerController = FindObjectOfType<Player_Controller>();
        m_prototypeClasses = FindObjectOfType<Prototype_Classes>();
    }
    // Update is called once per frame
    void Update()
    {
        currentHealth.text = "" + m_playerController.playerHealth;

        currentStoneCharge.text = "" + m_prototypeClasses.stonePower[m_prototypeClasses.classState].ToString();

        SS1.text = m_prototypeClasses.stonePower[0].ToString("F0");
        SS2.text = m_prototypeClasses.stonePower[1].ToString("F0");
        SS3.text = m_prototypeClasses.stonePower[2].ToString("F0");
        SS4.text = m_prototypeClasses.stonePower[3].ToString("F0");

        if (r_waveSystem.m_startedWaves == true)
        {
            m_currentSecond = Mathf.FloorToInt(m_targetTime % 60);
            m_currentMinute = Mathf.FloorToInt(m_targetTime / 60);

            m_targetTime -= Time.deltaTime;

            currentTimeText.text = "" + m_currentMinute.ToString("00") + ":" + m_currentSecond.ToString("00");

            if (m_targetTime <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    public void f_waveTimer()
    {
        m_targetTime = waveTimes[r_waveSystem.curRound];
    }
}
