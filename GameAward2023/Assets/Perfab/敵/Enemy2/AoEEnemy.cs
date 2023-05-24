using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class AoEEnemy : MonoBehaviour
{

    GameObject Player;
    public GameObject GravityFloor;
    [SerializeField] GameObject Light;
    //public string targetObjectName;// 目標オブジェクト名
    [Header("移動経路")] public GameObject[] movePoint;
    [Header("索敵範囲")] public float searchRange = 5;//索敵範囲
    [Header("攻撃範囲")] public float attackRange = 3;//攻撃範囲
    [Header("CHACE状態のspeed")] public float speed = 4; // スピード
    private float lightRange;//ライトの距離
    private float dis;//プレイヤーとエネミーの距離
    private Vector3 previousPosition;
    [Header("WARNING時間")] public float wariningTime = 100;//Warningの時の索敵時間
    private float limitTime;
    private Color lightColor;
    [Header("警戒色")]
    public float colorValueR;
    public float colorValueG;
    public float colorValueB;

    private Rigidbody2D rb2D;

    private int nowPoint = 0;
    private bool returnPoint = false;
    // Start is called before the first frame update

   
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player");
        previousPosition = Player.transform.position;
        GetComponent<State_Enemy>().State = State_Enemy.EnemyAiState.MOVE;

        lightRange = Light.GetComponent<Light2D>().pointLightOuterRadius;
        lightRange = Light.GetComponent<Light2D>().pointLightInnerRadius;
        lightRange = searchRange;

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
    // Update is called once per frame
    
    void FixedUpdate()
    {
        
        dis = Vector3.Distance(this.transform.position, Player.transform.position);
        Debug.Log(GetComponent<State_Enemy>().State);
        // 現在のステートのUpdateを呼び出す
        switch (GetComponent<State_Enemy>().State)
        {
            case State_Enemy.EnemyAiState.WAIT:
                Wait();
                break;
            case State_Enemy.EnemyAiState.MOVE:
                Move();
                break;
            case State_Enemy.EnemyAiState.CHASE:
                Chase();
                break;
            case State_Enemy.EnemyAiState.ATTACK:
                Attack();
                break;
            case State_Enemy.EnemyAiState.WARNING:
                Warning();
                break;
        }

        //OuterRadiusの設定
        Light.GetComponent<Light2D>().pointLightOuterRadius = lightRange;
        Light.GetComponent<Light2D>().pointLightInnerRadius = lightRange * 0.1f;
    }


    void Wait()
    {
      
    }
    void Move()
    {
        previousPosition = Player.transform.position;
        Light.GetComponent<Light2D>().color = lightColor;
        lightColor = new Color(0.5f, 0.5f, 0.5f);
        if (dis < searchRange)
        {
            GetComponent<State_Enemy>().State = State_Enemy.EnemyAiState.CHASE;
        }

        if (movePoint != null && movePoint.Length > 1 && rb2D != null)
        {
            //通常進行
            if (!returnPoint)
            {
                int nextPoint = nowPoint + 1;

                //目標ポイントとの誤差がわずかになるまで移動
                if (Vector2.Distance(new Vector2(transform.position.x, 0.0f), new Vector2(movePoint[nextPoint].transform.position.x, 0.0f)) > 0.1f)
                {
                    //現在地から次のポイントへのベクトルを作成
                    Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);
                   // Debug.Log(Vector2.Distance(transform.position, movePoint[nextPoint].transform.position));
                   if(toVector.x < 0) 
                    {
                        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward);
                        transform.rotation = toRotation;
                    }
                    else 
                    {
                        Quaternion toRotation = Quaternion.LookRotation(-Vector3.forward);
                        transform.rotation = toRotation;
                    }
                    //次のポイントへ移動
                    rb2D.MovePosition(toVector);
                }
                //次のポイントを１つ進める
                else
                {
                    rb2D.MovePosition(movePoint[nextPoint].transform.position);
                    ++nowPoint;
                    //現在地が配列の最後だった場合
                    if (nowPoint + 1 >= movePoint.Length - 1)
                    {
                        returnPoint = true;
                    }
                }
            }
            //折返し進行
            else
            {
                int nextPoint = nowPoint - 1;

                //目標ポイントとの誤差がわずかになるまで移動
                if (Vector2.Distance(new Vector2(transform.position.x, 0.0f), new Vector2(movePoint[nextPoint].transform.position.x,0.0f)) > 0.1f)
                {
                    //現在地から次のポイントへのベクトルを作成
                    Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);
                    if (toVector.x > 0)
                    {
                        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward);
                        transform.rotation = toRotation;
                    }
                    else
                    {
                        Quaternion toRotation = Quaternion.LookRotation(-Vector3.forward);
                        transform.rotation = toRotation;
                    }
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
            }
        }
    }
    public void Chase()
    {
        Light.GetComponent<Light2D>().color = lightColor;
        lightColor = new Color(colorValueR, colorValueG, colorValueB);
        // ずっと、目標オブジェクトの方向を調べて
        Vector2 dir = (Player.transform.position - this.transform.position).normalized;

        // その方向へ指定した量で進む
        float vx = dir.x * speed;
        float vy = dir.y * speed;

        if (dir.x > 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(-Vector3.forward);
            transform.rotation = toRotation;
            this.transform.Translate(-vx / 50, 0, 0);
        }
        else
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward);
            transform.rotation = toRotation;
            this.transform.Translate(vx / 50, 0, 0);

        }


        //if(dis < attackRange)
        //{
        //    GetComponent<State_Enemy>().State = State_Enemy.EnemyAiState.ATTACK;
        //}
    }
    void Attack()
    {
        Light.GetComponent<Light2D>().color = lightColor;
        lightColor = new Color(colorValueR, colorValueG, colorValueB);
        // ずっと、目標オブジェクトの方向を調べて
        Vector2 dir = (Player.transform.position - this.transform.position).normalized;
        // その方向へ指定した量で進む
        float vx = dir.x * speed;
        float vy = dir.y * speed;

        if (dir.x > 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward);
            transform.rotation = toRotation;
        }
        else
        {
            Quaternion toRotation = Quaternion.LookRotation(-Vector3.forward);
            transform.rotation = toRotation;
        }

        this.transform.Translate(vx / 50, 0, 0);
        //アニメーション差し込み
        //攻撃時の当たり判定
    }

   void Warning()
    {
        limitTime++;
        
        if (dis < searchRange)
        {
            GetComponent<State_Enemy>().State = State_Enemy.EnemyAiState.CHASE;
            
        }
        else
        {
            
            Vector2 dir = (previousPosition - this.transform.position).normalized;
            float absX = Mathf.Abs(dir.x);
            float vx;
            float vy;
            
                // その方向へ指定した量で進む
                vx = dir.x * speed;
                vy = dir.y * speed;
            
            
            this.transform.Translate(vx / 50, 0, vy / 50);
            

        }
        if(limitTime > wariningTime)
        {
            GetComponent<State_Enemy>().State = State_Enemy.EnemyAiState.MOVE;
            limitTime = 0;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && GetComponent<State_Enemy>().State == State_Enemy.EnemyAiState.ATTACK)
        {
            GameObject.Find("GameSystem").GetComponent<GameSystem>().GameOver = true;
        }
    }
}

