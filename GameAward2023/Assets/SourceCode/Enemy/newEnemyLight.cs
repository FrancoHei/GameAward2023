using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newEnemyLight : MonoBehaviour
{
    [SerializeField] private EnemyState MyState;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.Find("Player"))
        {
            if (MyState.State == EnemyState.EnemyAiState.VIGILANCE)
            {
                MyState.State = EnemyState.EnemyAiState.DISCOVER;
            }
        }
    }
}
