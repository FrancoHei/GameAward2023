using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerText : MonoBehaviour
{

    private bool m_StartTimer  = false;
    private int  m_Second      = 0;
    public float m_Size        = 0;

    private int  m_MicroSecond = 0;

    private TextMeshProUGUI m_Gui;
    public bool StartTimer 
    {
        set { m_StartTimer = value; }
        get { return m_StartTimer; }
    }

    public int Second 
    {
        set { m_Second = value; }
        get { return m_Second; }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Gui = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

        if (m_StartTimer) 
        {
            m_MicroSecond--;
            if(m_MicroSecond <= 0) 
            {
                m_Second--;
                if(m_Second < 0) 
                {
                    GameObject.Find("GameSystem").GetComponent<GameSystem>().BlackOut = false;
                    m_StartTimer = false;
                }
                else 
                {
                    m_MicroSecond = 59;
                }
            }
        }

        if (!m_StartTimer)
        {
            if (m_Gui)
                m_Gui.fontSize = 0;
        }
        else
        {
            if (m_Gui)
                m_Gui.fontSize = m_Size;
        }

        if (m_Gui) 
        {

            string microSec;

            if(m_MicroSecond < 10) 
            {
                microSec = "0" + m_MicroSecond.ToString();
            }else 
            {
                microSec = m_MicroSecond.ToString();
            }

            string sec;


            if (m_Second < 10)
            {
                sec = "0" + m_Second.ToString();
            }
            else
            {
                sec = m_Second.ToString();
            }

            m_Gui.text = sec + ":" + microSec;
        }

    }
}
