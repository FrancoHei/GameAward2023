using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    private bool m_IsBlackOut  = false;
    private bool m_IsGameOver  = false;
    private bool m_IsGameClear = false;
    private bool m_CanInput    = true;

    public List<GameObject> targetList;

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

    public bool CanInput 
    {
        get { return m_CanInput; }
        set { m_CanInput = value; }
    }

    public void OnRestart(InputValue input)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    private void Update()
    {
        if (m_IsGameOver) 
        {
            SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
        }
        if (m_IsGameClear) 
        {
            SceneManager.LoadScene("GameClearScene", LoadSceneMode.Single);
        }
    }
}