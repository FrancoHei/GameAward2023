using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BulletUseEnemyAI : MonoBehaviour
{

    public float m_BulletSpeed;
    public float m_BulletFlyDistance;
    public int   m_BulletShootSpace;

    private int  m_BulletShootTimer = 0;
    private int m_StateNumber = 0;
    private int m_MoveNumber = 0;


    private Vector3 m_StartPosition;
    private Vector3 m_GizmosPosition;
    private Rigidbody2D rb2D;

    private Vector3 LightposOffset;

    [System.Serializable] public struct TargetStruct
    {
        [Tooltip("移動先座標(ゲーム開始時からの相対座標)")] public Vector2 MovePosition;
        [Tooltip("移動速度(移動にかかる時間)")] public float MoveSpeed;
        [Tooltip("回転角度"), Range(0.0f, 360.0f)] public float RotationZ;
        [Tooltip("移動終了からの待機時間")] public float WeitTime;
    }

    [SerializeField][Tooltip("使用時 プレイヤーの初期位置はリストに入っていないので注意")] private List<TargetStruct> m_MovePosition;




    public GameObject m_Bullet;

    [SerializeField] private GameObject LightObject;
    private Vector3 m_Vel;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        m_StartPosition = transform.position;
        m_GizmosPosition = transform.position;
        m_StateNumber = 0;
        m_MoveNumber = 0;
        LightposOffset = LightObject.transform.position - transform.position;
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
                GetComponent<EnemyState>().State = EnemyState.EnemyAiState.NORMAL;
                break;
            default:
                break;
        }

        LightObject.transform.position =
             (LightposOffset + transform.position) * Mathf.Cos(Mathf.Deg2Rad * transform.rotation.z) 
           + (LightposOffset + transform.position) * Mathf.Sin(Mathf.Deg2Rad * transform.rotation.z);

        Vector3 rotBuff = LightObject.transform.eulerAngles;
        rotBuff.z = 90 - transform.eulerAngles.z;
        LightObject.transform.eulerAngles = rotBuff;


    }
    void FixedUpdate()
    {

    }

    void NormalState() 
    {
        LightObject.GetComponent<Light2D>().intensity = 0;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void VigilanceState() 
    {
        LightObject.GetComponent<Light2D>().intensity = 1;
        GetComponent<SpriteRenderer>().color = Color.cyan;

        if (m_MovePosition.Count > 0)
        {
            Vector3 Position = m_MovePosition[m_StateNumber].MovePosition;


            Vector3 MoveTargetPosition;
            float Rotation = 1;
            float Speed = 1;
            float WeitTime = 1;
            if (m_StateNumber + 1 >= m_MovePosition.Count)
            {
                MoveTargetPosition = m_MovePosition[0].MovePosition;
                Rotation = m_MovePosition[0].RotationZ;
                Speed = m_MovePosition[0].MoveSpeed;
                WeitTime = m_MovePosition[0].WeitTime;
            }
            else
            {
                MoveTargetPosition = m_MovePosition[m_StateNumber + 1].MovePosition;
                Rotation = m_MovePosition[m_StateNumber + 1].RotationZ;
                Speed = m_MovePosition[m_StateNumber + 1].MoveSpeed;
                WeitTime = m_MovePosition[m_StateNumber + 1].WeitTime;
            }


            Vector3 rotBuffer = transform.eulerAngles;
            rotBuffer.z = 180 - Rotation;
            transform.eulerAngles = rotBuffer;

            Vector3 OneMoveVector = (MoveTargetPosition - Position) / Speed;
            transform.position = m_StartPosition + (OneMoveVector * m_MoveNumber);

            
            m_MoveNumber += 1;
            if (m_MoveNumber >= Speed)
            {
                transform.position = m_StartPosition + (OneMoveVector * Speed);

                if (WeitTime + Speed < m_MoveNumber)
                {
                    m_StateNumber += 1;
                    m_MoveNumber = 0;
                    m_StartPosition = transform.position;
                    if (m_StateNumber >= m_MovePosition.Count)
                    {
                        m_StateNumber = 0;
                    }
                }
            }
        }



    }

    void Discover() 
    {
        LightObject.GetComponent<Light2D>().intensity = 0;
        GetComponent<SpriteRenderer>().color = Color.red;

        if(m_BulletShootTimer % m_BulletShootSpace == 0) 
        {
            float rotation = 180 - transform.eulerAngles.z;
            GameObject obj = Instantiate(m_Bullet, transform.position + Vector3.right * Mathf.Cos(rotation) + Vector3.up * Mathf.Sin(rotation), Quaternion.identity);
            obj.GetComponent<newBullet>().Speed     = m_BulletSpeed;
            obj.GetComponent<newBullet>().Distance = m_BulletFlyDistance;

            Vector3 rotBuffer = transform.eulerAngles;
            rotBuffer.z = rotation;
            obj.transform.eulerAngles = rotBuffer;
        }

        m_BulletShootTimer++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.Find("Player"))
        {
            if(GetComponent<EnemyState>().State == EnemyState.EnemyAiState.VIGILANCE) 
            {
                //プレイヤーを見つける部分
                GetComponent<EnemyState>().State = EnemyState.EnemyAiState.DISCOVER;
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (!EditorApplication.isPlaying)
        {
            m_StartPosition = transform.position;
            m_GizmosPosition = transform.position;
        }


        LightObject.transform.position =
             (LightposOffset + transform.position) * Mathf.Cos(Mathf.Deg2Rad * transform.rotation.z)
           + (LightposOffset + transform.position) * Mathf.Sin(Mathf.Deg2Rad * transform.rotation.z);

        Vector3 rotBuff = LightObject.transform.eulerAngles;
        rotBuff.z = 90 - transform.eulerAngles.z;
        LightObject.transform.eulerAngles = rotBuff;


        foreach (TargetStruct target in m_MovePosition)
        {
            Vector3 pos = target.MovePosition;

            pos += m_GizmosPosition;

            float rot = Mathf.Deg2Rad * target.RotationZ;

            Gizmos.color = Color.red;
            Vector3 Size = Vector3.right * 0.2f + Vector3.up * 0.2f;
            Gizmos.DrawWireCube(pos, Size);

            Gizmos.color = Color.yellow;
            Vector3 Direction = pos + Vector3.right * Mathf.Cos(rot) + Vector3.up * Mathf.Sin(rot);
            Gizmos.DrawLine(pos, Direction);

            Vector3 Angle = Direction + Vector3.right * 0.2f * Mathf.Cos(60 + rot) + Vector3.up * 0.2f * Mathf.Sin(60 + rot);
            Gizmos.DrawLine(Direction, Angle);

            Angle = Direction + Vector3.right * 0.2f * Mathf.Cos(-60 + rot) + Vector3.up * 0.2f * Mathf.Sin(-60 + rot);
            Gizmos.DrawLine(Direction, Angle);

        }



    }
}
