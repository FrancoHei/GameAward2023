using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlArea : MonoBehaviour
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
            gameObject.transform.parent.Find("Body").GetComponent<EnemyState>().State = EnemyState.EnemyAiState.VIGILANCE;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.Find("Player"))
        {
            gameObject.transform.parent.Find("Body").GetComponent<EnemyState>().State = EnemyState.EnemyAiState.RETURNTOSTARTPOINT;
        }
    }
}
