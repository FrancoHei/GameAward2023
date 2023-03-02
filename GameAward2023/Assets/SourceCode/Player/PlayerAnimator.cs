using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerState m_PS;
    // Start is called before the first frame update
    void Start()
    {
        m_PS = GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
