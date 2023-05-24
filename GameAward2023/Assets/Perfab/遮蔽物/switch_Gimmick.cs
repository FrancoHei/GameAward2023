using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class switch_Gimmick : MonoBehaviour
{
    private CapsuleCollider2D m_collider;

    private PlayerState m_PS;

    public bool m_SW; 

    // Start is called before the first frame update
    void Start()
    {
        m_PS = GameObject.Find("Player").GetComponent<PlayerState>();
        m_SW = false;

        // BoxCollider2Dコンポーネントを取得する
        m_collider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_SW) 
        {
            GetComponent<Light2D>().color = new Color(0.0f, 1.0f, 1.0f,1.0f);
        }
        else 
        {
            GetComponent<Light2D>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
          
            //if (m_PS.Target)
           // {
                m_SW = true;
            //}
           // else
           // {
               // m_SW = false;
          //  }
               

        }
        else
        {
            m_SW = false;
        }
    }
}
