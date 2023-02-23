using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameSystem")) 
        {
            if (GameObject.Find("GameSystem").GetComponent<GameSystem>().BlackOut) 
            {
                GetComponent<Light2D>().intensity = 0;
            }else 
            {
                GetComponent<Light2D>().intensity = 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") 
        {
            GameObject gs = GameObject.Find("GameSystem");
            if (gs)
            {
                if (!gs.GetComponent<GameSystem>().BlackOut)
                {
                    gs.GetComponent<GameSystem>().GameOver = true;  
                }
            }
        }
    }
}
