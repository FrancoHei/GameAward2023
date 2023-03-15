using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugText : MonoBehaviour
{
    private TextMeshProUGUI m_Gui;

    // Start is called before the first frame update
    void Start()
    {
        m_Gui = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Gui.text = "Player Input OnFloor\t" + GameObject.Find("Player").GetComponent<PlayerState>().MoveMentInput;
        m_Gui.text += "\nPlayer Input OnAir\t\t" + GameObject.Find("Player").GetComponent<PlayerState>().OnAirMoveMentInput;
        m_Gui.text += "\nPlayer Speed\t\t" + GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity;

        string state = "";

        if (GameObject.Find("Player").GetComponent<PlayerState>().IsJump)         { state = "JUMP"; }
        if (GameObject.Find("Player").GetComponent<PlayerState>().IsRightJump)    { state = "RIGHTJUMP"; }
        if (GameObject.Find("Player").GetComponent<PlayerState>().IsLeftJump)     { state = "LEFTJUMP"; }
        if (GameObject.Find("Player").GetComponent<PlayerState>().IsLeftWallJump) { state = "LEFTWALLJUMP"; }
        if (GameObject.Find("Player").GetComponent<PlayerState>().IsRightWallJump){ state = "RIGHTWALLJUMP"; }
        if (GameObject.Find("Player").GetComponent<PlayerState>().IsSlide)        { state = "SLIDE"; }
        if (GameObject.Find("Player").GetComponent<PlayerState>().IsSlideJump)    { state = "SLIDEJUMP"; }
        if (GameObject.Find("Player").GetComponent<PlayerState>().IsDoubleJump)   { state = "DOUBLEJUMP"; }
        m_Gui.text += "\nPlayer State\t\t\t" + state;

    }
}
