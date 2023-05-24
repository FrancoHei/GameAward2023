using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BulletUseEnemyAI2 : MonoBehaviour
{
    private Rigidbody2D rb2D;
    [SerializeField] private GameObject LightObject;
    public GameObject[] m_MovePoint;



    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        switch (GetComponent<EnemyState>().State)
        {
            case EnemyState.EnemyAiState.NORMAL:
                NormalState();
                break;
            case EnemyState.EnemyAiState.VIGILANCE:
                VigilanceState();
                break;
            case EnemyState.EnemyAiState.DISCOVER:
                Discover();
                break;
            case EnemyState.EnemyAiState.RETURNTOSTARTPOINT:
                GetComponent<EnemyState>().State = EnemyState.EnemyAiState.NORMAL;
                break;
            default:
                break;
        }
    }

    void NormalState()
    {
        LightObject.GetComponent<Light2D>().intensity = 0;
    }

    void VigilanceState()
    {
    }

    void Discover()
    {

    }
}
