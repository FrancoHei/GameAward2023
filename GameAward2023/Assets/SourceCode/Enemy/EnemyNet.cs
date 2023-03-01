using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNet : MonoBehaviour
{
    public float m_AttackSpeed;
    public float m_MaxRotateAngle;
    private bool m_ClockWise = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 pos = transform.parent.position;//+ new Vector3(1,0,0);

        if (m_ClockWise)
        {
            transform.RotateAround(pos, -transform.parent.forward, m_AttackSpeed * Time.fixedDeltaTime);
            if (transform.localRotation.eulerAngles.z > m_MaxRotateAngle)
            {
                m_ClockWise = false;
            }
        }else 
        {
            transform.RotateAround(pos, transform.parent.forward, m_AttackSpeed * Time.fixedDeltaTime);
            if (transform.localRotation.eulerAngles.z < 10)
            {
                m_ClockWise = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.Find("Player"))
        {     
            GameObject gs = GameObject.Find("GameSystem");

            if (gs && gameObject.transform.parent.GetComponent<EnemyState>().State == EnemyState.EnemyAiState.DISCOVER)
                gs.GetComponent<GameSystem>().GameOver = true;
        }
    }

}
