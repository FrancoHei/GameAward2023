using System.Collections;
using System.Collections.Generic;

using System.Runtime.CompilerServices;
using System.Security.Permissions;
using UnityEngine;

public class Door_Gimmick : MonoBehaviour
{
    private PlayerState m_PS;

    private switch_Gimmick m_SG;

    public GameObject door;

    private float move = 0.0f;

    public float move_assignment = 0.01f;

    bool Door = true;

    bool Close = false;



    // Start is called before the first frame update
    void Start()
    {
        m_PS = GameObject.Find("Player").GetComponent<PlayerState>();

        m_SG = GetComponent<switch_Gimmick>();

        Door = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_SG.m_SW == true)
        {
            Close = true;
        }

        if (Close == true)
        {
            //Œ»Ý‚ÌˆÊ’u‚ðŽæ“¾
            Vector3 pos = door.gameObject.transform.position;

            if (Door == true)
            {
                move += move_assignment * 10;
                door.gameObject.transform.position = new Vector3(pos.x, pos.y + (move_assignment * 10), pos.z);
                Debug.Log("“ü‚Á‚½");
            }

            if (Door == false)
            {
                move -= move_assignment;
                door.gameObject.transform.position = new Vector3(pos.x, pos.y - move_assignment, pos.z);
            }




            if (move >= 5.0f)
            {
                Door = false;
            }

            if (move <= 0.0f)
            {
                move = 0.0f;
                Door = true;
                Close = false;
            }

            Debug.Log(move);
            //   Debug.Log("‚±‚ê‚Í‡‚Á‚Ä‚¢‚é");

        }
    }

}
