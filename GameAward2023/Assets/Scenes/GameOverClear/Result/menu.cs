using UnityEngine;
using System.Collections;
using UnityEngine.UI; // UIコンポーネントの使用

public class menu : MonoBehaviour
{
    public Button retry;
    public Button stageselect;
    public Button title;

    void Start()
    {

        // 最初に選択状態にしたいボタンの設定
        retry.Select();
    }
}