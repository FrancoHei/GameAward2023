using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionCheck : MonoBehaviour
{
    private PlayerState m_PS;
    private Rigidbody2D m_Rb2D;
    private PlayerMovement m_PM;
    // Start is called before the first frame update
    void Start()
    {
        m_PS   = GetComponent<PlayerState>();
        m_Rb2D = GetComponent<Rigidbody2D>();
        m_PM   = GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            if (!collision.gameObject.GetComponent<Target>().CanCatch) return;
            m_PS.Target = collision.gameObject;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            m_PS.Target.GetComponent<BoxCollider2D>().isTrigger = true;
            m_PS.Target.GetComponent<Target>().StartThrow = false;

            GetComponents<CapsuleCollider2D>()[0].enabled = false;
            GetComponents<CapsuleCollider2D>()[1].enabled = false;
            GetComponents<CapsuleCollider2D>()[2].enabled = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if (collision.gameObject.tag == "Wire")
        {
            m_PM.InitJump();
            Physics2D.gravity = new Vector2(0, 0);
            m_PS.Wire   = collision.gameObject;
            m_PS.IsWire = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Wire")
        {
            m_PS.Wire           = null;
            m_PS.IsWire         = false;
            Physics2D.gravity   = new Vector2(0, -9.81f);
        }

  
    }
}
