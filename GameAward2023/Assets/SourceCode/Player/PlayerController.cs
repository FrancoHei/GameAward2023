using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_Rb2D;
    private PlayerState m_PS;
    void Start()
    {
        m_Rb2D = GetComponent<Rigidbody2D>();
        m_PS   = GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        //ターゲット前持つ
        if (m_PS.Target)
            m_PS.Target.transform.position = transform.position + new Vector3(transform.forward.z * 0.5f, transform.up.y * 0.5f, 0.0f);
        //var cursorPosition = Mouse.current.position.ReadValue();

    }

    void FixedUpdate()
    {
        //地面チェック
        CheckOnFloor();

        //地面時速度計算
        if (!m_PS.m_ChangeDirectionJump && !m_PS.IsWallJump) 
        {
            //空中方向変えない
            if(m_PS.OnFloor)
                m_Rb2D.velocity = new Vector2(m_PS.MoveMentInput.x * m_PS.m_OnFloorSpeed, 
                                              m_Rb2D.velocity.y);
        }else 
        {
            //空中方向変える
            m_Rb2D.velocity = new Vector2(m_PS.MoveMentInput.x      * m_PS.m_OnFloorSpeed + 
                                          m_PS.OnAirMoveMentInput.x * m_PS.m_OnAirSpeed, 
                                          m_Rb2D.velocity.y);
        }
       
        
        if (m_PS.IsWallJump && !m_PS.m_ChangeDirectionWallJump) 
        {
            //壁ジャンプ空中方向変えない
            //壁ジャンプ速度計算
            if(m_PS.OnAirMoveMentInput != Vector3.zero && CheckCanWallJumpWalk() == 0) 
            {
                m_PS.WallJumpVel = Vector3.zero;
                m_Rb2D.velocity  = new Vector2(m_PS.OnAirMoveMentInput.x * m_PS.m_WallJumpSpeed, m_Rb2D.velocity.y);
            }else 
            {
                m_Rb2D.velocity = new Vector2(m_PS.WallJumpVel.x * m_PS.m_WallJumpSpeed, m_Rb2D.velocity.y);
            }
        }

        //スライド速度計算
        if (m_PS.IsSlide)
            m_Rb2D.velocity = new Vector2(m_PS.SlideVel.x * m_PS.m_SlideSpeed, m_Rb2D.velocity.y);


        //二段ジャンプ速度計算
        if (m_PS.IsDoubleJump)
            m_Rb2D.velocity = new Vector2(m_PS.DoubleJumpVel.x * m_PS.m_DoubleJumpSpeed, m_Rb2D.velocity.y);

        //スライド
        SlideTimer();
        //回転
        SetRotate();
    }


    public void OnMove(InputValue input)
    {
        Vector2 value = input.Get<Vector2>();

        if (!m_PS.OnFloor)
        {
            m_PS.OnAirMoveMentInput = value;
            m_PS.MoveMentInput      = Vector3.zero;
        }
        else 
        {
            m_PS.MoveMentInput      = value;
            m_PS.OnAirMoveMentInput = Vector3.zero;
        }
    }

    public void OnJump(InputValue input)
    {
        if (m_PS.OnFloor)
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
                HandleWallJump();
                m_PS.IsWallJump = true;
                return;
            }

            //二段ジャンプ
            if (!m_PS.IsDoubleJump && CheckCanDoubleJump())
            {
                HandleDoubleJump();
                m_PS.IsDoubleJump = true;
                return;
            }
        }
    }

    public void OnSlide(InputValue input)
    {
        //地面いないスライド出来ない
        if (!m_PS.OnFloor) return;

        //当たり判定変わる
        GetComponents<CapsuleCollider2D>()[0].enabled = false;
        GetComponents<CapsuleCollider2D>()[1].enabled = true;


        m_PS.IsSlide    = true;
        m_PS.SlideVel   = new Vector3(transform.forward.z, 0.0f, 0.0f);
    }

    private void HandleJump()
    {
        m_Rb2D.AddForce(Vector3.up * m_PS.m_JumpPower);
        m_PS.FirstJumpVel = new Vector3(m_Rb2D.velocity.x, 0.0f, 0.0f);
    }

    private void HandleWallJump()
    {
        //二段ジャンプいてるなら、初期化
        if (m_PS.IsDoubleJump)
        {
            InitDoubleJump();
        }

        m_Rb2D.velocity = Vector2.zero;


        if (!m_PS.m_ChangeDirectionWallJump) 
        {
            //壁ジャンプ空中方向変えない
            m_Rb2D.AddForce(Vector3.up * m_PS.m_WallJumpPower);
            m_PS.WallJumpVel = new Vector3(CheckCanWallJump(),0.0f,0.0f);
            m_Rb2D.velocity = new Vector2(m_PS.WallJumpVel.x * m_PS.m_WallJumpSpeed, m_Rb2D.velocity.y);
        }
        else 
        {
            //壁ジャンプ空中方向変える
            //角度計算、壁方向壁ジャンプダメ
            Vector2 wallDirection = new Vector2(CheckCanWallJump(), 0.0f);

            float angle = Vector2.Dot(wallDirection, m_PS.OnAirMoveMentInput);

            if(angle <= 0.0) 
            {
                m_PS.OnAirMoveMentInput = new Vector2(0.0f, 0.0f);
            }

            //下方向壁ジャンプダメ
            if (m_PS.OnAirMoveMentInput.y < 0 && angle <= 0) 
            {
                m_PS.OnAirMoveMentInput = new Vector3(m_PS.OnAirMoveMentInput.x, 0, 0);
            }

            if (m_PS.OnAirMoveMentInput.y < 0 && angle > 0)
            {
                m_PS.OnAirMoveMentInput = new Vector3(m_PS.OnAirMoveMentInput.x, 0.1f, 0);
            }
            //
            m_Rb2D.AddForce(m_PS.OnAirMoveMentInput * m_PS.m_WallJumpPower);
            m_PS.WallJumpVel = new Vector3(m_PS.OnAirMoveMentInput.x,0.0f,0.0f);
        }
    }

    private void HandleDoubleJump()
    {
        m_PS.DoubleJumpVel = new Vector3(m_PS.FirstJumpVel.x, 0.0f, 0.0f);

        m_Rb2D.velocity    = Vector2.zero;
        
        m_Rb2D.AddForce(Vector3.up * m_PS.m_DoubleJumpPower);
    }

    private void CheckOnFloor()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, m_PS.m_OnFloorDistance, m_PS.m_OnFloorHitLayer);

        if (hit && (hit.transform.gameObject.tag == "Ground" || hit.transform.gameObject.tag == "Wall"))
        {
            //地面到着瞬間
            if (!m_PS.OnFloor)
            {
                //壁ジャンプ初期化
                if (m_PS.IsWallJump)
                {
                    InitWallJump();
                }

                if (m_PS.IsDoubleJump)
                {
                    InitDoubleJump();
                }
            }
            //地面にいる
            m_PS.OnFloor = true;
        }
        else
        {
            //空中
            m_PS.OnFloor = false;
        }

    }

    private int CheckCanWallJump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, m_PS.m_WallJumpDistance, m_PS.m_WallJumpHitLayer);

        if (hit && hit.transform.gameObject.tag == "Wall")
        {
            return -1;
        }

        hit = Physics2D.Raycast(transform.position, -Vector2.right, m_PS.m_WallJumpDistance, m_PS.m_WallJumpHitLayer);

        if (hit && hit.transform.gameObject.tag == "Wall")
        {
            return 1;
        }

        return 0;
    }

    private int CheckCanWallJumpWalk()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, m_PS.m_WallJumpWalkDistance, m_PS.m_WallJumpHitLayer);

        if (hit && hit.transform.gameObject.tag == "Wall")
        {
            return -1;
        }

        hit = Physics2D.Raycast(transform.position, -Vector2.right, m_PS.m_WallJumpWalkDistance, m_PS.m_WallJumpHitLayer);

        if (hit && hit.transform.gameObject.tag == "Wall")
        {
            return 1;
        }

        return 0;
    }
    private bool CheckCanWallJumpSnowMotion()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(transform.forward.z, 0.0f), m_PS.m_WallJumpDistance, m_PS.m_WallJumpHitLayer);

        if (hit && hit.transform.gameObject.tag == "Wall")
        {
            return true;
        }

        return false;
    }

    private bool CheckCanDoubleJump()
    {
        if (m_Rb2D.velocity.y > 0) return false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, m_PS.m_DoubleJumpDistance, m_PS.m_DoubleJumpHitLayer);

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
        if (m_PS.IsSlide)
        {
            m_PS.SlideTimer++;
            if (m_PS.SlideTimer > m_PS.m_MaxSlideTimer)
            {
                m_PS.SlideVel        = Vector3.zero;
                m_PS.SlideTimer      = 0;
                m_PS.IsSlide         = false;

                GetComponents<CapsuleCollider2D>()[0].enabled = true;
                GetComponents<CapsuleCollider2D>()[1].enabled = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            m_PS.Target = collision.gameObject;
            m_PS.Target.GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }

    private void HandleDelayUpdate() 
    {
        //if (!m_OnFloor) 
        //{
        //    if(CheckCanWallJump() != 0 && m_Rb2D.velocity.y <= m_WallJumpSnowMotionRange.y && m_Rb2D.velocity.y >= m_WallJumpSnowMotionRange.x) 
        //    {
        //        if (CheckCanWallJumpSnowMotion()) 
        //        {
        //            Time.timeScale = m_WallJumpSnowMotion;
        //            return;
        //        }
        //    }
        //}

        //if (!m_OnFloor && m_Target && CheckCanDoubleJump() && !m_DoubleJump)
        //{
        //    Time.timeScale = 0.1f;
        //    return;
        //}

        //Time.timeScale = 1.0f;
    }

    private void InitDoubleJump() 
    {
        m_PS.DoubleJumpVel   = Vector3.zero;
        m_PS.IsDoubleJump    = false;
    }

    private void InitWallJump() 
    {
        m_PS.WallJumpVel = Vector3.zero;
        m_PS.IsWallJump    = false;
    }
}
