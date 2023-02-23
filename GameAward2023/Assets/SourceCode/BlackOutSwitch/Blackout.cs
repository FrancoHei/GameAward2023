using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackout : MonoBehaviour
{

    public int m_StopSecond;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == GameObject.Find("Player")) 
        {
            if (!GameObject.Find("GameSystem").GetComponent<GameSystem>().BlackOut) 
            {
                GameObject.Find("GameSystem").GetComponent<GameSystem>().BlackOut = true;
                GameObject.Find("TimerText").GetComponent<TimerText>().StartTimer = true;
                GameObject.Find("TimerText").GetComponent<TimerText>().Second     = m_StopSecond;

            }
        }
    }
}
