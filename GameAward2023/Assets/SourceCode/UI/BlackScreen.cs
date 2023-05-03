using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject gs = GameObject.Find("GameSystem");

        if (!gs) return;

        if (gs.GetComponent<GameSystem>().GameOver || gs.GetComponent<GameSystem>().GameClear)
        {
            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 1.0f);
        }
        else
        {
            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 0.0f);
        }
    }
}
