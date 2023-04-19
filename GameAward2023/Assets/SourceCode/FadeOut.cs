using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public float m_FadeOutSpd;
    float m_Alpha = 1.0f;
    float m_FadeSpd;
    bool m_IsFadeOut = false;

    public bool IsFadeOut
    {
        get{ return m_IsFadeOut;  }
        set{ m_IsFadeOut = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, m_Alpha);
        GameObject.Find("GameSystem").GetComponent<GameSystem>().CanInput = false;
        m_FadeSpd = -m_FadeOutSpd;
    }

    // Update is called once per frame
    void Update()
    {
        m_Alpha += m_FadeSpd;

        if(m_Alpha <= 0 && !m_IsFadeOut) 
        {
            m_IsFadeOut = true;
            GameObject.Find("GameSystem").GetComponent<GameSystem>().CanInput = true;
        }

        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, m_Alpha);

    }
}
