using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    private bool m_IsBlackOut = false;
    private bool m_IsLoadGameOverScene = false;
    private bool m_IsLoadGameClearScene = false;

    private bool m_IsGameOver = false;
    private bool m_IsGameClear = false;
    private bool m_CanInput = true;

    public List<GameObject> m_CheckPoint;
    public GameObject m_SpwanPoint;

    private static List<bool> m_CheckPointClear;

    public bool IsLoadGameOverScene
    {
        get { return m_IsLoadGameOverScene; }
        set { m_IsLoadGameOverScene = value; }
    }

    public bool IsLoadGameClearScene
    {
        get { return m_IsLoadGameClearScene; }
        set { m_IsLoadGameClearScene = value; }
    }

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

    private DepthOfField dof;
    private void Awake()
    {
        
        int i = 0;
        if(m_CheckPointClear == null) 
        {
            m_CheckPointClear = new List<bool>(new bool[m_CheckPoint.Count]);
            Instantiate(m_SpwanPoint, m_CheckPoint[0].transform.GetChild(0).position + m_CheckPoint[0].transform.GetChild(0).localPosition, Quaternion.identity);
            return;
        }

        for (int a = 0; a < m_CheckPointClear.Count; a++)
        {
            m_CheckPoint[i].transform.GetChild(1).GetComponent<CheckPoint>().Clear = m_CheckPointClear[i];
        }

        for (i = 0; i < m_CheckPointClear.Count; i++)
        {
            if (!m_CheckPointClear[i]) break;
            if (i != 0 && !m_CheckPointClear[i]) break;
        }
        Instantiate(m_SpwanPoint, m_CheckPoint[i - 1].transform.GetChild(0).position + m_CheckPoint[i - 1].transform.GetChild(0).localPosition, Quaternion.identity);

        //Debug.Log(m_CheckPoint[0].transform.GetChild(0).position + m_CheckPoint[0].transform.GetChild(0).localPosition);
    }

    private void Update()
    {
        for (int i = 0; i < m_CheckPoint.Count; i++)
        {
            m_CheckPointClear[i] = m_CheckPoint[i].transform.GetChild(1).GetComponent<CheckPoint>().Clear;
        }

        if (m_IsGameOver)
        {
            
            if (!m_IsLoadGameOverScene) 
            {
                GameObject.Find("Player").GetComponent<PlayerMovement>().InitJump();
                m_CanInput = false;
                GameObject.Find("BlackScreen").SetActive(false);
                GameObject.Find("Volumn").GetComponent<Volume>().profile.TryGet(out dof);
                dof.active = true;
                SceneManager.LoadScene("GameOverScene", LoadSceneMode.Additive);
                m_IsLoadGameOverScene = true;
            }
        }

        if (m_IsGameClear) 
        {
            if (!m_IsLoadGameClearScene)
            {
                GameObject.Find("Player").GetComponent<PlayerMovement>().InitJump();
                m_CanInput = false;
                GameObject.Find("BlackScreen").SetActive(false);
                GameObject.Find("Volumn").GetComponent<Volume>().profile.TryGet(out dof);
                dof.active = true;
                SceneManager.LoadScene("GameClearScene", LoadSceneMode.Additive);
                m_IsLoadGameClearScene = true;
            }
        }
    }
}