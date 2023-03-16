using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D m_Rb2D;
    private PlayerState m_PS;
    private PlayerParticle m_PP;
    private PlayerController m_PC;

    public enum WallJumpControlMethod 
    {
        AUTO,
        PRESSING,
        RELEASE
    }

    public enum TARGETTHROWMETHOD
    {
        LEFTSTICK,
        MOVEMENT
    }

    public WallJumpControlMethod m_WallJumpControlMethod = WallJumpControlMethod.AUTO;
    public TARGETTHROWMETHOD     m_TargetThrowMethod     = TARGETTHROWMETHOD.LEFTSTICK;

    public bool m_CanLeftStickAffectWallJump;


    // Start is called before the first frame update
    void Start()
    {
        m_Rb2D = GetComponent<Rigidbody2D>();
        m_PS   = GetComponent<PlayerState>();
        m_PP   = GetComponent<PlayerParticle>();
        m_PC   = GetComponent<PlayerController>();
    }


    private void Update()
    {
        //ターゲット前持つ
        if (m_PS.Target)
            m_PS.Target.transform.position = transform.position + new Vector3(transform.forward.z * 0.5f, transform.up.y * 0.5f, 0.0f);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //地面チェック
        CheckOnFloor();

        //地面時速度計算
        if (!m_PS.m_ChangeDirectionJump && !m_PS.IsLeftWallJump && !m_PS.IsRightWallJump && !m_PS.IsSlideJump && !m_PS.IsRightJump && !m_PS.IsLeftJump)
        {
            //空中方向変えない
            if (m_PS.OnFloor)
                m_Rb2D.velocity = new Vector2(m_PS.MoveMentInput.x * m_PS.m_OnFloorSpeed,
                                              m_Rb2D.velocity.y);
        }
        else
        if (m_PS.m_ChangeDirectionJump && !m_PS.IsLeftWallJump && !m_PS.IsRightWallJump && !m_PS.IsSlideJump && !m_PS.IsRightJump && !m_PS.IsLeftJump)
        {
            //空中方向変える
            m_Rb2D.velocity = new Vector2(m_PS.MoveMentInput.x * m_PS.m_OnFloorSpeed +
                                          m_PS.OnAirMoveMentInput.x * m_PS.m_OnAirSpeed,
                                          m_Rb2D.velocity.y);
        }

        if (m_PC.m_IsNewControl)
        {
            if (m_PS.IsRightJump)
            {
                if (m_PS.OnAirMoveMentInput != Vector3.zero)
                {
                    if(Vector3.Dot(m_PS.OnAirMoveMentInput, m_PS.RightJumpVel) < 0)
                        m_PS.RightJumpVel += new Vector3(m_PS.OnAirMoveMentInput.x * m_PS.m_OnJumpAirSpeed, 0.0f, 0.0f);
                }
                m_Rb2D.velocity = new Vector2(m_PS.RightJumpVel.x * m_PS.m_JumpSpeed, m_Rb2D.velocity.y);
            }

            if (m_PS.IsLeftJump)
            {
                if (m_PS.OnAirMoveMentInput != Vector3.zero)
                {
                    if (Vector3.Dot(m_PS.OnAirMoveMentInput, m_PS.LeftJumpVel) < 0)
                        m_PS.LeftJumpVel += new Vector3(m_PS.OnAirMoveMentInput.x * m_PS.m_OnJumpAirSpeed, 0.0f, 0.0f) ;
                }
                m_Rb2D.velocity = new Vector2(m_PS.LeftJumpVel.x * m_PS.m_JumpSpeed, m_Rb2D.velocity.y);
            }


            if (!m_PS.Target) 
            {
                CheckRightWallJump();
                CheckLeftWallJump();
            }
        }

        //二段ジャンプ速度計算
        if (!m_PS.m_ChangeDirectionJump && m_PS.IsDoubleJump && !m_PC.m_IsNewControl)
        {
            m_Rb2D.velocity = new Vector2(m_PS.DoubleJumpVel.x * m_PS.m_DoubleJumpSpeed, m_Rb2D.velocity.y);
        }
        else
        if (m_PS.m_ChangeDirectionJump && m_PS.IsDoubleJump && !m_PC.m_IsNewControl)
        {
            if (m_PS.OnAirMoveMentInput != Vector3.zero)
            {
                m_PS.DoubleJumpVel = new Vector3(Mathf.Sign(m_PS.OnAirMoveMentInput.x) * Mathf.Abs(m_PS.DoubleJumpVel.x), 0.0f, 0.0f); ;
                m_Rb2D.velocity = new Vector2(m_PS.DoubleJumpVel.x * m_PS.m_DoubleJumpSpeed, m_Rb2D.velocity.y);
            }
            else
            {
                m_Rb2D.velocity = new Vector2(m_PS.DoubleJumpVel.x * m_PS.m_DoubleJumpSpeed, m_Rb2D.velocity.y);
            }
        }



        if ((m_PS.IsLeftWallJump || m_PS.IsRightWallJump) && !m_PS.m_ChangeDirectionWallJump)
        {
            //壁ジャンプ空中方向変えない
            //壁ジャンプ速度計算

            if (m_PS.OnAirMoveMentInput != Vector3.zero && CheckCanWallJumpWalk())
            {
                if (m_Rb2D.velocity.y < 0)
                    m_PS.WallJumpVel = m_PS.OnAirMoveMentInput;
            }      
            m_Rb2D.velocity = new Vector2(m_PS.WallJumpVel.x * m_PS.m_WallJumpSpeed, m_Rb2D.velocity.y);

            //???bebug
            if(m_Rb2D.velocity.y > 42) 
            {
                m_Rb2D.velocity = new Vector2(m_Rb2D.velocity.x,24);
            }
        }

        //スライド速度計算
        if (m_PS.IsSlide)
            m_Rb2D.velocity = new Vector2(m_PS.SlideVel.x * m_PS.m_SlideSpeed, m_Rb2D.velocity.y);

        if (m_PS.IsSlideJump)
        {
            if (m_PS.OnAirMoveMentInput != Vector3.zero)
            {
                m_PS.SlideJumpVel = new Vector3(Mathf.Sign(m_PS.OnAirMoveMentInput.x) * Mathf.Abs(m_PS.SlideJumpVel.x), 0.0f, 0.0f);
                m_Rb2D.velocity   = new Vector2(m_PS.SlideJumpVel.x * m_PS.m_SlideJumpSpeed, m_Rb2D.velocity.y);
            }
            else
            {
                m_Rb2D.velocity = new Vector2(m_PS.SlideJumpVel.x * m_PS.m_SlideJumpSpeed, m_Rb2D.velocity.y);
            }
        }


        if(m_PS.IsWire && m_PS.Wire && !m_PS.IsJump) 
        {
            if(transform.forward.z > 0)
            {
                m_Rb2D.velocity = (m_PS.Wire.transform.parent.GetComponent<LineRenderer>().GetPosition(1) - m_PS.Wire.transform.parent.GetComponent<LineRenderer>().GetPosition(0)).normalized * m_PS.m_WireSpeed;
            }else 
            {
                m_Rb2D.velocity = (m_PS.Wire.transform.parent.GetComponent<LineRenderer>().GetPosition(0) - m_PS.Wire.transform.parent.GetComponent<LineRenderer>().GetPosition(1)).normalized * m_PS.m_WireSpeed;
            }
        }

   

        //スライド
        SlideTimer();
        //回転
        SetRotate();
    }

    public void HandleJump()
    {
        Instantiate(m_PP.m_JumpVFX, transform.position, Quaternion.identity);

        if (m_PS.IsWire)
        {
            m_Rb2D.AddForce(Vector3.up * m_PS.m_JumpPower * 1.2f);
        }
        else
        {
            if (m_PC.m_IsNewControl) 
            {
                m_Rb2D.velocity = Vector2.zero;
                m_Rb2D.AddForce(Vector3.up * m_PS.m_JumpPower);
            }
            else 
            {
                m_PS.FirstJumpVel = new Vector3(m_Rb2D.velocity.x, 0.0f, 0.0f);
                m_Rb2D.AddForce(Vector3.up * m_PS.m_JumpPower);
            }
        }


        Physics2D.gravity = new Vector2(0, -9.81f);

        m_PS.IsJump = true;
    }

    public void HandleUpwardJump() 
    {
        if (m_PS.IsWire)
        {
            m_Rb2D.AddForce(Vector3.up * m_PS.m_JumpPower * 1.2f);
        }
        else
        {
            m_Rb2D.AddForce(Vector3.up * m_PS.m_JumpPower);
        }


        Physics2D.gravity = new Vector2(0, -9.81f);


        Instantiate(m_PP.m_JumpVFX, transform.position, Quaternion.identity);

        m_PS.IsJump = true;
    }

    public void HandlRightJump()
    {
        m_Rb2D.velocity = Vector2.zero;

        m_Rb2D.AddForce(Vector3.up * m_PS.m_JumpPower);

        Physics2D.gravity = new Vector2(0, -9.81f);


        Instantiate(m_PP.m_JumpVFX, transform.position, Quaternion.identity);

        m_PS.IsRightJump = true;

        m_PS.RightJumpVel = new Vector3(Vector2.right.x ,0.0f, 0.0f);

    }

    public void HandlLeftJump()
    {
        m_Rb2D.velocity = Vector2.zero;

        m_Rb2D.AddForce(Vector3.up * m_PS.m_JumpPower);

        Physics2D.gravity = new Vector2(0, -9.81f);


        Instantiate(m_PP.m_JumpVFX, transform.position, Quaternion.identity);

        m_PS.IsLeftJump = true;

        m_PS.LeftJumpVel = new Vector3(Vector2.left.x, 0.0f, 0.0f);
    }

    public void HandleWallJump()
    {
        Instantiate(m_PP.m_WallJumpVFX, transform.position, Quaternion.identity);

        //二段ジャンプいてるなら、初期化
        bool left  = m_PS.IsLeftWallJump;
        bool right = m_PS.IsRightWallJump;
        InitJump();
        m_PS.IsLeftWallJump  = left;
        m_PS.IsRightWallJump = right;
        m_Rb2D.velocity      = Vector2.zero;

        if(m_CanLeftStickAffectWallJump && m_PC.m_IsNewControl) 
        {
            Debug.Log(m_PS.OnAirMoveMentInput);
            if (m_PS.IsLeftWallJump)
            {
                m_PS.WallJumpVel = new Vector3(1, 0.0f, 0.0f);
            }

            if (m_PS.IsRightWallJump)
            {
                m_PS.WallJumpVel = new Vector3(-1.0f, 0.0f, 0.0f);
            }
            
            if(m_PS.OnAirMoveMentInput != Vector3.zero)
            {
                float height = m_PS.OnAirMoveMentInput.y;
                if(m_PS.OnAirMoveMentInput.y < 0) 
                {
                    height = 0;
                }
                m_PS.WallJumpVel = new Vector3(m_PS.WallJumpVel.x * (Mathf.Abs(m_PS.OnAirMoveMentInput.x) + 1) * 0.6f, 0.0f, 0.0f);
                m_Rb2D.AddForce(Vector3.up * (Mathf.Abs(height) + 1) * (m_PS.m_WallJumpPower) * 0.6f);
                return;
            }

            m_Rb2D.AddForce(Vector3.up * m_PS.m_WallJumpPower);
            m_Rb2D.velocity = new Vector2(m_PS.WallJumpVel.x * m_PS.m_WallJumpSpeed, 1.0f);
            return;
        }


        if (!m_PS.m_ChangeDirectionWallJump)
        {
            //壁ジャンプ空中方向変えない
            m_Rb2D.AddForce(Vector3.up * m_PS.m_WallJumpPower);
            if (m_PS.IsLeftWallJump)
            {
                m_PS.WallJumpVel = new Vector3(1, 0.0f, 0.0f);
            }

            if (m_PS.IsRightWallJump)
            {
                m_PS.WallJumpVel = new Vector3(-1.0f, 0.0f, 0.0f);
            }

            m_Rb2D.velocity = new Vector2(m_PS.WallJumpVel.x * m_PS.m_WallJumpSpeed, 1.0f);
        }
        else
        {
            //壁ジャンプ空中方向変える
            //角度計算、壁方向壁ジャンプダメ
            Vector2 wallDirection = Vector2.zero;
            if (m_PS.IsLeftWallJump)
            {
                wallDirection = new Vector2(1.0f, 0.0f);
            }

            if (m_PS.IsRightWallJump)
            {
                wallDirection = new Vector2(-1.0f, 0.0f);
            }

            float angle = Vector2.Dot(wallDirection, m_PS.OnAirMoveMentInput);

            if (angle <= 0.0)
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
            m_PS.WallJumpVel = new Vector3(m_PS.OnAirMoveMentInput.x, 0.0f, 0.0f);
        }
    }

    public void HandleDoubleJump()
    {
        Instantiate(m_PP.m_DoubleJumpVFX, transform.position, Quaternion.identity);

        InitJump();

        m_PS.IsDoubleJump = true;

        m_PS.DoubleJumpVel = new Vector3(m_Rb2D.velocity.x,0.0f,0.0f);
        m_Rb2D.velocity    = Vector3.zero;

        m_Rb2D.AddForce(Vector3.up * m_PS.m_DoubleJumpPower);
    }

    public void HandleSlideJump()
    {
        if (!CheckCanSlideJump()) return;
        Instantiate(m_PP.m_SlideJumpVFX, transform.position, Quaternion.identity);

        m_Rb2D.AddForce(Vector3.up * m_PS.m_SlideJumpPower);

        m_PS.SlideJumpVel = new Vector3(m_Rb2D.velocity.x + (m_PS.SlideTimer * Mathf.Sign(m_Rb2D.velocity.x)), 0.0f, 0.0f);
        m_PS.FirstJumpVel = new Vector3(m_Rb2D.velocity.x * m_PS.m_SlideJumpDoubleJumpOffset, 0.0f, 0.0f);
        m_PS.IsSlideJump = true;

        InitSlide();
    }

    public void HandleThrowTarget()
    {
        if (m_TargetThrowMethod == TARGETTHROWMETHOD.LEFTSTICK)
        {
            Vector2 direction = new Vector2(m_PS.MoveMentInput.x, Vector2.up.y * (Mathf.Abs(m_PS.MoveMentInput.y ) + 0.5f));
            m_PS.Target.GetComponent<CircleCollider2D>().isTrigger = false;
            m_PS.Target.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            m_PS.Target.GetComponent<Rigidbody2D>().AddForce(direction * m_PS.m_TargetThrowPower);
            m_PS.Target = null;
        }else
        if (m_TargetThrowMethod == TARGETTHROWMETHOD.MOVEMENT)
        {
            Vector2 direction = Vector2.zero;
            if (m_Rb2D.velocity == Vector2.zero)
            {
                direction = new Vector2(0.0f, Vector2.up.y);
            }
            else 
            {
                direction = new Vector2(Mathf.Sign(m_Rb2D.velocity.x), Vector2.up.y);
            }
            m_PS.Target.GetComponent<CircleCollider2D>().isTrigger = false;
            m_PS.Target.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            m_PS.Target.GetComponent<Rigidbody2D>().AddForce(direction * m_PS.m_TargetThrowPower);
            m_PS.Target = null;
        }
    }
    private void CheckRightWallJump() 
    {
        if (m_PS.IsRightJump) 
        {
            if(CheckCanWallJump() == -1) 
            {
                if(m_WallJumpControlMethod == WallJumpControlMethod.AUTO)
                {
                    m_PS.IsLeftWallJump = false;
                    m_PS.IsRightWallJump = true;
                    HandleWallJump();
                }else
                if (m_WallJumpControlMethod == WallJumpControlMethod.PRESSING)
                {
                    if (m_PC.IsBPressed) 
                    {
                        m_PS.IsLeftWallJump = false;
                        m_PS.IsRightWallJump = true;
                        HandleWallJump();
                    }
                }

            }
        }
    }

    private void CheckLeftWallJump()
    {
        if (m_PS.IsLeftJump)
        {
            if (CheckCanWallJump() == 1)
            {
                if (m_WallJumpControlMethod == WallJumpControlMethod.AUTO)
                {
                    m_PS.IsLeftWallJump  = true;
                    m_PS.IsRightWallJump = false;
                    HandleWallJump();
                }
                else
                if (m_WallJumpControlMethod == WallJumpControlMethod.PRESSING)
                {
                    if (m_PC.IsXPressed)
                    {
                        m_PS.IsLeftWallJump  = true;
                        m_PS.IsRightWallJump = false;
                        HandleWallJump();
                    }
                }
               

            }
        }
    }

    public void InitJump()
    {
        m_PS.IsJump          = false;
        m_PS.DoubleJumpVel   = Vector3.zero;
        m_PS.IsDoubleJump    = false;
        m_PS.WallJumpVel     = Vector3.zero;
        m_PS.IsLeftWallJump  = false;
        m_PS.IsRightWallJump = false;
        m_PS.SlideJumpVel    = Vector3.zero;
        m_PS.FirstJumpVel    = Vector3.zero;
        m_PS.RightJumpVel    = Vector3.zero;
        m_PS.IsRightJump     = false;
        m_PS.LeftJumpVel     = Vector3.zero;
        m_PS.IsLeftJump      = false;
        m_PS.IsSlideJump     = false;
        //m_PS.OnAirMoveMentInput = Vector3.zero;
        //m_PS.MoveMentInput      = Vector3.zero;
    }

    private void InitSlide()
    {
        m_PS.IsSlide = false;
        GetComponents<CapsuleCollider2D>()[0].enabled = true;
        GetComponents<CapsuleCollider2D>()[1].enabled = false;
        m_PS.SlideVel = Vector3.zero;
        m_PS.SlideTimer = 0;

    }

    private void CheckOnFloor()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, m_PS.m_OnFloorDistance, m_PS.m_OnFloorHitLayer);

        if (hit && (hit.transform.gameObject.tag == "Ground" || hit.transform.gameObject.tag == "Wall"))
        {
            //地面到着瞬間
            if (!m_PS.OnFloor)
            {
                m_PS.MoveMentInput = m_PS.OnAirMoveMentInput;
                m_Rb2D.velocity = new Vector2(m_PS.MoveMentInput.x * m_PS.m_OnFloorSpeed +
                                              m_PS.OnAirMoveMentInput.x * m_PS.m_OnAirSpeed,
                                              m_Rb2D.velocity.y);
                //ジャンプ初期化
                InitJump();
            }
            //地面にいる
            m_PS.OnFloor = true;
        }
        else
        {
            if (m_PS.IsSlide)
            {
                InitSlide();
            }
            //空中
            m_PS.OnFloor = false;
        }

    }

    public int CheckCanWallJump()
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

    private bool CheckCanWallJumpWalk()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.right, m_PS.m_WallJumpWalkDistance, m_PS.m_WallJumpHitLayer);

        if (hit && hit.transform.gameObject.tag == "Wall")
        {
            return false;
        }

        hit = Physics2D.Raycast(transform.position, Vector2.right, m_PS.m_WallJumpWalkDistance, m_PS.m_WallJumpHitLayer);

        if (hit && hit.transform.gameObject.tag == "Wall")
        {
            return false;
        }

        return true;
    }


    private bool CheckCanSlideJump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(transform.forward.z * 0.5f,0.0f,0.0f) + Vector3.up, Vector2.up, 1.5f, m_PS.m_OnFloorHitLayer);

      

        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + Vector3.up, Vector2.up * 0.5f, 1.5f, m_PS.m_OnFloorHitLayer);

        if ((hit && hit.transform.gameObject.tag == "Ground")|| (hit2 && hit2.transform.gameObject.tag == "Ground"))
        {
            return false;
        }

        return true;
    }

    private bool CheckSlide()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(transform.forward.z, 0.0f, 0.0f), 1.0f, m_PS.m_WallJumpHitLayer);

        if (hit && hit.transform.gameObject.tag == "Wall")
        {
            return true;
        }

        return false;
    }

    private void SetRotate()
    {
        if (m_Rb2D.velocity.x > 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward);
            transform.rotation = toRotation;
        }

        if (m_Rb2D.velocity.x < 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(-Vector3.forward);
            transform.rotation = toRotation;
        }
    }

    private void SlideTimer()
    {
        if (CheckSlide())
        {
            InitSlide();
            return;
        }

        if (m_PS.IsSlide)
        {
            m_PS.SlideTimer++;
            if (m_PS.SlideTimer > m_PS.m_MaxSlideTimer)
            {
                InitSlide();
                return;
            }
        }
    }
}
