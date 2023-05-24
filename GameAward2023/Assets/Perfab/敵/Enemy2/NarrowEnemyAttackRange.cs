using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrowEnemyAttackRange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.Find("Player"))
        {
            if (gameObject.transform.parent.GetComponent<State_Enemy>().State == State_Enemy.EnemyAiState.CHASE)
            {
                //gameObject.transform.parent.GetComponent<State_Enemy>().State = State_Enemy.EnemyAiState.CHASE;
                gameObject.transform.parent.GetComponent<State_Enemy>().State = State_Enemy.EnemyAiState.ATTACK;
            }
        }
    }
}
