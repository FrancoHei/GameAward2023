using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CautionText : MonoBehaviour
{
    [Header("éÂêl")]
    public GameObject m_Owner;
    [Header("éÂêlÇ∆ãóó£")]
    public Vector3    m_OffsetPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_Owner.transform.position + m_OffsetPosition;

        if (m_Owner.transform.Find("Body")) 
        {
            transform.position = m_Owner.transform.Find("Body").position + m_OffsetPosition;
        }

        if (m_Owner.GetComponent<State_Enemy>())
        {
            if (m_Owner.GetComponent<State_Enemy>().State == State_Enemy.EnemyAiState.MOVE)
            {
                transform.Find("Canvas").Find("Text").GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                transform.Find("Canvas").Find("Text").GetComponent<TextMeshProUGUI>().text = "í èÌ";
            }
            else
           if (m_Owner.GetComponent<State_Enemy>().State == State_Enemy.EnemyAiState.CHASE)
            {
                transform.Find("Canvas").Find("Text").GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                transform.Find("Canvas").Find("Text").GetComponent<TextMeshProUGUI>().text = "åxâ˙";
            }else
           if (m_Owner.GetComponent<State_Enemy>().State == State_Enemy.EnemyAiState.ATTACK)
            {
                transform.Find("Canvas").Find("Text").GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                transform.Find("Canvas").Find("Text").GetComponent<TextMeshProUGUI>().text = "çUåÇ";
            }
        }
        else
        if (m_Owner.GetComponent<EnemyState>())
        {
            if (m_Owner.GetComponent<EnemyState>().State == EnemyState.EnemyAiState.NORMAL || m_Owner.GetComponent<EnemyState>().State == EnemyState.EnemyAiState.RETURNTOSTARTPOINT)
            {
                transform.Find("Canvas").Find("Text").GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                transform.Find("Canvas").Find("Text").GetComponent<TextMeshProUGUI>().text = "í èÌ";
            }
            else
            if (m_Owner.GetComponent<EnemyState>().State == EnemyState.EnemyAiState.VIGILANCE)
            {
                transform.Find("Canvas").Find("Text").GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                transform.Find("Canvas").Find("Text").GetComponent<TextMeshProUGUI>().text = "åxâ˙";
            }
            else
            if (m_Owner.GetComponent<EnemyState>().State == EnemyState.EnemyAiState.DISCOVER)
            {
                transform.Find("Canvas").Find("Text").GetComponent<TextMeshProUGUI>().text = "î≠å©";
            }
        }
    }
}
