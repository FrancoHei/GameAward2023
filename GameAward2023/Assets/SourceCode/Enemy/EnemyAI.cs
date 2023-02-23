using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyAiState
    {
        NORMAL,
        VIGILANCE,
        DISCOVER
    }

    private EnemyAiState m_State = EnemyAiState.NORMAL;

    public float m_BulletSpeed;
    public int   m_BulletShootSpace;

    private int  m_BulletShootTimer = 0;
    public EnemyAiState State 
    {
        set { m_State = value; }
        get { return m_State; }
    }


    private Rigidbody2D rb2D;

    public GameObject m_Bullet;

    public Vector3 m_Point_1;
    public Vector3 m_Point_2;

    private Vector3 m_Vel;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_State) 
        {
            case EnemyAiState.NORMAL:
                NormalState();
                break;
            case EnemyAiState.VIGILANCE:
                VigilanceState();
                break;
            case EnemyAiState.DISCOVER:
                Discover();
                break;
            default:
                break;
        }


    }
    void FixedUpdate()
    {

    }

    void NormalState() 
    {
        transform.Find("Light 2D").gameObject.GetComponent<Light2D>().intensity = 0;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void VigilanceState() 
    {

        transform.Find("Light 2D").gameObject.GetComponent<Light2D>().intensity = 1;
        GetComponent<SpriteRenderer>().color = Color.cyan;
    }

    void Discover() 
    {
        transform.Find("Light 2D").gameObject.GetComponent<Light2D>().intensity = 0;
        GetComponent<SpriteRenderer>().color = Color.red;

        m_BulletShootTimer++;

        if(m_BulletShootTimer % m_BulletShootSpace == 0) 
        {
            GameObject obj = Instantiate(m_Bullet, transform.Find("ShootPosition").position, Quaternion.identity);
            obj.GetComponent<Bullet>().Speed     = m_BulletSpeed;
            obj.GetComponent<Bullet>().Direction = new Vector2(transform.forward.z,0.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.Find("Player"))
        {
            if(m_State == EnemyAiState.VIGILANCE) 
            {
                m_State = EnemyAiState.DISCOVER;
            }
        }
    }
}
