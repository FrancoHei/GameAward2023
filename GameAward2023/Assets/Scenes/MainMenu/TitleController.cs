using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class TitleController : MonoBehaviour
{

    [System.Serializable]
    public struct TitleHierarchy
    {
        [Tooltip("リスト名(メモ用)")] public string ListName;
        [Tooltip("登録オブジェクト")] public List<GameObject> ObjectList;
        [Tooltip("選択時にカーソルが重なる登録オブジェクト\n")] public List<GameObject> SelectableObjectList;
        [Tooltip("各階層ごとにカーソルを保存する\nデバッグ用に選択数値がわかるようにしてるだけ")] public int SelectedObject;
    }

    [SerializeField, Tooltip("オブジェクトを階層構造ごとに\n表示非表示するためのリスト")] private List<TitleHierarchy> m_TitleHierarchy;
    [SerializeField, Tooltip("現在の階層位置")] private int m_SelectHierarchy;

    [SerializeField, Tooltip("リピート入力の速度")] private float InputRepeatSpeed;
    [SerializeField, Tooltip("リピートが始まるまでのフレーム")] private float InputRepeatStartTime;

    struct input
    {
        public bool Up;
        public bool Down;
        public bool Left;
        public bool Right;

        public bool Enter;
        public bool Back;

    }


    private float RepeatTimer_Up;
    private float RepeatTimer_Down;
    private float RepeatTimer_Left;
    private float RepeatTimer_Right;

    private bool Repeat_Up;
    private bool Repeat_Down;
    private bool Repeat_Left;
    private bool Repeat_Right;

    private input TriggerInput;
    private input OldInput;
    private input NowInput;

    private Gamepad PAD;
    private bool ConnectPAD;


    // Start is called before the first frame update
    void Start()
    {
        m_SelectHierarchy = 0;

        for (int i = 0; i < m_TitleHierarchy.Count; i++)
        {
            foreach (var item in m_TitleHierarchy[i].SelectableObjectList)
            {
                item.SetActive(false);
            }
            foreach (var item in m_TitleHierarchy[i].ObjectList)
            {
                item.SetActive(false);
            }
            if (i == m_SelectHierarchy)
            {
                foreach (var item in m_TitleHierarchy[i].SelectableObjectList)
                {
                    item.SetActive(true);
                }
                foreach (var item in m_TitleHierarchy[i].ObjectList)
                {
                    item.SetActive(true);
                }
            }

        }



        ConnectPAD = false;
        if (Gamepad.current != null)
        {
            ConnectPAD = true;
            PAD = Gamepad.current;
        }
    }

    // Update is called once per frame
    void Update()
    {

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

            if (ConnectPAD && PAD.dpad.up.isPressed || Input.GetKey(KeyCode.UpArrow) || (ConnectPAD && PAD.leftStick.y.ReadValue() > 0.0f))
            {
                NowInput.Up = true;
            }
            if (ConnectPAD && PAD.dpad.down.isPressed || Input.GetKey(KeyCode.DownArrow) || (ConnectPAD && PAD.leftStick.y.ReadValue() < 0.0f))
            {
                NowInput.Down = true;
            }


            if (ConnectPAD && PAD.dpad.right.isPressed || Input.GetKey(KeyCode.RightArrow))
            {
                NowInput.Right = true;
            }
            if (ConnectPAD && PAD.dpad.left.isPressed || Input.GetKey(KeyCode.LeftArrow))
            {
                NowInput.Left = true;
            }


            if (ConnectPAD && PAD.crossButton.isPressed || Input.GetKey(KeyCode.Return))
            {
                NowInput.Enter = true;
            }
            if (ConnectPAD && PAD.circleButton.isPressed || Input.GetKey(KeyCode.Escape))
            {
                NowInput.Back = true;
            }



            if (OldInput.Up && NowInput.Up)
            {
                RepeatTimer_Up += Time.deltaTime;
            }
            if (OldInput.Down && NowInput.Down)
            {
                RepeatTimer_Down += Time.deltaTime;
            }
            if (OldInput.Right && NowInput.Right)
            {
                RepeatTimer_Right += Time.deltaTime;
            }
            if (OldInput.Left && NowInput.Left)
            {
                RepeatTimer_Left += Time.deltaTime;
            }

            if (!NowInput.Up)
            {
                RepeatTimer_Up = 0;
                Repeat_Up = false;
            }
            if (!NowInput.Down)
            {
                RepeatTimer_Down = 0;
                Repeat_Down = false;
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


            if (RepeatTimer_Up > InputRepeatStartTime)
            {
                Repeat_Up = true;
            }
            if (RepeatTimer_Down > InputRepeatStartTime)
            {
                Repeat_Down = true;
            }
            if (RepeatTimer_Right > InputRepeatStartTime)
            {
                Repeat_Right = true;
            }
            if (RepeatTimer_Left > InputRepeatStartTime)
            {
                Repeat_Left = true;
            }



            if ((!OldInput.Up && NowInput.Up) || (Repeat_Up && RepeatTimer_Up > InputRepeatSpeed))
            {
                RepeatTimer_Up = 0;
                TriggerInput.Up = true;
            }
            if ((!OldInput.Down && NowInput.Down) || (Repeat_Down && RepeatTimer_Down > InputRepeatSpeed))
            {
                RepeatTimer_Down = 0;
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




        int i = 0;
        foreach (var item in m_TitleHierarchy[m_SelectHierarchy].SelectableObjectList)
        {
            item.SetActive(true);

            if (m_TitleHierarchy[m_SelectHierarchy].SelectedObject == i)
            {
                if (item.GetComponent<Text>())
                {
                    item.GetComponent<Text>().color = Color.HSVToRGB(0, 0, 1.0f);
                }
                if (item.GetComponent<TextMeshProUGUI>())
                {
                    item.GetComponent<TextMeshProUGUI>().color = Color.HSVToRGB(0, 0, 1.0f);
                }
                if (item.GetComponent<Image>())
                {
                    item.GetComponent<Image>().color = Color.HSVToRGB(0, 0, 1.0f);
                }
            }
            else
            {
                if (item.GetComponent<Text>())
                {
                    item.GetComponent<Text>().color = Color.HSVToRGB(0, 0, 0.4f);
                }
                if (item.GetComponent<TextMeshProUGUI>())
                {
                    item.GetComponent<TextMeshProUGUI>().color = Color.HSVToRGB(0, 0, 0.4f);
                }
                if (item.GetComponent<Image>())
                {
                    item.GetComponent<Image>().color = Color.HSVToRGB(0, 0, 0.4f);
                }
            }
            i++;
        }



        if (TriggerInput.Up)
        {
            TitleHierarchy buffer = m_TitleHierarchy[m_SelectHierarchy];
            buffer.SelectedObject--;
            if (buffer.SelectableObjectList.Count - 1 < buffer.SelectedObject)
            {
                buffer.SelectedObject = 0;
            }
            if (0 > buffer.SelectedObject)
            {
                buffer.SelectedObject = buffer.SelectableObjectList.Count - 1;
            }

            m_TitleHierarchy[m_SelectHierarchy] = buffer;
        }

        if (TriggerInput.Down)
        {
            TitleHierarchy buffer = m_TitleHierarchy[m_SelectHierarchy];
            buffer.SelectedObject++;

            if (buffer.SelectableObjectList.Count - 1 < buffer.SelectedObject)
            {
                buffer.SelectedObject = 0;
            }
            if (0 > buffer.SelectedObject)
            {
                buffer.SelectedObject = buffer.SelectableObjectList.Count - 1;
            }

            m_TitleHierarchy[m_SelectHierarchy] = buffer;
        }


        if (TriggerInput.Enter)
        {

            if (m_SelectHierarchy < m_TitleHierarchy.Count - 1)
            {
                foreach (var item in m_TitleHierarchy[m_SelectHierarchy].SelectableObjectList)
                {
                    item.SetActive(false);
                }
                foreach (var item in m_TitleHierarchy[m_SelectHierarchy].ObjectList)
                {
                    item.SetActive(false);
                }
                m_SelectHierarchy++;
                foreach (var item in m_TitleHierarchy[m_SelectHierarchy].SelectableObjectList)
                {
                    item.SetActive(true);
                }
                foreach (var item in m_TitleHierarchy[m_SelectHierarchy].ObjectList)
                {
                    item.SetActive(true);
                }
                //シーン遷移等のコードはここに書く

                // m_TitleHierarchy[m_SelectHierarchy].SelectableObjectList で選択しているオブジェクトに特定のイベントを送る
                // という形式が楽そうではある

            }
            else
            {

                int t = 0;
                foreach (var item in m_TitleHierarchy[m_SelectHierarchy].SelectableObjectList)
                {
                    if (m_TitleHierarchy[m_SelectHierarchy].SelectedObject == t)
                    {
                        if (item.GetComponent<TitleEvent>())
                        {
                            item.GetComponent<TitleEvent>().TriggerInput();
                        }
                        if (item.GetComponent<Quit>()) 
                        {
                            item.GetComponent<Quit>().AppQuit();
                        }
                        break;
                    }
                    t++;
                }
            }
        }

        if (TriggerInput.Back)
        {
            if (m_SelectHierarchy > 0)
            {
                foreach (var item in m_TitleHierarchy[m_SelectHierarchy].SelectableObjectList)
                {
                    item.SetActive(false);
                }
                foreach (var item in m_TitleHierarchy[m_SelectHierarchy].ObjectList)
                {
                    item.SetActive(false);
                }
                m_SelectHierarchy--;
                foreach (var item in m_TitleHierarchy[m_SelectHierarchy].SelectableObjectList)
                {
                    item.SetActive(true);
                }
                foreach (var item in m_TitleHierarchy[m_SelectHierarchy].ObjectList)
                {
                    item.SetActive(true);
                }
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

    public void SetSelectObject(int Hierarchy, int SelectObjectNumber)
    {

        TitleHierarchy buffer = m_TitleHierarchy[Hierarchy];
        buffer.SelectedObject = SelectObjectNumber;

        if (buffer.SelectableObjectList.Count - 1 < buffer.SelectedObject)
        {
            buffer.SelectedObject = 0;
        }
        if (0 > buffer.SelectedObject)
        {
            buffer.SelectedObject = buffer.SelectableObjectList.Count - 1;
        }

        m_TitleHierarchy[Hierarchy] = buffer;
    }








}
