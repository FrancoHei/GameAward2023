using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCameraUI : MonoBehaviour
{
    private GameObject target;
    private GameObject player;


    //メモ
    //移動範囲は0 から 画面サイズのマイナス(-1920)
    //右上が0
    //サイズの関係上 移動範囲は125位のマージンを取っておいた方がいい

    private float Direction;

    public float Height = -120;

    [Tooltip("プレイヤーとターゲットの距離感割合")]public float Range = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            float setposX = target.transform.position.x;
            setposX -= player.transform.position.x;
            setposX *= Range;
            //setposX -= Screen.width;


            //float setposY = player.transform.position.y;
            float setposY = Height;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(setposX, setposY, 0);

        }
    }


    public void SetTargetObject(GameObject tgt)
    {
        target = tgt;
    }
}
