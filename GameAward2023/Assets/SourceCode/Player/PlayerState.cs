using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool m_ChangeDirectionJump;

    public bool m_ChangeDirectionWallJump;

    //�v���C���[�n�ʓ����X�s�[�h
    public float m_OnFloorSpeed;
    //�v���C���[�󒆓����X�s�[�h
    public float m_OnAirSpeed;
    //�W�����v�p���[(�W�����v����)
    public float m_JumpPower;


    //�v���C���[�C���v�b�g
    private Vector3 m_MoveMentInput;

    public Vector3 MoveMentInput 
    {
        set { m_MoveMentInput = value; }
        get { return m_MoveMentInput; }
    }

    //�v���C���[�󎞃C���v�b�g
    private Vector3 m_OnAirMoveMentInput;

    public Vector3 OnAirMoveMentInput
    {
        set { m_OnAirMoveMentInput = value; }
        get { return m_OnAirMoveMentInput; }
    }

    //�n�ʃ`�F�b�N���Ă邩�H
    private bool m_OnFloor;

    public bool OnFloor 
    {
        set { m_OnFloor = value; }
        get { return m_OnFloor;  }
    }

    //�W�����v������LAYER
    public LayerMask m_OnFloorHitLayer;
    //�W�����v���苗��
    public float m_OnFloorDistance;

    private Vector3 m_FirstJumpVel;
    
    public  Vector3 FirstJumpVel 
    {
        set { m_FirstJumpVel = value; }
        get { return m_FirstJumpVel;  }
    }
    //-----------------------------------
    //�ǃW�����v
    //-----------------------------------
    //�ǃW�����v���Ă邩�H
    private bool m_IsWallJump;

    public bool IsWallJump 
    {
        set { m_IsWallJump = value; }
        get { return m_IsWallJump; }
    }

    //�ǃW�����v������LAYER
    public LayerMask m_WallJumpHitLayer;
    //�ǃW�����v���苗��
    public float m_WallJumpDistance;
    public float m_WallJumpWalkDistance;

    //�ǃW�����v�p���[(�W�����v����)
    public float m_WallJumpPower;
    //�ǃW�����v�p���[(�W�����v����)
    public float m_WallJumpSpeed;
    //�ǃW�����v�^���鑬�x
    private Vector3 m_WallJumpVel;

    public Vector3 WallJumpVel
    {
        set { m_WallJumpVel = value; }
        get { return m_WallJumpVel; }
    }


    //-----------------------------------

    //-----------------------------------
    //�_�u���W�����v
    //-----------------------------------
    //�_�u���W�����v���Ă邩�H
    private bool m_IsDoubleJump;

    public bool IsDoubleJump
    {
        set { m_IsDoubleJump = value; }
        get { return m_IsDoubleJump; }
    }

    //�_�u���W�����v������LAYER
    public LayerMask m_DoubleJumpHitLayer;
    //�_�u���W�����v���苗��
    public float m_DoubleJumpDistance;
    //�_�u���W�����v�p���[(�W�����v����)
    public float m_DoubleJumpPower;
    //�_�u���W�����v�p���[(�W�����v����)
    public float m_DoubleJumpSpeed;
    //�_�u���W�����v�^���鑬�x
    private Vector3 m_DoubleJumpVel;

    public Vector3 DoubleJumpVel
    {
        set { m_DoubleJumpVel = value; }
        get { return m_DoubleJumpVel; }
    }
    //-----------------------------------
    //�X���C�h
    //-----------------------------------
    //�X���C�h���Ă邩
    private bool m_Slide;

    public bool IsSlide
    {
        set { m_Slide = value; }
        get { return m_Slide;  }
    }

    //�X���C�h���x
    private Vector3 m_SlideVel;

    public Vector3 SlideVel
    {
        set { m_SlideVel = value; }
        get { return m_SlideVel;  }
    }


    //�X���C�h�^���鑬�x
    public float  m_SlideSpeed;
    //�ő�X���C�h����
    public int    m_MaxSlideTimer;
    //���X���C�h����
    private float m_SlideTimer;

    public float SlideTimer
    {
        set { m_SlideTimer = value; }
        get { return m_SlideTimer; }
    }

    //�g��Ȃ�
    //public float m_WallJumpSnowMotion;
    //public Vector2 m_WallJumpSnowMotionRange;

    //�v���[���[���^�[�Q�b�g����Ă邩�H
    private GameObject m_Target = null;

    public GameObject Target
    {
        set { m_Target = value; }
        get { return m_Target; }
    }
}
