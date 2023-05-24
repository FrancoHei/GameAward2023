using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour
{

    
    [Range(0,1), Tooltip("フェード動作速度")] public float FadeSpeed = 0.1f;
    [Tooltip("シーンが読み込まれてから フェードが始まるまでのタイミング")] public float FadeStartTime = 2.0f;

    [Space,Tooltip("fade用(変更不要)")] public Image fadeImage;
    [Tooltip("fade用(変更不要)")] public GameObject MainCanvas;

    bool FadeToBlack = true;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        Color color = fadeImage.color;
        color.a = 0;
        fadeImage.color = color;
        FadeToBlack = true;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (timer >= FadeStartTime)
        {
            if (FadeToBlack)
            {
                Color color = fadeImage.color;
                color.a += FadeSpeed * 0.1f;
                fadeImage.color = color;

                if (color.a > 1.0f)
                {
                    //画面が完全に黒くなったタイミングでUIを消す
                    FadeToBlack = false;
                    MainCanvas.SetActive(true);
                }
            }
            if (!FadeToBlack)
            {
                Color color = fadeImage.color;
                if (color.a > 0.0f)
                {
                    color.a -= FadeSpeed * 0.1f;
                    fadeImage.color = color;
                }               
            }

        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
