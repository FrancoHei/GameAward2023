using UnityEngine;
using System.Collections;
using UnityEngine.UI; // UIコンポーネントの使用

public class menu : MonoBehaviour
{
    Button retry;
    Button stageselect;
    Button title;

    void Start()
    {
        // ボタンコンポーネントの取得
        retry = GameObject.Find("/Canvas/Button1").GetComponent<Button>();
        stageselect = GameObject.Find("/Canvas/Button2").GetComponent<Button>();
        title = GameObject.Find("/Canvas/Button3").GetComponent<Button>();

        // 最初に選択状態にしたいボタンの設定
        retry.Select();
    }
}