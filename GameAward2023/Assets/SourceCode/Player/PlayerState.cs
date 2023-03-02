using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool m_ChangeDirectionJump;

    public bool m_ChangeDirectionWallJump;

    //プレイヤー地面動くスピード
    public float m_OnFloorSpeed;
    //プレイヤー空中動くスピード
    public float m_OnAirSpeed;
    //ジャンプパワー(ジャンプ高さ)
    public float m_JumpPower;


    //プレイヤーインプット
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

    //地面チェックしてるか？
    private bool m_OnFloor;

    public bool OnFloor 
    {
        set { m_OnFloor = value; }
        get { return m_OnFloor;  }
    }

    //ジャンプ当たるLAYER
    public LayerMask m_OnFloorHitLayer;
    //ジャンプ判定距離
    public float m_OnFloorDistance;

    private Vector3 m_FirstJumpVel;
    
    public  Vector3 FirstJumpVel 
    {
        set { m_FirstJumpVel = value; }
        get { return m_FirstJumpVel;  }
    }
    //-----------------------------------
    //壁ジャンプ
    //-----------------------------------
    //壁ジャンプしてるか？
    private bool m_IsWallJump;

    public bool IsWallJump 
    {
        set { m_IsWallJump = value; }
        get { return m_IsWallJump; }
    }

    //壁ジャンプ当たるLAYER
    public LayerMask m_WallJumpHitLayer;
    //壁ジャンプ判定距離
    public float m_WallJumpDistance;
    public float m_WallJumpWalkDistance;

    //壁ジャンプパワー(ジャンプ高さ)
    public float m_WallJumpPower;
    //壁ジャンプパワー(ジャンプ長さ)
    public float m_WallJumpSpeed;
    //壁ジャンプ与える速度
    private Vector3 m_WallJumpVel;

    public Vector3 WallJumpVel
    {
        set { m_WallJumpVel = value; }
        get { return m_WallJumpVel; }
    }


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

    //ダブルジャンプ当たるLAYER
    public LayerMask m_DoubleJumpHitLayer;
    //ダブルジャンプ判定距離
    public float m_DoubleJumpDistance;
    //ダブルジャンプパワー(ジャンプ高さ)
    public float m_DoubleJumpPower;
    //ダブルジャンプパワー(ジャンプ長さ)
    public float m_DoubleJumpSpeed;
    //ダブルジャンプ与える速度
    private Vector3 m_DoubleJumpVel;

    public Vector3 DoubleJumpVel
    {
        set { m_DoubleJumpVel = value; }
        get { return m_DoubleJumpVel; }
    }
    //-----------------------------------
    //スライド
    //-----------------------------------
    //スライドしてるか
    private bool m_Slide;

    public bool IsSlide
    {
        set { m_Slide = value; }
        get { return m_Slide;  }
    }

    //スライド速度
    private Vector3 m_SlideVel;

    public Vector3 SlideVel
    {
        set { m_SlideVel = value; }
        get { return m_SlideVel;  }
    }


    //スライド与える速度
    public float  m_SlideSpeed;
    //最大スライド時間
    public int    m_MaxSlideTimer;
    //今スライド時間
    private float m_SlideTimer;

    public float SlideTimer
    {
        set { m_SlideTimer = value; }
        get { return m_SlideTimer; }
    }

    //使わない
    //public float m_WallJumpSnowMotion;
    //public Vector2 m_WallJumpSnowMotionRange;

    //プレーヤーがターゲット取ってるか？
    private GameObject m_Target = null;

    public GameObject Target
    {
        set { m_Target = value; }
        get { return m_Target; }
    }
}
