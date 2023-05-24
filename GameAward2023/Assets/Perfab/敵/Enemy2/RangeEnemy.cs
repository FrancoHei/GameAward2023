using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RangeEnemy : MonoBehaviour
{
    GameObject Player;
    public GameObject GravityFloor;
    public GameObject BulletObj;
    [SerializeField] GameObject Light;
    [Header("攻撃範囲")] public float attackRange = 10;//攻撃範囲

    [Header("弾の間隔")] public float bulletTime = 100;
    [Header("弾のスピード")] public float bulletSpeed = 10;
    private float bulletLimit = 0;
    
    Vector3 bulletPoint;
    

    [Header("移動経路")] public GameObject[] movePoint;
    [Header("MOVE状態のspeed")] public float speed = 1.0f;
    [Header("CHACE状態のspeed")] public float chaceSpeed = 4; // スピード
    [Header("WARNING時間")] public float wariningTime = 100;//Warningの時の索敵時間
    private Vector3 previousPosition;
    private float limitTime;
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
        previousPosition = Player.transform.position;
        
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
        
        dis = Vector3.Distance(new Vector3(transform.position.x,0.0f,0.0f), new Vector3(Player.transform.position.x, 0.0f,0.0f));

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
            case State_Enemy.EnemyAiState.WARNING:
                Warning();
                break;
        }
        /////////////////////////////////
        
    }
    void Wait()
    {

    }
    void Move()
    {
        previousPosition = Player.transform.position;
        Light.GetComponent<Light2D>().color = lightColor;
        lightColor = new Color(0.5f, 0.5f, 0.5f);
        if (movePoint != null && movePoint.Length > 1 && rb2D != null)
        {
            //通常進行
            if (!returnPoint)
            {

                int nextPoint = nowPoint + 1;


                //目標ポイントとの誤差がわずかになるまで移動
                if (Vector2.Distance(new Vector2(transform.position.x, 0.0f), new Vector2(movePoint[nextPoint].transform.position.x,0.0f)) > 0.1f)
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
                if (Vector2.Distance(new Vector2(transform.position.x, 0.0f) , new Vector2(movePoint[nextPoint].transform.position.x, 0.0f)) > 0.1f)
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

        Debug.Log("aaaaa");
        Light.GetComponent<Light2D>().color = lightColor;
        lightColor = new Color(colorValueR, colorValueG, colorValueB, 0.0f);

        
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
            Debug.Log("aaaa");
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        bulletLimit++;
        if (bulletLimit > bulletTime)
        {
            var pos = gameObject.transform.position;
            var t = Instantiate(BulletObj, this.transform.position, Quaternion.identity) as GameObject;
            Vector2 vec = new Vector2(Player.transform.position.x,0.0f) - new Vector2(this.transform.position.x,0.0f);
            vec.Normalize();
            t.GetComponent<Rigidbody2D>().velocity = vec * bulletSpeed;
            bulletLimit = 0;
        }


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

        if (dis < attackRange)
        {
            GetComponent<State_Enemy>().State = State_Enemy.EnemyAiState.ATTACK;
        }

    }
    void Warning()
    {
        limitTime++;

        // Debug.Log("dis");

        Vector2 dir = (previousPosition - this.transform.position).normalized;
        float absX = Mathf.Abs(dir.x);
        float vx;
        float vy;

        // その方向へ指定した量で進む
        vx = dir.x * speed;
        vy = dir.y * speed;


        this.transform.Translate(vx / 50, 0, vy / 50);
        //Debug.Log("dis" + absX);
        if (dir.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }

        if (limitTime > wariningTime)
        {
            GetComponent<State_Enemy>().State = State_Enemy.EnemyAiState.MOVE;
            limitTime = 0;
        }
    }
}
