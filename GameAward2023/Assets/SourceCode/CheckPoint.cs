using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool m_IsClear = false;

    public bool Clear
    {
        set { m_IsClear = value; }
        get { return m_IsClear;  }
    }

    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_IsClear = true;
        }
    }
}
