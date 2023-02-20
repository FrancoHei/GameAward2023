using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_Rb2D;
    //プレイヤー動くスピード
    public float        m_Speed;
    //ジャンプパワー(ジャンプ高さ)
    public float        m_JumpPower;

    private Vector3     m_MoveMentInput;
    //地面にいるか
    private bool        m_OnFloor;
    //当たるLayer
    public LayerMask    m_HitLayer;

    void Start()
    {
        m_Rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        CheckOnFloor();

        m_Rb2D.velocity = new Vector2(m_MoveMentInput.x * m_Speed, m_Rb2D.velocity.y);
        //rb2D.AddForce(m_MoveMentInput * m_Speed);
        //m_Rb2D.MovePosition(transform.position + m_MoveMentInput * m_Speed * Time.fixedDeltaTime);
    }


    public void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();

        if (m_OnFloor) 
        {
            m_MoveMentInput = new Vector3(inputVec.x, 0, 0);
        }else 
        {
            m_MoveMentInput = new Vector3(0, 0, 0);
        }
    }

    public void OnJump(InputValue input)
    {
        if (m_OnFloor) 
        {
            HandleJump();
        }else 
        {
            HandleSecondJump();
        }
    }

    private void HandleJump()
    {
        m_Rb2D.velocity = new Vector2(0,1) * m_JumpPower;
    }

    private void HandleSecondJump() 
    {
    
    }

    private void CheckOnFloor() 
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1.0f, m_HitLayer);
        if (hit && hit.transform.gameObject.tag == "Ground")
        {
            m_OnFloor = true;
        }else 
        {
            m_OnFloor = false;
        }

    }
}
