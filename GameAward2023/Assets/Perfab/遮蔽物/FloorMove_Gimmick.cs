using UnityEngine;

public class FloorMove_Gimmick : MonoBehaviour
{
    private PlayerState m_PS;

    GameObject up;
    
    private float Y;

    private float moveup = 0.0f;
    private float movedown = 0.0f;


    [Header("移動速度")]
    public float move_assignment = 0.01f;

    [Header("どのくらいの距離移動させたいか")]
    public float maximum;

    private bool m_Up = false;
    private bool m_Down = false;

    private Vector3 posdown;

    public bool m_IsDown = false;


    // Start is called before the first frame update
    void Start()
    {
        m_PS = GameObject.Find("Player").GetComponent<PlayerState>();

 //       down = GameObject.Find("FllorMoveUp");

        //現在の位置を取得
        posdown = this.gameObject.transform.position;

        Y = posdown.y;
        movedown = Y;
        if (!m_IsDown) 
        {
            maximum = Y - maximum;
        }else 
        {
            maximum = Y + maximum;
        }
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        //現在の位置を取得
        posdown = this.gameObject.transform.position;

        {
            if (!m_IsDown)
            {
                if (posdown.y < maximum)
                {
                    return;
                }
            }
            else
            {
                if (posdown.y > maximum)
                {
                    return;
                }
            }
        }
        // Debug.Log(movedown);

        if (m_PS.Target == true)
        {
           // Debug.Log("宝を持っている");
            if (m_Down == true)
            {
                if (!m_IsDown) 
                {
                    movedown -= move_assignment;
                    this.gameObject.transform.position = new Vector3(posdown.x, posdown.y - move_assignment, posdown.z);
                }else 
                {
                    movedown += move_assignment;
                    this.gameObject.transform.position = new Vector3(posdown.x, posdown.y + move_assignment, posdown.z);
                }
              

      
            
            
            }
        }
        else
        {
         //   Debug.Log("宝を持っていない");
        }

            if (m_Down == false)
            {
            if (posdown.y < Y)
            {
                if (!m_IsDown)
                {
                    movedown += move_assignment;
                    this.gameObject.transform.position = new Vector3(posdown.x, posdown.y + move_assignment, posdown.z);
                }else 
                {
                    movedown -= move_assignment;
                    this.gameObject.transform.position = new Vector3(posdown.x, posdown.y - move_assignment, posdown.z);
                }
     
            }
            }



        if (m_PS.Target == false)
        {
            m_Down = false;
        }



    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (m_PS.Target == true)
    //    {
      
    //        if (collision.gameObject.name == "FloorMoveDown")
    //        {
    //            Debug.Log("宝持ってるし、リフトにも乗っている");
    //            m_Down = true;
              
    //        }
    //        else
    //        {
    //            Debug.Log("宝持ってるけど、リフトには乗っていない");
    //            m_Down = false;
    //        }



    //    }

    //    else
    //    {
 
    //        m_Down = false;
    //    }



    //}


    void OnCollisionStay2D(Collision2D collision)
    {
        if (m_PS.Target == true)
        {

            if (collision.gameObject.tag == "Player" && CheckPlayer())
            {
                m_Down = true;
                Debug.Log("宝を持っている");
            }
            else
            {
                m_Down = false;
                Debug.Log("宝を持っていない");
            }
        }
        else
        {
            m_Down = false;
        }
    }

    private bool CheckPlayer()
    {
        LayerMask mask = LayerMask.GetMask("Player");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1.5f, mask);

        if (hit && (hit.transform.gameObject.tag == "Player" ))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

}
