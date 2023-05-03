using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniIconUI : MonoBehaviour
{
    private GameObject target;
    private GameObject player;


    [Tooltip("最大表示距離")]public float MaxDistance = 25.0f;

    [Tooltip("矢印の表示位置調整")]public float ArrowDistance = 25.0f;

    private GameObject Arrow;

    //メモ
    //横移動範囲は-960 から 960 スクリーンサイズの半分
    //中央が0
    //サイズの関係上 移動範囲は100位のマージンを取っておいた方がいい



    [Tooltip("UI表示位置の調整(表示位置)")] public float WidthAdjust = 0;
    [Tooltip("UI表示位置の調整(表示位置)")] public float HeightAdjust = 0;

    [Tooltip("UI表示位置の調整(画面外防止)")] public float WidthClamp = 100;
    [Tooltip("UI表示位置の調整(画面外防止)")] public float HeightClamp = 100;


    [Tooltip("プレイヤーとターゲットの距離感割合")] public float Range = 150.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Arrow = transform.Find("Arrow").gameObject;
    }


    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            float setposX = target.transform.position.x;
            setposX -= player.transform.position.x;
            setposX *= Range;
            setposX += WidthAdjust;

            //float setposY = player.transform.position.y;
            float setposY = target.transform.position.y;
            setposY -= player.transform.position.y;
            setposY *= Range;
            setposY += HeightAdjust;


            if (Mathf.Abs(setposX) > (Screen.width / 2) - (WidthClamp * 1.1f) ||
                Mathf.Abs(setposY) > (Screen.height / 2) + (HeightClamp * 0.5f) )
            {
                this.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                Arrow.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                this.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                Arrow.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }


            float distance = Vector3.Distance(target.transform.position, player.transform.position);

            if (distance > MaxDistance)
            {
                target = null;
                this.GetComponent<Image>().sprite = null;
                this.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                Arrow.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }

            setposX = Mathf.Clamp(setposX, -Screen.width / 2 + WidthClamp, Screen.width / 2 - WidthClamp);
            setposY = Mathf.Clamp(setposY, -Screen.height / 2 + HeightClamp, Screen.height / 2 - HeightClamp);

            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

            Vector3 iconpos = new Vector3(setposX, setposY, 0);
            rectTransform.localPosition = iconpos;


            Vector3 arrowDir = Vector3.Normalize(target.transform.position - player.transform.position);
            arrowDir.z = 0;

            rectTransform = Arrow.GetComponent<RectTransform>();


            Vector3 arrowPos = (arrowDir * ArrowDistance);
            rectTransform.localPosition = arrowPos;

            float direction = Mathf.Atan2(arrowDir.y, arrowDir.x) * Mathf.Rad2Deg;
            Arrow.transform.eulerAngles = new Vector3(0, 0, direction + 90);

        }
        else
        {
            Arrow.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            this.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            this.GetComponent<Image>().sprite = null;
        }
    }
    public void SetTarget(GameObject tgt)
    {
        target = tgt;
        this.GetComponent<Image>().sprite = tgt.GetComponent<SpriteRenderer>().sprite;
    }

    public void HideCamera()
    {
        target = null;
        this.GetComponent<Image>().sprite = null;
        this.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        Arrow.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }
}
