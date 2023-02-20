using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    //プレイヤー動くスピード
    public float m_Speed;

    private Vector3 m_MoveMentInput;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

        //rb2D.velocity = m_MoveMentInput * m_Speed;
        //rb2D.AddForce(m_MoveMentInput * m_Speed);
        //rb2D.MovePosition(transform.position + m_MoveMentInput * m_Speed * Time.fixedDeltaTime);
        transform.Translate(m_MoveMentInput);
    }


    public void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();

        m_MoveMentInput = new Vector3(inputVec.x, 0, 0);
    }
}
