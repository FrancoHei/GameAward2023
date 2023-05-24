using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D    m_Rb2D;
    private PlayerState    m_PS;
    private PlayerParticle m_PP;
    private PlayerMovement m_PM;
    private PlayerAnimator m_PA;

    [Header("新しいコントロール方法")]
    public bool m_IsNewControl;

    private bool m_IsBPressed = false;
    public  bool IsBPressed 
    {
        set { m_IsBPressed = value; }
        get { return m_IsBPressed;  }
    }

    private bool m_IsXPressed = false;
    public bool IsXPressed
    {
        set { m_IsXPressed = value; }
        get { return m_IsXPressed; }
    }

    private int m_ThrowTimer;

    void Start()
    {
        m_Rb2D = GetComponent<Rigidbody2D>();
        m_PS   = GetComponent<PlayerState>();
        m_PP   = GetComponent<PlayerParticle>();
        m_PM   = GetComponent<PlayerMovement>();
        m_PA   = GetComponent<PlayerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (m_PS.ReadyThrow) 
        {
            m_ThrowTimer++;
            if (m_ThrowTimer % 20 == 0) 
            {
                GetComponents<CapsuleCollider2D>()[0].isTrigger = true;
                GetComponents<CapsuleCollider2D>()[1].isTrigger = true;
                GetComponents<CapsuleCollider2D>()[2].isTrigger = true;
                m_Rb2D.gravityScale = 0;
                m_PM.HandleThrowTarget();
            }
            return;
        }
    }

    public void OnMove(InputValue input)
    {
        if (!GameObject.Find("GameSystem").GetComponent<GameSystem>().CanInput) return;
        if (GameObject.Find("GameSystem").GetComponent<GameSystem>().GameOver) return;

        Vector2 value = input.Get<Vector2>();

        if (!m_PS.OnFloor)
        {
            m_PS.OnAirMoveMentInput = value;
            m_PS.MoveMentInput = Vector3.zero;
            return;
        }
        else
        {
            m_PS.MoveMentInput = value;
            m_PS.OnAirMoveMentInput = Vector3.zero;
        }
    }

    public void OnJump(InputValue input)
    {
        if (!GameObject.Find("GameSystem").GetComponent<GameSystem>().CanInput) return;

        if (m_IsNewControl && m_PS.Target) 
        {
            m_Rb2D.velocity = Vector2.zero;
            m_PA.SetAnimation("Throw");
            m_PS.ReadyThrow = true;
            m_ThrowTimer    = 0;
            return;
        }

        if (m_PS.OnFloor || m_PS.IsWire)
        {
                if (m_PS.IsSlide)
                {
                    m_PA.SetAnimation("JumpStart");
                    m_PM.HandleSlideJump();
                    return;
                }
                    m_PA.SetAnimation("JumpStart");
                    //ジャンプ処理
                    m_PM.HandleJump();
                return;
        }
        else
        {
                if (m_IsNewControl) 
                {
                    if ((m_PS.IsRightJump || m_PS.IsLeftJump || m_PS.IsJump))
                    {
                        m_PA.SetAnimation("JumpStart");
                        m_PS.IsRightJump = false;
                        m_PS.IsLeftJump  = false;
                        m_PM.HandleJump();
                        return;
                    }
                }
                ////二段ジャンプ
                //if (!m_PS.IsDoubleJump && m_PS.IsJump && !m_PS.IsLeftWallJump && !m_PS.IsRightWallJump)
                //{
                //    m_PM.HandleDoubleJump();
                //    return;
                //}

                ////二段ジャンプ
                //if (!m_PS.IsDoubleJump && m_PS.IsSlideJump && !m_PS.IsLeftWallJump && !m_PS.IsRightWallJump)
                //{
                //    m_PM.HandleDoubleJump();
                //    return;
                //}
        }
       
    }

    public void OnLeftWallJump(InputValue input)
    {
        if (!GameObject.Find("GameSystem").GetComponent<GameSystem>().CanInput) return;

        if (m_IsNewControl && m_PS.Target) return;

        if (!m_IsNewControl)
        {
            if (!m_PS.OnFloor)
            {
                if (m_PM.CheckCanWallJump() == 1)
                {
                    m_PA.SetAnimation("JumpStart");
                    m_PS.IsRightWallJump = false;
                    m_PS.IsLeftWallJump = true;
                    m_PM.HandleWallJump();
                }
            }
        }else 
        {
            if (m_IsXPressed)
            {
                if (!input.isPressed)
                {
                    if (m_PM.CheckCanWallJump() == 1)
                    {
                        if (m_PM.m_WallJumpControlMethod == PlayerMovement.WallJumpControlMethod.RELEASE)
                        {
                            m_PA.SetAnimation("JumpStart");

                            m_PS.IsLeftWallJump = true;
                            m_PS.IsRightWallJump = false;
                            m_PM.HandleWallJump();
                        }
                    }
                }
            }


            if (m_PS.OnFloor)
            {
                if (input.isPressed)
                {
                    m_PA.SetAnimation("JumpStart");
                    m_PM.HandlLeftJump();
                }
            }
            else 
            {
                if (input.isPressed)
                {
                    if (m_PS.IsRightJump || m_PS.IsLeftJump || m_PS.IsJump)
                    {
                        m_PA.SetAnimation("JumpStart");
                        m_PS.IsRightJump = false;
                        m_PM.HandlLeftJump();
                    }
                }
            }

            if ((m_PS.IsRightWallJump || m_PS .IsSlideJump) && m_PM.CheckCanWallJump() == 1)
            {
                if (m_PM.m_WallJumpControlMethod == PlayerMovement.WallJumpControlMethod.AUTO || m_PM.m_WallJumpControlMethod == PlayerMovement.WallJumpControlMethod.PRESSING)
                {
                    if (input.isPressed)
                    {
                        m_PA.SetAnimation("JumpStart");
                        m_PS.IsLeftWallJump = true;
                        m_PS.IsRightWallJump = false;
                        m_PM.HandleWallJump();
                    }
                }else
                if (m_PM.m_WallJumpControlMethod == PlayerMovement.WallJumpControlMethod.RELEASE)
                {
                    if (m_IsXPressed)
                    {
                        if (!input.isPressed)
                        {
                            m_PA.SetAnimation("JumpStart");
                            m_PS.IsLeftWallJump = true;
                            m_PS.IsRightWallJump = false;
                            m_PM.HandleWallJump();
                        }
                    }
                }
            }

            m_IsXPressed = input.isPressed;

        }
    }

    public void OnRightWallJump(InputValue input)
    {
        if (!GameObject.Find("GameSystem").GetComponent<GameSystem>().CanInput) return;

        if (m_IsNewControl && m_PS.Target) return;

        if (!m_IsNewControl) 
        {
            if (!m_PS.OnFloor)
            {
                if (m_PM.CheckCanWallJump() == -1)
                {
                    m_PA.SetAnimation("JumpStart");
                    m_PS.IsLeftWallJump = false;
                    m_PS.IsRightWallJump = true;
                    m_PM.HandleWallJump();
                }
            }
        }else
        {

            if (m_IsBPressed)
            {
                if (!input.isPressed)
                {
                    if (m_PM.CheckCanWallJump() == -1)
                    {
                        if (m_PM.m_WallJumpControlMethod == PlayerMovement.WallJumpControlMethod.RELEASE)
                        {
                            m_PA.SetAnimation("JumpStart");
                            m_PS.IsLeftWallJump = false;
                            m_PS.IsRightWallJump = true;
                            m_PM.HandleWallJump();
                        }
                    }
                }
            }
           
            if (m_PS.OnFloor) 
            {
                if (input.isPressed) 
                {
                    m_PA.SetAnimation("JumpStart");
                    m_PM.HandlRightJump();
                }
            }
            else 
            {

                if (input.isPressed)
                {
                    if (m_PS.IsRightJump || m_PS.IsLeftJump || m_PS.IsJump)
                    {
                        m_PA.SetAnimation("JumpStart");
                        m_PS.IsLeftJump   = false;
                        m_PM.HandlRightJump();
                    }
                }
            }


            if ((m_PS.IsLeftWallJump || m_PS.IsSlideJump) && m_PM.CheckCanWallJump() == -1)
            {
                if (m_PM.m_WallJumpControlMethod == PlayerMovement.WallJumpControlMethod.AUTO || m_PM.m_WallJumpControlMethod == PlayerMovement.WallJumpControlMethod.PRESSING)
                {
                    if (input.isPressed)
                    {
                        m_PA.SetAnimation("JumpStart");
                        m_PS.IsLeftWallJump = false;
                        m_PS.IsRightWallJump = true;
                        m_PM.HandleWallJump();
                    }
                }
                if (m_PM.m_WallJumpControlMethod == PlayerMovement.WallJumpControlMethod.RELEASE)
                {
                    if (m_IsBPressed)
                    {
                        if (!input.isPressed)
                        {
                            m_PA.SetAnimation("JumpStart");
                            m_PS.IsLeftWallJump = false;
                            m_PS.IsRightWallJump = true;
                            m_PM.HandleWallJump();
                        }
                    }
                }
            }

            m_IsBPressed = input.isPressed;

        }

    }




    public void OnSlide(InputValue input)
    {
        if (!GameObject.Find("GameSystem").GetComponent<GameSystem>().CanInput) return;

        //地面いないスライド出来ない
        if (!m_PS.OnFloor) return;
        if (m_IsNewControl && m_PS.Target)   return;
        //当たり判定変わる
        GetComponents<CapsuleCollider2D>()[0].enabled = false;
        GetComponents<CapsuleCollider2D>()[1].enabled = true;


        m_PS.IsSlide = true;
        m_PS.SlideVel = new Vector3(-transform.forward.z, 0.0f, 0.0f);
    }


}
