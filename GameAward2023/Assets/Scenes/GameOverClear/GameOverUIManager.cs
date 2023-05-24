using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour
{

    
    [Range(0,1), Tooltip("�t�F�[�h���쑬�x")] public float FadeSpeed = 0.1f;
    [Tooltip("�V�[�����ǂݍ��܂�Ă��� �t�F�[�h���n�܂�܂ł̃^�C�~���O")] public float FadeStartTime = 2.0f;

    [Space,Tooltip("fade�p(�ύX�s�v)")] public Image fadeImage;
    [Tooltip("fade�p(�ύX�s�v)")] public GameObject MainCanvas;

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
                    //��ʂ����S�ɍ����Ȃ����^�C�~���O��UI������
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
