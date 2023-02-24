using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_Rb2D;

    //プレイヤー動くスピード
    public float m_Speed;
    //ジャンプパワー(ジャンプ高さ)
    public float m_JumpPower;


    //プレイヤーインプット
    private Vector3 m_MoveMentInput;
    //プレイヤー空時インプット
    private Vector3 m_OnAirMoveMentInput;

    private Vector3 m_FirstJumpVel;

    //地面チェックしてるか？
    private bool m_OnFloor;
    //ジャンプ当たるLAYER
    public LayerMask m_OnFloorHitLayer;
    //ジャンプ判定距離
    public float m_OnFloorDistance;

    //-----------------------------------
    //壁ジャンプ
    //-----------------------------------
    //壁ジャンプしてるか？
    private bool m_WallJump;
    //壁ジャンプ当たるLAYER
    public LayerMask m_WallJumpHitLayer;
    //壁ジャンプ判定距離
    public float m_WallJumpDistance;
    //壁ジャンプパワー(ジャンプ高さ)
    public float m_WallJumpPower;
    //壁ジャンプパワー(ジャンプ長さ)
    public float m_WallJumpSpeed;
    //壁ジャンプ与える速度
    private Vector3 m_WallJumpVel;
    //-----------------------------------

    //-----------------------------------
    //ダブルジャンプ
    //-----------------------------------
    //ダブルジャンプしてるか？
    private bool m_DoubleJump;
    //ダブルジャンプ当たるLAYER
    public LayerMask m_DoubleJumpHitLayer;
    //ダブルジャンプ判定距離
    public float m_DoubleJumpDistance;
    //ダブルジャンプパワー(ジャンプ高さ)
    public float m_DoubleJumpPower;
    //ダブルジャンプパワー(ジャンプ長さ)
    public float m_DoubleJumpSpeed;
    //ダブルジャンプ与える速度
    private Vector3 m_DoubleJumpVel;

    //-----------------------------------
    //スライド
    //-----------------------------------
    private bool m_Slide;
    //スライド与える速度
    private Vector3 m_SlideVel;
    public float    m_SlideSpeed;
    public int      m_MaxSlideTimer;
    private float   m_SlideTimer;


    public float    m_WallJumpSnowMotion;
    public Vector2  m_WallJumpSnowMotionRange;

    private GameObject m_Target = null;

    public GameObject Target 
    {
        set { m_Target = value; }
        get { return m_Target; }
    }

    void Start()
    {
        m_Rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Target)
            m_Target.transform.position = transform.position + new Vector3(transform.forward.z * 0.5f, transform.up.y * 0.5f, 0.0f);
        //var cursorPosition = Mouse.current.position.ReadValue();

    }

    void FixedUpdate()
    {
        //地面チェック
        CheckOnFloor();

        //地面時速度計算
        if (m_OnFloor)
            m_Rb2D.velocity = new Vector2(m_MoveMentInput.x * m_Speed, m_Rb2D.velocity.y);
        //壁ジャンプ時速度計算
        if (m_WallJumpVel != Vector3.zero)
            m_Rb2D.velocity = new Vector2(m_WallJumpVel.x * m_WallJumpSpeed, m_Rb2D.velocity.y);
        if (m_SlideVel != Vector3.zero)
            m_Rb2D.velocity = new Vector2(m_SlideVel.x * m_SlideSpeed, m_Rb2D.velocity.y);
        if (m_DoubleJumpVel != Vector3.zero)
            m_Rb2D.velocity = new Vector2(m_DoubleJumpVel.x * m_DoubleJumpSpeed, m_Rb2D.velocity.y);

        SlideTimer();
        //回転
        SetRotate();

        HandleDelayUpdate();
        Debug.Log(m_Rb2D.velocity);
    }


    public void OnMove(InputValue input)
    {
        m_MoveMentInput = input.Get<Vector2>();

        if (!m_OnFloor)
        {
            m_OnAirMoveMentInput = m_MoveMentInput;
        }
    }

    public void OnJump(InputValue input)
    {
        if (m_OnFloor)
        {
            //ジャンプ処理
            HandleJump();
            return;
        }
        else
        {
            //壁ジャンプ処理
            if (CheckCanWallJump() != 0)
            {
                Debug.Log("Perform Wall Jump");
                HandleWallJump();
                m_WallJump = true;
                return;
            }

            if (!m_DoubleJump && CheckCanDoubleJump())
            {
                Debug.Log("Perform Double Jump");
                HandleDoubleJump();
                m_DoubleJump = true;
                return;
            }
        }
    }

    public void OnSlide(InputValue input)
    {
        if (!m_OnFloor) return;

        GetComponents<CapsuleCollider2D>()[0].enabled = false;
        GetComponents<CapsuleCollider2D>()[1].enabled = true;

        m_Slide    = true;
        m_SlideVel = new Vector3(transform.forward.z, 0.0f, 0.0f);
    }

    private void HandleJump()
    {
        m_Rb2D.AddForce(Vector3.up * m_JumpPower);
        m_FirstJumpVel = new Vector3(m_Rb2D.velocity.x, 0.0f, 0.0f);
    }

    private void HandleWallJump()
    {
        if (m_DoubleJump)
        {
            InitDoubleJump();
        }

        m_Rb2D.velocity = Vector2.zero;

        m_Rb2D.AddForce(Vector3.up * m_WallJumpPower);

        m_WallJumpVel.x = CheckCanWallJump();
    }

    private void HandleDoubleJump()
    {
        m_DoubleJumpVel.x = m_FirstJumpVel.x;

        m_Rb2D.velocity   = Vector2.zero;

        m_Rb2D.AddForce(Vector3.up * m_DoubleJumpPower);
    }

    private void CheckOnFloor()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, m_OnFloorDistance, m_OnFloorHitLayer);

        if (hit && hit.transform.gameObject.tag == "Ground")
        {
            //地面到着瞬間
            if (!m_OnFloor)
            {
                //壁ジャンプ初期化
                if (m_WallJump)
                {
                    InitWallJump();
                }

                if (m_DoubleJump)
                {
                    InitDoubleJump();
                }

                m_Rb2D.velocity = new Vector2((m_MoveMentInput.x + m_WallJumpVel.x + m_OnAirMoveMentInput.x) * m_Speed, m_Rb2D.velocity.y);
            }
            //地面にいる
            m_OnFloor = true;
        }
        else
        {
            //空中
            m_OnFloor = false;
        }

    }

    private int CheckCanWallJump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, m_WallJumpDistance, m_WallJumpHitLayer);

        if (hit && hit.transform.gameObject.tag == "Wall")
        {
            return -1;
        }

        hit = Physics2D.Raycast(transform.position, -Vector2.right, m_WallJumpDistance, m_WallJumpHitLayer);

        if (hit && hit.transform.gameObject.tag == "Wall")
        {
            return 1;
        }

        return 0;
    }

    private bool CheckCanWallJumpSnowMotion()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(transform.forward.z, 0.0f), m_WallJumpDistance, m_WallJumpHitLayer);

        if (hit && hit.transform.gameObject.tag == "Wall")
        {
            return true;
        }

        return false;
    }

    private bool CheckCanDoubleJump()
    {
        if (m_Rb2D.velocity.y > 0) return false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, m_DoubleJumpDistance, m_DoubleJumpHitLayer);

        if (hit && hit.transform.gameObject.tag == "Ground")
        {
            return true;
        }
        else
        {
            return false;
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

    private void SlideTimer()
    {
        if (m_Slide)
        {
            m_SlideTimer++;
            if (m_SlideTimer > m_MaxSlideTimer)
            {
                m_SlideVel   = Vector3.zero;
                m_SlideTimer = 0;
                m_Slide      = false;
                GetComponents<CapsuleCollider2D>()[0].enabled = true;
                GetComponents<CapsuleCollider2D>()[1].enabled = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            m_Target = collision.gameObject;
            m_Target.GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }

    private void HandleDelayUpdate() 
    {
        if (!m_OnFloor) 
        {
            if(CheckCanWallJump() != 0 && m_Rb2D.velocity.y <= m_WallJumpSnowMotionRange.y && m_Rb2D.velocity.y >= m_WallJumpSnowMotionRange.x) 
            {
                if (CheckCanWallJumpSnowMotion()) 
                {
                    Time.timeScale = m_WallJumpSnowMotion;
                    return;
                }
            }
        }

        //if (!m_OnFloor && m_Target && CheckCanDoubleJump() && !m_DoubleJump)
        //{
        //    Time.timeScale = 0.1f;
        //    return;
        //}

        Time.timeScale = 1.0f;
    }

    private void InitDoubleJump() 
    {
        m_DoubleJumpVel = Vector3.zero;
        m_DoubleJump    = false;
    }

    private void InitWallJump() 
    {
      m_WallJumpVel = Vector3.zero;
      m_WallJump    = false;
    }
}
