using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    private bool m_IsBlackOut = false;
    private bool m_IsGameOver = false;
    private bool m_IsGameClear = false;

    public bool BlackOut
    {
        get { return m_IsBlackOut; }
        set { m_IsBlackOut = value; }

    }

    public bool GameOver
    {
        get { return m_IsGameOver; }
        set { m_IsGameOver = value; }

    }

    public bool GameClear
    {
        get { return m_IsGameClear; }
        set { m_IsGameClear = value; }
    }

    public void OnRestart(InputValue input)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}