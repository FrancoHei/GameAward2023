using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public float m_FadeInSpd;
    public float m_FadeOutSpd;
    float  m_Alpha = 0.0f;
    float  m_FadeSpd;
    float  m_Time = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, m_Alpha);
        m_FadeSpd = m_FadeInSpd;
    }

    // Update is called once per frame
    void Update()
    {
        m_Alpha += m_FadeSpd;

        if((m_Alpha < 0.0f || m_Alpha > 1.0f) && m_Time < 1.0f) 
        {
            if(m_FadeSpd == m_FadeInSpd) 
            {
                m_FadeSpd = -m_FadeOutSpd;
                m_Time += 0.5f;
            }
            else
            if (m_FadeSpd == -m_FadeOutSpd)
            {
                m_FadeSpd = m_FadeInSpd;
                m_Time += 0.5f;
            }
        }

        if (m_Time >= 1.0f) 
        {
            SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
        }
        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, m_Alpha);

    }
}
