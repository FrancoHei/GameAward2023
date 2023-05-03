using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GameObject.Find("MiniCamera_UI").transform.position;
        this.GetComponent<Image>().color = GameObject.Find("MiniCamera_UI").GetComponent<Image>().color;
    }
}
