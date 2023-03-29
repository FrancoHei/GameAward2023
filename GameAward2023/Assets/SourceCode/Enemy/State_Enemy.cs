using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Enemy : MonoBehaviour
{
    public enum EnemyAiState
    {
        WAIT,           //�s������U��~
        MOVE,           //�ړ�
        ATTACK,         // �U��
        CHASE,         //�ǐ�
    }
    private EnemyAiState state = EnemyAiState.WAIT;     // ���݂̃X�e�[�g

    public EnemyAiState State
    {
        set { state = value; }
        get { return state; }
    }
}
