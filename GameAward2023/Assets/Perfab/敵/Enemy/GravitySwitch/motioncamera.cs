using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motioncamera : MonoBehaviour
{
    private Rigidbody2D rb2D;

    public float dis = 10;
    GameObject Player;

    private GameObject player;
    private List<State_Enemy> enemies = new List<State_Enemy>();

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        // Playerオブジェクトを検索
        player = GameObject.FindGameObjectWithTag("Player");

        // Enemyオブジェクトを検索
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyObject in enemyObjects)
        {
            State_Enemy enemy = enemyObject.GetComponent<State_Enemy>();
            if (enemy != null)
            {
                enemies.Add(enemy);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // dis = Vector3.Distance(this.transform.position, Player.transform.position);
        //Debug.Log(state_enemy1.State);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        foreach (State_Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, player.transform.position);

            // 距離が閾値未満ならば何らかの処理を行う
            if (distance < dis)
            {
                Debug.Log("PlayerとEnemyの距離が" + distance + "です。");
                if (enemy.State == State_Enemy.EnemyAiState.CHASE)
                {
                    return;
                }
                if (collision.gameObject == player)
                {
                    enemy.State = State_Enemy.EnemyAiState.WARNING;
                }
            }
        }
    }

}
