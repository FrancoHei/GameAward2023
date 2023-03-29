using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class NarrowEnemy : MonoBehaviour
{
    GameObject Player;
    [SerializeField] GameObject Light;
    [Header("�ړ��o�H")] public GameObject[] movePoint;
    [Header("MOVE��Ԃ�speed")] public float speed = 1.0f;
    [Header("CHACE��Ԃ�speed")] public float chaceSpeed = 4; // �X�s�[�h

    private Color lightColor;
    [Header("�x���F")]
    public float colorValueR;
    public float colorValueG;
    public float colorValueB;
    //bool rightTleftF;//�E������
    int xVector;//���E�ǂ����ɐi�ނ��̐���

    private float dis;//�v���C���[�ƃG�l�~�[�̋���
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

        // ���݂̃X�e�[�g��Update���Ăяo��
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
            //�ʏ�i�s
            if (!returnPoint)
            {

                int nextPoint = nowPoint + 1;


                //�ڕW�|�C���g�Ƃ̌덷���킸���ɂȂ�܂ňړ�
                if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
                {
                    //���ݒn���玟�̃|�C���g�ւ̃x�N�g�����쐬
                    Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);

                    //���̃|�C���g�ֈړ�
                    rb2D.MovePosition(toVector);
                }
                //���̃|�C���g���P�i�߂�
                else
                {
                    rb2D.MovePosition(movePoint[nextPoint].transform.position);
                    ++nowPoint;

                    //���ݒn���z��̍Ōゾ�����ꍇ
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
            //�ܕԂ��i�s
            else
            {
                int nextPoint = nowPoint - 1;

                //�ڕW�|�C���g�Ƃ̌덷���킸���ɂȂ�܂ňړ�
                if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
                {
                    //���ݒn���玟�̃|�C���g�ւ̃x�N�g�����쐬
                    Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);

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
        // �����ƁA�ڕW�I�u�W�F�N�g�̕����𒲂ׂ�
        Vector2 dir = (Player.transform.position - this.transform.position).normalized;

        // ���̕����֎w�肵���ʂŐi��
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
