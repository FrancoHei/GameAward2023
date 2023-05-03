using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody2D m_Rb2D;
    //ƒWƒƒƒ“ƒv“–‚½‚éLAYER
    public LayerMask m_OnFloorHitLayer;
    private bool m_StartThrow;
    private bool m_CanCatch = true;

    public bool StartThrow
    {
        set { m_StartThrow = value; }
        get { return m_StartThrow; }
    }

    public bool CanCatch 
    {
        set { m_CanCatch = value; }
        get { return m_CanCatch;  }
    }

    public float m_CheckFloorDistance;
    // Start is called before the first frame update
    void Start()
    {
        m_Rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_StartThrow) 
        {
            CheckOnFloor();
        }

        if (!m_CanCatch && m_Rb2D.velocity.y <= 0.0f)
        {
            m_CanCatch = true;
        }
    }

    private void CheckOnFloor()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, m_CheckFloorDistance, m_OnFloorHitLayer);

        if (hit && (hit.transform.gameObject.tag == "Ground" || hit.transform.gameObject.tag == "Wall"))
        {
            if(m_Rb2D.velocity.y < 0.0f) 
            {
                GameObject.Find("GameSystem").GetComponent<GameSystem>().GameOver = true;
                //’n–Ê‚É‚¢‚é
                m_Rb2D.velocity = Vector3.zero;
            }

        }
        else
        {
        }

    }
}
