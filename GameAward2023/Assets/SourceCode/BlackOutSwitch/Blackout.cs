using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Blackout : MonoBehaviour
{
    [Header("’â“dŽžŠÔ")]
    public int m_StopSecond;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameSystem").GetComponent<GameSystem>().BlackOut)
        {
            GetComponent<Light2D>().color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
        }
        else
        if (!GameObject.Find("GameSystem").GetComponent<GameSystem>().BlackOut)
        {
            GetComponent<Light2D>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
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
