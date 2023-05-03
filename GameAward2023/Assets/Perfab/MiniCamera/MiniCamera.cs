using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCamera : MonoBehaviour
{
    private GameObject target;
    private Vector3    targetCameraVector;

    void Start()
    {
        if (target)
            targetCameraVector = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
            transform.position = target.transform.position + targetCameraVector;
    }
    public void SetTargetObject(GameObject tgt)
    {
        target = tgt;
        this.Start();
    }
}
