using UnityEngine;
using System.Collections;
using UnityEngine.UI; // UI�R���|�[�l���g�̎g�p

public class menu : MonoBehaviour
{
    public Button retry;
    public Button stageselect;
    public Button title;

    void Start()
    {

        // �ŏ��ɑI����Ԃɂ������{�^���̐ݒ�
        retry.Select();
    }
}