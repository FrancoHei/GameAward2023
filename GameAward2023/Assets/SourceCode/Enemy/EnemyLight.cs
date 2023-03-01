using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.Find("Player"))
        {
            if (gameObject.transform.parent.GetComponent<EnemyState>().State == EnemyState.EnemyAiState.VIGILANCE)
            {
                gameObject.transform.parent.GetComponent<EnemyState>().State = EnemyState.EnemyAiState.DISCOVER;
            }
        }
    }
}
