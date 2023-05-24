using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FKD_StageSelect : MonoBehaviour
{


    [Tooltip("ステージ名のリスト ただしビルド設定で追加していないと動作しない")] public List<string> StageNameList;
    [Tooltip("ステージ画面から 戻るボタンを押したときに移行するシーン名 ビルド設定で追加していないと動作しない")] public string BackSceneName;
    [Space]
    [Tooltip("ここに入れた数字を最大値としてクランプしている。 ステージの進行度等を入れるといい")] public int MaxSelect = 6;
    [Tooltip("選択されていないオブジェクトの色の変化度合い")] public float ColorChangeValue = 0.2f;
    [Tooltip("選択されたオブジェクトの大きさの変化度合い")] public float SizeChangeValue = 1.1f;
    [Space]
    [SerializeField, Tooltip("リピート入力の速度")] private float InputRepeatSpeed = 0.125f;
    [SerializeField, Tooltip("リピートが始まるまでのフレーム")] private float InputRepeatStartTime = 0.4f;

    [Space(40)]
    [Tooltip("一応自動割り当てされるから触らなくていい(デバッグ用)")] public List<GameObject> StageSelectUI;
    [Tooltip("現在選択しているUIの番号(デバッグ用)")] public int select = 0;

    private float Xsize = 500.0f;
    private float Ysize = 300.0f;

    struct input
    {
        public bool Up;
        public bool Down;
        public bool Left;
        public bool Right;

        public bool Enter;
        public bool Back;

    }


    private float RepeatTimer_Left;
    private float RepeatTimer_Right;

    private bool Repeat_Left;
    private bool Repeat_Right;

    private input TriggerInput;
    private input OldInput;
    private input NowInput;

    private Gamepad PAD;
    private bool ConnectPAD;

    private int m_Timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            int t = (i + 1);
            string text = "StageUI_" + t;
            StageSelectUI.Add(transform.Find(text).gameObject);
        }


        ConnectPAD = false;
        if (Gamepad.current != null)
        {
            ConnectPAD = true;
            PAD = Gamepad.current;
        }

        m_Timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer++;

        if (m_Timer < 10) return;


        if (Gamepad.current == null)
        {
            ConnectPAD = false;
        }

        if (!ConnectPAD)
        {
            if (Gamepad.current != null)
            {
                ConnectPAD = true;
                PAD = Gamepad.current;
            }
        }



        {
            NowInput.Up = false;
            NowInput.Down = false;
            NowInput.Enter = false;
            NowInput.Back = false;
            NowInput.Left = false;
            NowInput.Right = false;

            if (ConnectPAD && PAD.dpad.up.isPressed || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                NowInput.Up = true;
            }
            if (ConnectPAD && PAD.dpad.down.isPressed || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                NowInput.Down = true;
            }


            if (ConnectPAD && PAD.dpad.right.isPressed || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                NowInput.Right = true;
            }
            if (ConnectPAD && PAD.dpad.left.isPressed || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                NowInput.Left = true;
            }


            if (ConnectPAD && PAD.crossButton.isPressed || Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space))
            {
                NowInput.Enter = true;
            }
            if (ConnectPAD && PAD.circleButton.isPressed || Input.GetKey(KeyCode.Escape))
            {
                NowInput.Back = true;
            }



            if (OldInput.Right && NowInput.Right)
            {
                RepeatTimer_Right += Time.deltaTime;
            }
            if (OldInput.Left && NowInput.Left)
            {
                RepeatTimer_Left += Time.deltaTime;
            }

            if (!NowInput.Right)
            {
                RepeatTimer_Right = 0;
                Repeat_Right = false;
            }
            if (!NowInput.Left)
            {
                RepeatTimer_Left = 0;
                Repeat_Left = false;
            }


            if (RepeatTimer_Right > InputRepeatStartTime)
            {
                Repeat_Right = true;
            }
            if (RepeatTimer_Left > InputRepeatStartTime)
            {
                Repeat_Left = true;
            }

            if ((!OldInput.Up && NowInput.Up))
            {
                TriggerInput.Up = true;
            }
            if ((!OldInput.Down && NowInput.Down))
            {
                TriggerInput.Down = true;
            }

            if ((!OldInput.Right && NowInput.Right) || (Repeat_Right && RepeatTimer_Right > InputRepeatSpeed))
            {
                RepeatTimer_Right = 0;
                TriggerInput.Right = true;
            }
            if ((!OldInput.Left && NowInput.Left) || (Repeat_Left && RepeatTimer_Left > InputRepeatSpeed))
            {
                RepeatTimer_Left = 0;
                TriggerInput.Left = true;
            }





            if (!OldInput.Enter && NowInput.Enter)
            {
                TriggerInput.Enter = true;
            }
            if (!OldInput.Back && NowInput.Back)
            {
                TriggerInput.Back = true;
            }
        }

        if (TriggerInput.Right)
        {
            select += 1;
        }

        if (TriggerInput.Left)
        {
            select -= 1;
        }

        if (TriggerInput.Down)
        {
            select += 3;
        }

        if (TriggerInput.Up)
        {
            select -= 3;
        }



        if (select > MaxSelect - 1)
        {
            select = MaxSelect - 1;
        }

        if (select < 0)
        {
            select = 0;
        }

        for (int i = 0; i < 6; i++)
        {
            GameObject item = StageSelectUI[i];

            if (i == select)
            {
                if (item.GetComponent<Text>())
                    item.GetComponent<Text>().color = Color.HSVToRGB(0, 0, 1.0f);

                if (item.GetComponent<TextMeshProUGUI>())
                    item.GetComponent<TextMeshProUGUI>().color = Color.HSVToRGB(0, 0, 1.0f);

                if (item.GetComponent<Image>())
                    item.GetComponent<Image>().color = Color.HSVToRGB(0, 0, 1.0f);

                RectTransform rectTransform = item.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(Xsize * SizeChangeValue, Ysize * SizeChangeValue);


                if (TriggerInput.Enter)
                {
                    SceneManager.LoadScene(StageNameList[i]);
                }
                else if (TriggerInput.Back)
                {
                    SceneManager.LoadScene(BackSceneName);
                }


            }
            else
            {
                RectTransform rectTransform = item.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(Xsize, Ysize);

                if (item.GetComponent<Text>())
                    item.GetComponent<Text>().color = Color.HSVToRGB(0, 0, 0.4f);

                if (item.GetComponent<TextMeshProUGUI>())
                    item.GetComponent<TextMeshProUGUI>().color = Color.HSVToRGB(0, 0, ColorChangeValue);

                if (item.GetComponent<Image>())
                    item.GetComponent<Image>().color = Color.HSVToRGB(0, 0, ColorChangeValue);
            }
        }






        OldInput = NowInput;
        TriggerInput.Up = false;
        TriggerInput.Down = false;
        TriggerInput.Right = false;
        TriggerInput.Left = false;
        TriggerInput.Enter = false;
        TriggerInput.Back = false;
    }
}
