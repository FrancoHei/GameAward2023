using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D m_Rb2D;

    private Vector2 m_Direction;
    private float   m_Speed;

    public Vector2 Direction 
    {
        set { m_Direction = value; }
        get { return m_Direction; }
    }

    public float Speed
    {
        set { m_Speed = value; }
        get { return m_Speed; }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        m_Rb2D.velocity = new Vector2(m_Direction.x * m_Speed, 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.Find("Player"))
        {
            if (!GameObject.Find("GameSystem").GetComponent<GameSystem>().GameOver)
            {
                GameObject.Find("GameSystem").GetComponent<GameSystem>().GameOver = true;
            }
        }
    }
}
