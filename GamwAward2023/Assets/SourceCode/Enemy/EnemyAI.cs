using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D rb2D;

    public Vector3 m_Point_1;
    public Vector3 m_Point_2;

    private Vector3 m_Vel;
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
        m_Vel          = new Vector3(1, 0, 0);
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, m_Vel);
        transform.rotation = toRotation;
        rb2D.MovePosition(transform.position + m_Vel * Time.fixedDeltaTime);

    }
}
