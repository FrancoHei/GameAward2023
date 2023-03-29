using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody2D m_Rb2D;
    //ƒWƒƒƒ“ƒv“–‚½‚éLAYER
    public LayerMask m_OnFloorHitLayer;
    // Start is called before the first frame update
    void Start()
    {
        m_Rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckOnFloor();   
    }

    private void CheckOnFloor()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.2f, m_OnFloorHitLayer);

        if (hit && (hit.transform.gameObject.tag == "Ground" || hit.transform.gameObject.tag == "Wall"))
        {
            //’n–Ê‚É‚¢‚é
            m_Rb2D.velocity = Vector3.zero;
        }
        else
        {
        }

    }
}
