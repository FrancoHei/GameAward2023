using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStartPoint : MonoBehaviour
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
        if (collision.gameObject == gameObject.transform.parent.Find("Body"))
        {
            if (collision.gameObject.GetComponent<EnemyState>().State == EnemyState.EnemyAiState.RETURNTOSTARTPOINT)
            {
                collision.gameObject.GetComponent<EnemyState>().State = EnemyState.EnemyAiState.NORMAL;
            }
        }
    }
}
