using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private GameObject m_Player;
    public  GameObject m_Target;
    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_Player.transform.position + (m_Player.transform.up) * 1.4f;
    }
}
