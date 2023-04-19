using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScale : MonoBehaviour
{
    public Vector3 m_LowestScale;
    public Vector3 m_BiggestScale;
    public float   m_ScaleSpeed;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = m_LowestScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += new Vector3(m_ScaleSpeed, m_ScaleSpeed, 0.0f); 
        if((transform.localScale.x > m_BiggestScale.x && transform.localScale.y > m_BiggestScale.y) ||
           (transform.localScale.x < m_LowestScale.x && transform.localScale.y < m_LowestScale.y))
        {
            m_ScaleSpeed *= -1;
        }

    }
}
