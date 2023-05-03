using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniCameraManager : MonoBehaviour
{

    public GameObject target;
    private GameObject player;

    public float MaxDistance = 25.0f;

    public MiniCamera MiniCamera;
    public MiniCameraUI MiniUI;

    //おたからを投げるタイミングでこれを読んでくれ 
    public void setTarget(GameObject tgt)
    {
        Debug.Log("set target");
        target = tgt;
        MiniCamera.SetTargetObject(tgt);
        MiniUI.SetTargetObject(tgt);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            float distance = Vector3.Distance(target.transform.position, player.transform.position);
            MiniUI.gameObject.GetComponent<RawImage>().color = new Color(1, 1, 1, 1);
           
            if (distance > MaxDistance)
            {
                target = null;
                MiniCamera.SetTargetObject(null);
                MiniUI.SetTargetObject(null);
            }
           
        }
        else
        {
            MiniUI.gameObject.GetComponent<RawImage>().color = new Color(1,1,1,0);
        }
    }


    public void HideCamera()
    {
        target = null;
        MiniCamera.SetTargetObject(null);
        MiniUI.SetTargetObject(null);
    }
}
