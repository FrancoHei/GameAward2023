using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
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
        if (collision.gameObject == GameObject.Find("Player"))
        {
            if (!GameObject.Find("GameSystem").GetComponent<GameSystem>().GameClear)
            {
                if(collision.gameObject.GetComponent<PlayerState>().Target)
                    GameObject.Find("GameSystem").GetComponent<GameSystem>().GameClear = true;
            }
        }
    }

}
