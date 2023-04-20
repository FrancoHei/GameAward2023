using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{

    [Header("���ԓr�������ς��邩")]
    public bool m_ChangeDirectionJump;

    //�v���C���[�n�ʓ����X�s�[�h
    [Header("�n�ʈړ����x")]
    public float m_OnFloorSpeed;
    //�v���C���[�󒆓����X�s�[�h
    [Header("�󒆈ړ����x")]
    public float m_OnAirSpeed;
    [Header("�ǃW�����v�󒆈ړ����x")]
    public float m_OnJumpAirSpeed;

    //------------------------------
    //���ʃW�����v
    //------------------------------
    //------------------------------
    [Header("�W�����v����")]
    public float m_JumpPower;
    [Header("�W�����v���ړ����x")]
    public float m_JumpSpeed;
    private bool m_IsJump;

    public bool IsJump
    {
        set { m_IsJump = value; }
        get { return m_IsJump; }
    }

    private Vector3 m_FirstJumpVel;

    public Vector3 FirstJumpVel
    {
        set { m_FirstJumpVel = value; }
        get { return m_FirstJumpVel; }
    }

    //-----------------------------
    //-----------------------------
    //-----------------------------
    //�v���C���[�C���v�b�g
    //-----------------------------
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

    //----------------------------
    //�n�ʃ`�F�b�N���Ă邩�H
    private bool m_OnFloor;

    public bool OnFloor 
    {
        set { m_OnFloor = value; }
        get { return m_OnFloor;  }
    }

    //�W�����v������LAYER
    [Header("�n�ʓ����郌�C��")]
    public LayerMask m_OnFloorHitLayer;
    //�W�����v���苗��
    [Header("�n�ʔ��苗��")]
    public float m_OnFloorDistance;

    //-----------------------------------
    //-----------------------------------
    //-----------------------------------
    //�ǃW�����v
    //-----------------------------------
    //���ǃW�����v���Ă邩�H
    private bool m_IsLeftWallJump;

    public bool IsLeftWallJump 
    {
        set { m_IsLeftWallJump = value; }
        get { return m_IsLeftWallJump; }
    }

    //�E�ǃW�����v���Ă邩�H
    private bool m_IsRightWallJump;

    public bool IsRightWallJump
    {
        set { m_IsRightWallJump = value; }
        get { return m_IsRightWallJump; }
    }

    //�ǃW�����v������LAYER
    [Header("�ǃW�����v�����郌�C��")]
    public LayerMask m_WallJumpHitLayer;
    //�ǃW�����v���苗��
    [Header("�ǃW�����v���苗��")]
    public float m_WallJumpDistance;
    [Header("�ǃW�����v�����锻�苗��")]
    public float m_WallJumpWalkDistance;

    //�ǃW�����v�p���[(�W�����v����)
    [Header("�ǃW�����v����")]
    public float m_WallJumpPower;
    //�ǃW�����v�p���[(�W�����v����)
    [Header("�ǃW�����v���ړ��X�s�[�h")]
    public float m_WallJumpSpeed;
    //�ǃW�����v�^���鑬�x
    private Vector3 m_WallJumpVel;

    public Vector3 WallJumpVel
    {
        set { m_WallJumpVel = value; }
        get { return m_WallJumpVel; }
    }

    public float m_MaxWallJumpVel;

    private bool m_IsRightJump;

    public bool IsRightJump 
    {
        set { m_IsRightJump = value; }
        get { return m_IsRightJump;  }
    }

    private Vector3 m_RightJumpVel;

    public Vector3 RightJumpVel
    {
        set { m_RightJumpVel = value; }
        get { return m_RightJumpVel; }
    }





    private bool m_IsLeftJump;

    public bool IsLeftJump
    {
        set { m_IsLeftJump = value; }
        get { return m_IsLeftJump; }
    }

    private Vector3 m_LeftJumpVel;

    public Vector3 LeftJumpVel
    {
        set { m_LeftJumpVel = value; }
        get { return m_LeftJumpVel; }
    }

    //-----------------------------------
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

    //�_�u���W�����v�p���[(�W�����v����)
    [Header("��i�W�����v����")]
    public float m_DoubleJumpPower;
    //�_�u���W�����v�p���[(�W�����v����)
    [Header("��i�W�����v���ړ��X�s�[�h")]
    public float m_DoubleJumpSpeed;
    //�_�u���W�����v�^���鑬�x
    private Vector3 m_DoubleJumpVel;

    public Vector3 DoubleJumpVel
    {
        set { m_DoubleJumpVel = value; }
        get { return m_DoubleJumpVel; }
    }

    //-----------------------------------
    //-----------------------------------
    //-----------------------------------
    //�X���C�f�B���O
    //-----------------------------------
    //�X���C�f�B���O���Ă邩
    private bool m_Slide;

    public bool IsSlide
    {
        set { m_Slide = value; }
        get { return m_Slide;  }
    }

    //�X���C�f�B���O���x
    private Vector3 m_SlideVel;

    public Vector3 SlideVel
    {
        set { m_SlideVel = value; }
        get { return m_SlideVel;  }
    }


    //�X���C�f�B���O�^���鑬�x
    [Header("�X���C�f�B���O�X�s�[�h")]
    public float  m_SlideSpeed;
    //�ő�X���C�h����
    [Header("�X���C�f�B���O�ő厞��")]
    public int    m_MaxSlideTimer;
    //���X���C�h����
    private float m_SlideTimer;

    public float SlideTimer
    {
        set { m_SlideTimer = value; }
        get { return m_SlideTimer; }
    }

    //-----------------------------------
    //-----------------------------------
    //-----------------------------------
    //�X���C�f�B���O�W�����v
    //-----------------------------------
    //�X���C�f�B���O�W�����v���Ă邩
    private bool m_SlideJump;

    //�X���C�f�B���O�W�����v�p���[(�W�����v����)
    [Header("�X���C�f�B���O�W�����v����")]
    public float m_SlideJumpPower;
    public bool IsSlideJump
    {
        set { m_SlideJump = value; }
        get { return m_SlideJump; }
    }

    //�X���C�h���x
    private Vector3 m_SlideJumpVel;
    public Vector3 SlideJumpVel
    {
        set { m_SlideJumpVel = value; }
        get { return m_SlideJumpVel; }
    }

    //�X���C�h�^���鑬�x
    [Header("�X���C�f�B���O�W�����v���ړ��X�s�[�h")]
    public float m_SlideJumpSpeed;

    //�X���C�f�B���O�W�����v����󒆃W�����v���x�I�t�Z�b�g
    [Header("�X���C�f�B���O�W�����v����󒆃W�����v���x�I�t�Z�b�g")]
    public float m_SlideJumpDoubleJumpOffset;

    //�g��Ȃ�
    //public float m_WallJumpSnowMotion;
    //public Vector2 m_WallJumpSnowMotionRange;

    //�v���[���[���^�[�Q�b�g����Ă邩�H
    private GameObject m_Target = null;

    [Header("�^�[�Q�b�g������p���[")]
    public float m_TargetThrowPower;
    [Header("�^�[�Q�b�g������p���[�I�t�Z�b�g")]
    public Vector2 m_TargetThrowPowerOffset;
    public GameObject Target
    {
        set { m_Target = value; }
        get { return m_Target; }
    }

    //------------------------------------
    //------------------------------------
    private bool m_IsWire;

    public bool IsWire 
    {
        set { m_IsWire = value; }
        get { return m_IsWire;  }
    }

    private GameObject m_Wire;

    public GameObject Wire 
    {
        set { m_Wire = value; }
        get { return m_Wire;  }
    }

    [Header("���C���[�X�s�[�h")]
    public float m_WireSpeed;

}
