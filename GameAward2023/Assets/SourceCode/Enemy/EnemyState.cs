using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public enum EnemyAiState
    {
        NORMAL,
        VIGILANCE,
        DISCOVER,
        RETURNTOSTARTPOINT
    }

    private EnemyAiState m_State = EnemyAiState.NORMAL;

    public EnemyAiState State
    {
        set { m_State = value; }
        get { return m_State; }
    }

}
