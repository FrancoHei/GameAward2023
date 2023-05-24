using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private AnimationReferenceAsset m_IdleAnimation;
    [SerializeField] private AnimationReferenceAsset m_IdleHoldAnimation;
    [SerializeField] private AnimationReferenceAsset m_WalkAnimation;
    [SerializeField] private AnimationReferenceAsset m_WalkHoldAnimation;
    [SerializeField] private AnimationReferenceAsset m_RunAnimation;
    [SerializeField] private AnimationReferenceAsset m_RunHoldAnimation;
    [SerializeField] private AnimationReferenceAsset m_JumpStartAnimation;
    [SerializeField] private AnimationReferenceAsset m_JumpUpAnimation;
    [SerializeField] private AnimationReferenceAsset m_JumpTopAnimation;
    [SerializeField] private AnimationReferenceAsset m_JumpDownAnimation;
    [SerializeField] private AnimationReferenceAsset m_JumpEndAnimation;

    [SerializeField] private AnimationReferenceAsset m_SJumpUpAnimation;
    [SerializeField] private AnimationReferenceAsset m_SJumpTopAnimation;

    [SerializeField] private AnimationReferenceAsset m_WJumpAnimation;
    [SerializeField] private AnimationReferenceAsset m_RJumpAnimation;

    [SerializeField] private AnimationReferenceAsset m_SlideAnimation;
    [SerializeField] private AnimationReferenceAsset m_SlideingAnimation;
    [SerializeField] private AnimationReferenceAsset m_SlideStandUpAnimation;

    [SerializeField] private AnimationReferenceAsset m_ThrowAnimation;

    SkeletonAnimation m_Ska;
    private PlayerState m_PS;
    private Rigidbody2D m_Rb;
    // Start is called before the first frame update
    void Start()
    {
        m_PS  = GetComponent<PlayerState>();
        m_Rb  = GetComponent<Rigidbody2D>();
        m_Ska = GetComponent<SkeletonAnimation>();
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (m_Ska.AnimationName == m_ThrowAnimation.Animation.Name)
        {
            if(!m_Ska.AnimationState.GetCurrent(0).IsComplete)
            {
                return;
            }else 
            {
                GameObject.Find("Player").GetComponents<CapsuleCollider2D>()[0].isTrigger = false;
                GameObject.Find("Player").GetComponents<CapsuleCollider2D>()[1].isTrigger = false;
                GameObject.Find("Player").GetComponents<CapsuleCollider2D>()[2].isTrigger = false;
                GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = 5;
                GameObject.Find("Player").GetComponent<PlayerState>().ReadyThrow   = false;
                GameObject.Find("Player").GetComponent<PlayerMovement>().InitJump();
                GameObject.Find("GameSystem").GetComponent<GameSystem>().CanInput = true;
            }
        }

        if (m_PS.IsSlide) 
        {
            if (m_Ska.AnimationName != m_SlideingAnimation.Animation.Name && m_Ska.AnimationName != m_SlideAnimation.Animation.Name && m_Ska.AnimationName != m_SlideStandUpAnimation.Animation.Name) 
            {
                m_Ska.AnimationState.SetAnimation(0, m_SlideAnimation.Animation, false);
            }

            if (m_Ska.AnimationName == m_SlideAnimation.Animation.Name && m_Ska.AnimationState.GetCurrent(0).IsComplete)
            {
                m_Ska.AnimationState.SetAnimation(0, m_SlideingAnimation.Animation, true);
                m_Ska.AnimationState.TimeScale = 1.0f;
            }
            return;
        }

        if (m_PS.Target) 
        {
            if (m_Rb.velocity == Vector2.zero &&
                        m_Ska.AnimationName != m_JumpStartAnimation.Animation.Name &&
                        m_Ska.AnimationName != m_JumpEndAnimation.Animation.Name)
            {
                if (m_Ska.AnimationName != m_IdleHoldAnimation.Animation.Name) 
                {
                    m_Ska.AnimationState.SetAnimation(0, m_IdleHoldAnimation.Animation, true);
                    m_Ska.AnimationState.TimeScale = 1.0f;
                }
            }else
            if (m_Rb.velocity.x != 0.0f && !m_PS.IsJump && !m_PS.IsLeftJump && !m_PS.IsLeftWallJump && !m_PS.IsRightJump && !m_PS.IsRightWallJump && !m_PS.IsSlideJump && !m_PS.IsDoubleJump)
            {
                if (Mathf.Abs(m_Rb.velocity.x) > 3)
                {
                    if (m_Ska.AnimationName != m_RunHoldAnimation.Animation.Name)
                        m_Ska.AnimationState.SetAnimation(0, m_RunHoldAnimation.Animation, true);
                }else
                if (m_Ska.AnimationName != m_WalkHoldAnimation.Animation.Name) 
                {
                    m_Ska.AnimationState.SetAnimation(0, m_WalkHoldAnimation.Animation, true);
                    m_Ska.AnimationState.TimeScale = 1.0f;
                }
            }

            if (m_Rb.velocity.y < -0.01f)
            {
                if (m_Ska.AnimationName != m_JumpDownAnimation.Animation.Name)
                    m_Ska.AnimationState.SetAnimation(0, m_JumpDownAnimation.Animation, true);
            }
            return;
        }

        if (m_PS.IsSlideJump) 
        {
            if (m_Rb.velocity.y > 0.0f)
            {
                if (m_Ska.AnimationName == m_JumpStartAnimation.Animation.Name && m_Ska.AnimationState.GetCurrent(0).IsComplete)
                {
                    m_Ska.AnimationState.SetAnimation(0, m_SJumpUpAnimation.Animation, true);
                    m_Ska.AnimationState.TimeScale = 1.0f;
                }
                return;
            }

            if (m_Ska.AnimationName == m_SJumpUpAnimation.Animation.Name && m_Rb.velocity.y <= 0.0001f && m_Rb.velocity.y >= 0.0f)
            {
                m_Ska.AnimationState.SetAnimation(0, m_JumpTopAnimation.Animation, false);
                m_Ska.AnimationState.TimeScale = 1.0f;
                return;
            }

        }

        if (m_PS.IsLeftWallJump || m_PS.IsRightWallJump) 
        {
         if (m_Rb.velocity.y > 0.0f)
            {
                if (m_Ska.AnimationName != m_WJumpAnimation.Animation.Name)
                {
                    m_Ska.AnimationState.SetAnimation(0, m_WJumpAnimation.Animation, true);
                    m_Ska.AnimationState.TimeScale = 1.0f;
                }
                return;
            }

            if (m_Ska.AnimationName == m_WJumpAnimation.Animation.Name && m_Rb.velocity.y <= 0.0001f && m_Rb.velocity.y >= 0.0f)
            {
                m_Ska.AnimationState.SetAnimation(0, m_JumpTopAnimation.Animation, false);
                m_Ska.AnimationState.TimeScale = 1.0f;
                return;
            }
        }

        if (m_PS.IsDoubleJump)
        {
            if (m_Ska.AnimationName != m_RJumpAnimation.Animation.Name)
            {
                m_Ska.AnimationState.SetAnimation(0, m_RJumpAnimation.Animation, true);
                m_Ska.AnimationState.TimeScale = 1.0f;
            }
            return;
        }

        if (m_Rb.velocity == Vector2.zero &&
            m_Ska.AnimationName != m_JumpStartAnimation.Animation.Name &&
            m_Ska.AnimationName != m_JumpEndAnimation.Animation.Name)
        {
            if (m_Ska.AnimationName != m_IdleAnimation.Animation.Name)
                m_Ska.AnimationState.SetAnimation(0, m_IdleAnimation.Animation, true);
        }
        if (m_Rb.velocity.x != 0.0f && !m_PS.IsJump && !m_PS.IsLeftJump && !m_PS.IsLeftWallJump && !m_PS.IsRightJump && !m_PS.IsRightWallJump && !m_PS.IsSlideJump && !m_PS.IsDoubleJump)
        {
            if(Mathf.Abs(m_Rb.velocity.x) > 3) 
            {
                if (m_Ska.AnimationName != m_RunAnimation.Animation.Name)
                    m_Ska.AnimationState.SetAnimation(0, m_RunAnimation.Animation, true);
            }else 
            {
                if (m_Ska.AnimationName != m_WalkAnimation.Animation.Name)
                    m_Ska.AnimationState.SetAnimation(0, m_WalkAnimation.Animation, true);
            }
        }
        if (m_Rb.velocity.y > 0.0f)
        {
            if (m_Ska.AnimationName == m_JumpStartAnimation.Animation.Name && m_Ska.AnimationState.GetCurrent(0).IsComplete)
            {
                m_Ska.AnimationState.SetAnimation(0, m_JumpUpAnimation.Animation, true);
                m_Ska.AnimationState.TimeScale = 1.0f;
            }
        }

        if (m_Rb.velocity.y < 0.0f)
        {
            if (m_Ska.AnimationName == m_JumpTopAnimation.Animation.Name && m_Ska.AnimationState.GetCurrent(0).IsComplete)
            {
                m_Ska.AnimationState.SetAnimation(0, m_JumpDownAnimation.Animation, true);
                m_Ska.AnimationState.TimeScale = 1.0f;
                return;
            }
        }

        if (m_Ska.AnimationName == m_JumpUpAnimation.Animation.Name && m_Rb.velocity.y < 0.5f && m_Rb.velocity.y > -0.5f)
        {
            m_Ska.AnimationState.SetAnimation(0, m_JumpTopAnimation.Animation, true);
            m_Ska.AnimationState.TimeScale = 1.0f;
            return;
        }

        if ((m_Ska.AnimationName == m_JumpDownAnimation.Animation.Name || m_Ska.AnimationName == m_RJumpAnimation.Animation.Name) && m_Rb.velocity.y <= -0.5f)
        {
            m_Ska.AnimationState.SetAnimation(0, m_JumpEndAnimation.Animation, false);
            m_Ska.AnimationState.TimeScale = 1.0f;
            return;
        }
    }

    public void SetAnimation(string s) 
    {
        if(s == "JumpStart") 
        {
            if (m_Ska.AnimationName != m_JumpStartAnimation.Animation.Name && !m_PS.IsDoubleJump) 
            {
                m_Ska.AnimationState.SetAnimation(0, m_JumpStartAnimation.Animation, false);
                m_Ska.AnimationState.TimeScale = 1.0f;
            }
        }
        if (s == "Throw") 
        {
            if (m_Ska.AnimationName != m_ThrowAnimation.Animation.Name)
            {
                m_Ska.AnimationState.SetAnimation(0, m_ThrowAnimation.Animation, true);
                m_Ska.AnimationState.TimeScale = 1.0f;
            }
        }
        if (s == "SlideStandUp")
        {
            if (m_Ska.AnimationName != m_SlideingAnimation.Animation.Name)
            {
                m_Ska.AnimationState.SetAnimation(0, m_SlideStandUpAnimation.Animation, false);
                m_Ska.AnimationState.TimeScale = 1.0f;
            }
        }
    }

    private bool CheckOnFloor()
    {
        LayerMask mask = LayerMask.GetMask("FallGround");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.5f, mask);

        if (hit && (hit.transform.gameObject.tag == "FallGround"))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
