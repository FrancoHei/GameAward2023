using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCameraUI : MonoBehaviour
{
    private GameObject target;
    private GameObject player;


    //����
    //�ړ��͈͂�0 ���� ��ʃT�C�Y�̃}�C�i�X(-1920)
    //�E�オ0
    //�T�C�Y�̊֌W�� �ړ��͈͂�125�ʂ̃}�[�W��������Ă�������������

    private float Direction;

    public float Height = -120;

    [Tooltip("�v���C���[�ƃ^�[�Q�b�g�̋���������")]public float Range = 100.0f;

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
