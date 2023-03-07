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

    void Start()
    {
        m_Rb2D = GetComponent<Rigidbody2D>();
        m_PS   = GetComponent<PlayerState>();
        m_PP   = GetComponent<PlayerParticle>();
        m_PM   = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnMove(InputValue input)
    {
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
        if (m_PS.OnFloor)
        {
            if (m_PS.IsSlide)
            {
                m_PM.HandleSlideJump();
                return;
            }

            //ジャンプ処理
            HandleJump();
            return;
        }
        else
        {
            //二段ジャンプ
            if (!m_PS.IsDoubleJump && m_PS.IsJump && !m_PS.IsLeftWallJump && !m_PS.IsRightWallJump)
            {
                m_PM.HandleDoubleJump();
                return;
            }

            //二段ジャンプ
            if (!m_PS.IsDoubleJump && m_PS.IsSlideJump && !m_PS.IsLeftWallJump && !m_PS.IsRightWallJump)
            {
                m_PM.HandleDoubleJump();
                return;
            }
        }
    }

    public void OnLeftWallJump(InputValue input)
    {
        if (!m_PS.OnFloor)
        {
            if (m_PM.CheckCanWallJump() == 1)
            {
                m_PS.IsRightWallJump = false;
                m_PS.IsLeftWallJump  = true;
                m_PM.HandleWallJump();
            }
        }
    }

    public void OnRightWallJump(InputValue input)
    {
        if (!m_PS.OnFloor)
        {
            if (m_PM.CheckCanWallJump() == -1)
            {
                m_PS.IsLeftWallJump  = false;
                m_PS.IsRightWallJump = true;
                m_PM.HandleWallJump();
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


        m_PS.IsSlide = true;
        m_PS.SlideVel = new Vector3(transform.forward.z, 0.0f, 0.0f);
    }

    private void HandleJump()
    {
        Instantiate(m_PP.m_JumpVFX, transform.position, Quaternion.identity);

        m_Rb2D.AddForce(Vector3.up * m_PS.m_JumpPower);

        m_PS.IsJump = true;
        m_PS.FirstJumpVel = new Vector3(m_Rb2D.velocity.x, 0.0f, 0.0f);
    }

}
