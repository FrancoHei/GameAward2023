using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class NarrowEnemy : MonoBehaviour
{
    GameObject Player;
    [SerializeField] GameObject Light;
    [Header("移動経路")] public GameObject[] movePoint;
    [Header("MOVE状態のspeed")] public float speed = 1.0f;
    [Header("CHACE状態のspeed")] public float chaceSpeed = 4; // スピード

    private Color lightColor;
    [Header("警戒色")]
    public float colorValueR;
    public float colorValueG;
    public float colorValueB;
    //bool rightTleftF;//右左判定
    int xVector;//左右どっちに進むかの数字

    private float dis;//プレイヤーとエネミーの距離
    private Rigidbody2D rb2D;


    private int nowPoint = 0;
    private bool returnPoint = false;


    void Start()
    {
        GetComponent<State_Enemy>().State = State_Enemy.EnemyAiState.MOVE;
        Player = GameObject.Find("Player");
        lightColor = Light.GetComponent<Light2D>().color;
    }
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        if (movePoint != null && movePoint.Length > 0 && rb2D != null)
        {
            rb2D.position = movePoint[0].transform.position;
        }
    }

    private void FixedUpdate()
    {
        dis = Vector3.Distance(this.transform.position, Player.transform.position);

        // 現在のステートのUpdateを呼び出す
        switch (GetComponent<State_Enemy>().State)
        {
            case State_Enemy.EnemyAiState.WAIT:
                Wait();
                break;
            case State_Enemy.EnemyAiState.MOVE:
                Move();
                break;
            case State_Enemy.EnemyAiState.ATTACK:
                Attack();
                break;
            case State_Enemy.EnemyAiState.CHASE:
                Chase();
                break;
        }
        /////////////////////////////////

    }
    void Wait()
    {

    }
    void Move()
    {
        
        Light.GetComponent<Light2D>().color = lightColor;
        lightColor = new Color(0.5f, 0.5f, 0.5f);
        if (movePoint != null && movePoint.Length > 1 && rb2D != null)
        {
            //通常進行
            if (!returnPoint)
            {

                int nextPoint = nowPoint + 1;


                //目標ポイントとの誤差がわずかになるまで移動
                if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
                {
                    //現在地から次のポイントへのベクトルを作成
                    Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);

                    //次のポイントへ移動
                    rb2D.MovePosition(toVector);
                }
                //次のポイントを１つ進める
                else
                {
                    rb2D.MovePosition(movePoint[nextPoint].transform.position);
                    ++nowPoint;

                    //現在地が配列の最後だった場合
                    if (nowPoint + 1 >= movePoint.Length)
                    {
                        returnPoint = true;
                    }
                }
                Vector2 dir = (movePoint[nextPoint].transform.position - this.transform.position).normalized;
                if (dir.x < 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                }
            }
            //折返し進行
            else
            {
                int nextPoint = nowPoint - 1;

                //目標ポイントとの誤差がわずかになるまで移動
                if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
                {
                    //現在地から次のポイントへのベクトルを作成
                    Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);

                    //次のポイントへ移動
                    rb2D.MovePosition(toVector);
                }
                //次のポイントを１つ戻す
                else
                {
                    rb2D.MovePosition(movePoint[nextPoint].transform.position);
                    --nowPoint;

                    //現在地が配列の最初だった場合
                    if (nowPoint <= 0)
                    {
                        returnPoint = false;
                    }
                }
                Vector2 dir = (movePoint[nextPoint].transform.position - this.transform.position).normalized;
                if (dir.x < 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                }
            }
        }

        
    }
    void Attack()
    {

    }
    void Chase()
    {
        Light.GetComponent<Light2D>().color = lightColor;
        lightColor = new Color(colorValueR, colorValueG, colorValueB);
        // ずっと、目標オブジェクトの方向を調べて
        Vector2 dir = (Player.transform.position - this.transform.position).normalized;

        // その方向へ指定した量で進む
        float vx = dir.x * chaceSpeed;
        float vy = dir.y * chaceSpeed;

        rb2D.velocity = new Vector2(vx, 0);

        if (dir.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }



    }
}
