using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameClearText : MonoBehaviour
{
    public float m_TextSize;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject gs = GameObject.Find("GameSystem");

        if (!gs) return;
        if (gs.GetComponent<GameSystem>().GameOver) return;


        if (!gs.GetComponent<GameSystem>().GameClear)
        {
            if (GetComponent<TextMeshProUGUI>())
                GetComponent<TextMeshProUGUI>().fontSize = 0;
        }
        else
        {
            if (GetComponent<TextMeshProUGUI>())
                GetComponent<TextMeshProUGUI>().fontSize = m_TextSize;
        }
    }

}
