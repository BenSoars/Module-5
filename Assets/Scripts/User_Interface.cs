using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class User_Interface : MonoBehaviour
{
    //Kurtis Watson
    public TMPro.TextMeshProUGUI m_currentTimeText;
    public TMPro.TextMeshProUGUI m_currentHealth;
    public TMPro.TextMeshProUGUI m_currentStoneCharge;

    public TMPro.TextMeshPro m_SS1;
    public TMPro.TextMeshPro m_SS2;
    public TMPro.TextMeshPro m_SS3;
    public TMPro.TextMeshPro m_SS4;

    private Wave_System r_waveSystem;
    private Player_Controller r_playerController;
    private Prototype_Classes r_prototypeClasses;

    private float m_targetTime;
    private int m_currentSecond;
    private int m_currentMinute;

    public List<int> m_waveTimes = new List<int>();

    // Start is called before the first frame update

    private void Start()
    {
        r_waveSystem = FindObjectOfType<Wave_System>();
        r_playerController = FindObjectOfType<Player_Controller>();
        r_prototypeClasses = FindObjectOfType<Prototype_Classes>();
    }
    // Update is called once per frame
    void Update()
    {
        m_currentHealth.text = "" + r_playerController.m_playerHealth;

        m_currentStoneCharge.text = "" + r_prototypeClasses.m_stonePower[r_prototypeClasses.m_classState].ToString();

        m_SS1.text = r_prototypeClasses.m_stonePower[0].ToString("F0");
        m_SS2.text = r_prototypeClasses.m_stonePower[1].ToString("F0");
        m_SS3.text = r_prototypeClasses.m_stonePower[2].ToString("F0");
        m_SS4.text = r_prototypeClasses.m_stonePower[3].ToString("F0");

        if (r_waveSystem.m_startedWaves == true)
        {
            m_currentSecond = Mathf.FloorToInt(m_targetTime % 60);
            m_currentMinute = Mathf.FloorToInt(m_targetTime / 60);

            m_targetTime -= Time.deltaTime;

            m_currentTimeText.text = "" + m_currentMinute.ToString("00") + ":" + m_currentSecond.ToString("00");

            if (m_targetTime <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    public void f_waveTimer()
    {
        m_targetTime = m_waveTimes[r_waveSystem.curRound];
    }
}
