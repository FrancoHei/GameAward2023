using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyAI2 : MonoBehaviour
{


    private Rigidbody2D m_Rb2D;

    private Vector2 m_StartPos;
    

    private Vector3 m_Vel;
    public float m_Speed;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Rb2D     = GetComponent<Rigidbody2D>();
        m_StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (GetComponent<EnemyState>().State) 
        {
            case EnemyState.EnemyAiState.NORMAL:
                NormalState();
                break;
            case EnemyState.EnemyAiState.VIGILANCE:
                VigilanceState();
                break;
            case EnemyState.EnemyAiState.DISCOVER:
                Discover();
                break;
            case EnemyState.EnemyAiState.RETURNTOSTARTPOINT:
                BackToStartPoint();
                break;
            default:
                break;
        }


    }
    void FixedUpdate()
    {
        m_Rb2D.velocity = new Vector2(m_Vel.x * m_Speed, m_Rb2D.velocity.y);
        SetRotate();
    }

    void NormalState() 
    {
        m_Vel = Vector3.zero;
        transform.Find("Light 2D").gameObject.GetComponent<Light2D>().intensity = 0;
        transform.Find("Net").gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void VigilanceState() 
    {
        m_Vel = Vector3.zero;
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward);
        transform.rotation    = toRotation;
        transform.Find("Light 2D").gameObject.GetComponent<Light2D>().intensity = 1;
        transform.Find("Net").gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().color = Color.cyan;
    }

    void Discover() 
    {
        transform.Find("Light 2D").gameObject.GetComponent<Light2D>().intensity = 0;
        transform.Find("Net").gameObject.SetActive(true);
        GetComponent<SpriteRenderer>().color = Color.red;
        Vector3 playerPos = GameObject.Find("Player").transform.position;
                    m_Vel = playerPos - transform.position;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == GameObject.Find("Player"))
        {
            GameObject gs = GameObject.Find("GameSystem");
            
            if(gs && GetComponent<EnemyState>().State == EnemyState.EnemyAiState.DISCOVER)
                gs.GetComponent<GameSystem>().GameOver = true;


        }
    }

    private void SetRotate()
    {
        if (m_Rb2D.velocity.x > 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward);
            transform.rotation    = toRotation;
        }

        if (m_Rb2D.velocity.x < 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(-Vector3.forward);
            transform.rotation    = toRotation;
        }
    }

    private void BackToStartPoint() 
    {
        transform.Find("Light 2D").gameObject.GetComponent<Light2D>().intensity = 0;
        transform.Find("Net").gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().color = Color.white;
        m_Vel = new Vector3(m_StartPos.x, m_StartPos.y, 0.0f) - transform.position;

    }
}
