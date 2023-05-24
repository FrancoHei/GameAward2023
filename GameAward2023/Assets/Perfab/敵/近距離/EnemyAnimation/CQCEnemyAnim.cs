using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CQCEnemyAnim : MonoBehaviour
{
    private Animator animator;
    private State_Enemy enemystate;
    private SpriteRenderer SpriteRenderer;

    public bool Flip = false;
    [SerializeField] private Vector3 AdjustPosOnFlip;
    [SerializeField] private Vector3 AdjustPosNotFlip;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemystate = GetComponentInParent<State_Enemy>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FlipRight()
    {
        Flip = true;
    }
    public void FlipLeft()
    {
        Flip = true;
    }
    public void setFlip(bool value)
    {
        Flip = value;
    }


    // Update is called once per frame
    void Update()
    {
        State_Enemy.EnemyAiState Status = enemystate.State;

        //SpriteRenderer.flipX = Flip;

        //if (Flip)
        //    transform.localPosition = transform.localPosition  + AdjustPosNotFlip;
        //else
        //    transform.localPosition = transform.localPosition  + AdjustPosOnFlip;




        switch (Status)
        {
            case State_Enemy.EnemyAiState.WAIT:
                animator.SetBool("Warning",false);
                animator.SetBool("Find",false);
                animator.SetBool("Attack",false);
                break;
            case State_Enemy.EnemyAiState.MOVE:
                animator.SetBool("Warning", false);
                animator.SetBool("Find", false);
                animator.SetBool("Attack", false);
                break;
            case State_Enemy.EnemyAiState.ATTACK:
                animator.SetBool("Warning", true);
                animator.SetBool("Find", true);
                animator.SetBool("Attack", true);
                break;
            case State_Enemy.EnemyAiState.CHASE:
                animator.SetBool("Warning", true);
                animator.SetBool("Find", true);
                animator.SetBool("Attack", false);
                break;
            case State_Enemy.EnemyAiState.WARNING:
                animator.SetBool("Warning", true);
                animator.SetBool("Find", false);
                animator.SetBool("Attack", false);
                break;
            default:
                animator.SetBool("Warning", false);
                animator.SetBool("Find", false);
                animator.SetBool("Attack", false);
                break;
        }
    }
}
