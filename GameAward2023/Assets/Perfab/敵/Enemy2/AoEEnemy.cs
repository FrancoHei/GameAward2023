using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class AoEEnemy : MonoBehaviour
{

    GameObject Player;
    public GameObject GravityFloor;
    [SerializeField] GameObject Light;
    //public string targetObjectName;// �ڕW�I�u�W�F�N�g��
    [Header("�ړ��o�H")] public GameObject[] movePoint;
    [Header("���G�͈�")] public float searchRange = 5;//���G�͈�
    [Header("�U���͈�")] public float attackRange = 3;//�U���͈�
    [Header("CHACE��Ԃ�speed")] public float speed = 4; // �X�s�[�h
    private float lightRange;//���C�g�̋���
    private float dis;//�v���C���[�ƃG�l�~�[�̋���
    private Vector3 previousPosition;
    [Header("WARNING����")] public float wariningTime = 100;//Warning�̎��̍��G����
    private float limitTime;
    private Color lightColor;
    [Header("�x���F")]
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
        // ���݂̃X�e�[�g��Update���Ăяo��
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

        //OuterRadius�̐ݒ�
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
            //�ʏ�i�s
            if (!returnPoint)
            {
                int nextPoint = nowPoint + 1;

                //�ڕW�|�C���g�Ƃ̌덷���킸���ɂȂ�܂ňړ�
                if (Vector2.Distance(new Vector2(transform.position.x, 0.0f), new Vector2(movePoint[nextPoint].transform.position.x, 0.0f)) > 0.1f)
                {
                    //���ݒn���玟�̃|�C���g�ւ̃x�N�g�����쐬
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
                    //���̃|�C���g�ֈړ�
                    rb2D.MovePosition(toVector);
                }
                //���̃|�C���g���P�i�߂�
                else
                {
                    rb2D.MovePosition(movePoint[nextPoint].transform.position);
                    ++nowPoint;
                    //���ݒn���z��̍Ōゾ�����ꍇ
                    if (nowPoint + 1 >= movePoint.Length - 1)
                    {
                        returnPoint = true;
                    }
                }
            }
            //�ܕԂ��i�s
            else
            {
                int nextPoint = nowPoint - 1;

                //�ڕW�|�C���g�Ƃ̌덷���킸���ɂȂ�܂ňړ�
                if (Vector2.Distance(new Vector2(transform.position.x, 0.0f), new Vector2(movePoint[nextPoint].transform.position.x,0.0f)) > 0.1f)
                {
                    //���ݒn���玟�̃|�C���g�ւ̃x�N�g�����쐬
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
                    //���̃|�C���g�ֈړ�
                    rb2D.MovePosition(toVector);
                }
                //���̃|�C���g���P�߂�
                else
                {
                    rb2D.MovePosition(movePoint[nextPoint].transform.position);
                    --nowPoint;

                    //���ݒn���z��̍ŏ��������ꍇ
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
        // �����ƁA�ڕW�I�u�W�F�N�g�̕����𒲂ׂ�
        Vector2 dir = (Player.transform.position - this.transform.position).normalized;

        // ���̕����֎w�肵���ʂŐi��
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
        // �����ƁA�ڕW�I�u�W�F�N�g�̕����𒲂ׂ�
        Vector2 dir = (Player.transform.position - this.transform.position).normalized;
        // ���̕����֎w�肵���ʂŐi��
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
        //�A�j���[�V������������
        //�U�����̓����蔻��
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
            
                // ���̕����֎w�肵���ʂŐi��
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

