using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut2 : MonoBehaviour
{
    public float m_FadeSpd;
    float m_Alpha = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, m_Alpha);
    }

    // Update is called once per frame
    void Update()
    {
        m_Alpha += m_FadeSpd;

        if ((m_Alpha < 0.0f || m_Alpha > 1.0f))
        {
            m_FadeSpd *= -1;
        }

        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, m_Alpha);
    }
}
