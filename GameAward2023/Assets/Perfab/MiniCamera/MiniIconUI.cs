using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniIconUI : MonoBehaviour
{
    private GameObject target;
    private GameObject player;


    [Tooltip("�ő�\������")]public float MaxDistance = 25.0f;

    [Tooltip("���̕\���ʒu����")]public float ArrowDistance = 25.0f;

    private GameObject Arrow;

    //����
    //���ړ��͈͂�-960 ���� 960 �X�N���[���T�C�Y�̔���
    //������0
    //�T�C�Y�̊֌W�� �ړ��͈͂�100�ʂ̃}�[�W��������Ă�������������



    [Tooltip("UI�\���ʒu�̒���(�\���ʒu)")] public float WidthAdjust = 0;
    [Tooltip("UI�\���ʒu�̒���(�\���ʒu)")] public float HeightAdjust = 0;

    [Tooltip("UI�\���ʒu�̒���(��ʊO�h�~)")] public float WidthClamp = 100;
    [Tooltip("UI�\���ʒu�̒���(��ʊO�h�~)")] public float HeightClamp = 100;


    [Tooltip("�v���C���[�ƃ^�[�Q�b�g�̋���������")] public float Range = 150.0f;

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
