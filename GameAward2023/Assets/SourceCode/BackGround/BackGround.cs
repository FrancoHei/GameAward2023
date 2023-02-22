using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    private GameObject target;
    private Vector3 targetCameraVector;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Main Camera");
        targetCameraVector = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        transform.position = target.transform.position + targetCameraVector;
    }
}
