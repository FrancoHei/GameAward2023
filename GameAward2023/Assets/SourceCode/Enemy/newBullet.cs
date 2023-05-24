using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newBullet : MonoBehaviour
{
    private Rigidbody2D m_Rb2D;

    private float m_Speed = 1;

    public float Speed
    {
        set { m_Speed = value; }
        get { return m_Speed; }
    }


    //追加要素
    private float m_Distance = 10;
    private Vector3 StartPos;
    public float Distance
    {
        set { m_Distance = value; }
        get { return m_Distance; }
    }
    // Update is called once per frame
    void Update()
    {
        //if (Vector3.Distance(StartPos, transform.position) > m_Distance)
        //{
        //    //弾の最大距離による破壊(エフェクト入れるならココ)

        //    Destroy(this.gameObject);
        //}
    }

    //追加要素終わり

    // Start is called before the first frame update
    void Start()
    {
        m_Rb2D = GetComponent<Rigidbody2D>();
        StartPos = transform.position;
    }


    private void FixedUpdate()
    {
        //float rot = Mathf.Deg2Rad * transform.rotation.eulerAngles.z;
        //m_Rb2D.velocity = new Vector2(
        //    Mathf.Cos(rot) * m_Speed,
        //    Mathf.Sin(rot) * m_Speed);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.Find("Player"))
        {
            if (!GameObject.Find("GameSystem").GetComponent<GameSystem>().GameOver)
            {
                GameObject.Find("GameSystem").GetComponent<GameSystem>().GameOver = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        float rot = Mathf.Deg2Rad * transform.rotation.eulerAngles.z;
        Vector3 pos = transform.position;
        Gizmos.color = Color.blue;
        Vector3 Direction = pos + Vector3.right * Mathf.Cos(rot) + Vector3.up * Mathf.Sin(rot);
        Gizmos.DrawLine(pos, Direction);
    }
}
