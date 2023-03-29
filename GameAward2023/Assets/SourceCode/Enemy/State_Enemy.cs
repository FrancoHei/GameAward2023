using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Enemy : MonoBehaviour
{
    public enum EnemyAiState
    {
        WAIT,           //行動を一旦停止
        MOVE,           //移動
        ATTACK,         // 攻撃
        CHASE,         //追跡
    }
    private EnemyAiState state = EnemyAiState.WAIT;     // 現在のステート

    public EnemyAiState State
    {
        set { state = value; }
        get { return state; }
    }
}
