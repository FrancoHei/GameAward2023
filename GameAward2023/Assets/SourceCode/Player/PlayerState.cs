using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{

    [Header("跳ぶ途中方向変えるか")]
    public bool m_ChangeDirectionJump;

    //プレイヤー地面動くスピード
    [Header("地面移動速度")]
    public float m_OnFloorSpeed;
    //プレイヤー空中動くスピード
    [Header("空中移動速度")]
    public float m_OnAirSpeed;
    [Header("壁ジャンプ空中移動速度")]
    public float m_OnJumpAirSpeed;

    //------------------------------
    //普通ジャンプ
    //------------------------------
    //------------------------------
    [Header("ジャンプ高さ")]
    public float m_JumpPower;
    [Header("ジャンプ横移動速度")]
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
    //プレイヤーインプット
    //-----------------------------
    private Vector3 m_MoveMentInput;

    public Vector3 MoveMentInput 
    {
        set { m_MoveMentInput = value; }
        get { return m_MoveMentInput; }
    }

    //プレイヤー空時インプット
    private Vector3 m_OnAirMoveMentInput;

    public Vector3 OnAirMoveMentInput
    {
        set { m_OnAirMoveMentInput = value; }
        get { return m_OnAirMoveMentInput; }
    }

    //----------------------------
    //地面チェックしてるか？
    private bool m_OnFloor;

    public bool OnFloor 
    {
        set { m_OnFloor = value; }
        get { return m_OnFloor;  }
    }

    //ジャンプ当たるLAYER
    [Header("地面当たるレイヤ")]
    public LayerMask m_OnFloorHitLayer;
    //ジャンプ判定距離
    [Header("地面判定距離")]
    public float m_OnFloorDistance;

    //-----------------------------------
    //-----------------------------------
    //-----------------------------------
    //壁ジャンプ
    //-----------------------------------
    //左壁ジャンプしてるか？
    private bool m_IsLeftWallJump;

    public bool IsLeftWallJump 
    {
        set { m_IsLeftWallJump = value; }
        get { return m_IsLeftWallJump; }
    }

    //右壁ジャンプしてるか？
    private bool m_IsRightWallJump;

    public bool IsRightWallJump
    {
        set { m_IsRightWallJump = value; }
        get { return m_IsRightWallJump; }
    }

    //壁ジャンプ当たるLAYER
    [Header("壁ジャンプ当たるレイヤ")]
    public LayerMask m_WallJumpHitLayer;
    //壁ジャンプ判定距離
    [Header("壁ジャンプ判定距離")]
    public float m_WallJumpDistance;
    [Header("壁ジャンプ歩ける判定距離")]
    public float m_WallJumpWalkDistance;

    //壁ジャンプパワー(ジャンプ高さ)
    [Header("壁ジャンプ高さ")]
    public float m_WallJumpPower;
    //壁ジャンプパワー(ジャンプ長さ)
    [Header("壁ジャンプ横移動スピード")]
    public float m_WallJumpSpeed;
    //壁ジャンプ与える速度
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
    //ダブルジャンプ
    //-----------------------------------
    //ダブルジャンプしてるか？
    private bool m_IsDoubleJump;

    public bool IsDoubleJump
    {
        set { m_IsDoubleJump = value; }
        get { return m_IsDoubleJump; }
    }

    //ダブルジャンプパワー(ジャンプ高さ)
    [Header("二段ジャンプ高さ")]
    public float m_DoubleJumpPower;
    //ダブルジャンプパワー(ジャンプ長さ)
    [Header("二段ジャンプ横移動スピード")]
    public float m_DoubleJumpSpeed;
    //ダブルジャンプ与える速度
    private Vector3 m_DoubleJumpVel;

    public Vector3 DoubleJumpVel
    {
        set { m_DoubleJumpVel = value; }
        get { return m_DoubleJumpVel; }
    }

    //-----------------------------------
    //-----------------------------------
    //-----------------------------------
    //スライディング
    //-----------------------------------
    //スライディングしてるか
    private bool m_Slide;

    public bool IsSlide
    {
        set { m_Slide = value; }
        get { return m_Slide;  }
    }

    //スライディング速度
    private Vector3 m_SlideVel;

    public Vector3 SlideVel
    {
        set { m_SlideVel = value; }
        get { return m_SlideVel;  }
    }


    //スライディング与える速度
    [Header("スライディングスピード")]
    public float  m_SlideSpeed;
    //最大スライド時間
    [Header("スライディング最大時間")]
    public int    m_MaxSlideTimer;
    //今スライド時間
    private float m_SlideTimer;

    public float SlideTimer
    {
        set { m_SlideTimer = value; }
        get { return m_SlideTimer; }
    }

    //-----------------------------------
    //-----------------------------------
    //-----------------------------------
    //スライディングジャンプ
    //-----------------------------------
    //スライディングジャンプしてるか
    private bool m_SlideJump;

    //スライディングジャンプパワー(ジャンプ高さ)
    [Header("スライディングジャンプ高さ")]
    public float m_SlideJumpPower;
    public bool IsSlideJump
    {
        set { m_SlideJump = value; }
        get { return m_SlideJump; }
    }

    //スライド速度
    private Vector3 m_SlideJumpVel;
    public Vector3 SlideJumpVel
    {
        set { m_SlideJumpVel = value; }
        get { return m_SlideJumpVel; }
    }

    //スライド与える速度
    [Header("スライディングジャンプ横移動スピード")]
    public float m_SlideJumpSpeed;

    //スライディングジャンプから空中ジャンプ速度オフセット
    [Header("スライディングジャンプから空中ジャンプ速度オフセット")]
    public float m_SlideJumpDoubleJumpOffset;

    //使わない
    //public float m_WallJumpSnowMotion;
    //public Vector2 m_WallJumpSnowMotionRange;

    //プレーヤーがターゲット取ってるか？
    private GameObject m_Target = null;

    [Header("ターゲット投げるパワー")]
    public float m_TargetThrowPower;
    [Header("ターゲット投げるパワーオフセット")]
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

    [Header("ワイヤースピード")]
    public float m_WireSpeed;

}
