using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameSystem").GetComponent<GameSystem>().GameOver ||
            GameObject.Find("GameSystem").GetComponent<GameSystem>().GameClear) 
        {
            gameObject.SetActive(false);
            return;
        }
            transform.position = GameObject.Find("MiniCamera_UI").transform.position;
        this.GetComponent<Image>().color = GameObject.Find("MiniCamera_UI").GetComponent<Image>().color;
    }
}
