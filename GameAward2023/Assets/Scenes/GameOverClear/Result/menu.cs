using UnityEngine;
using System.Collections;
using UnityEngine.UI; // UI�R���|�[�l���g�̎g�p

public class menu : MonoBehaviour
{
    Button retry;
    Button stageselect;
    Button title;

    void Start()
    {
        // �{�^���R���|�[�l���g�̎擾
        retry = GameObject.Find("/Canvas/Button1").GetComponent<Button>();
        stageselect = GameObject.Find("/Canvas/Button2").GetComponent<Button>();
        title = GameObject.Find("/Canvas/Button3").GetComponent<Button>();

        // �ŏ��ɑI����Ԃɂ������{�^���̐ݒ�
        retry.Select();
    }
}