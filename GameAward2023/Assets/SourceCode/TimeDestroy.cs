using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroy : MonoBehaviour
{
    public int m_Time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_Time--;
        if (m_Time < 0)
            Destroy(this.gameObject);
    }
}
