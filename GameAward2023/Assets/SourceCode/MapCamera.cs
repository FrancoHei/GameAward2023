using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    private GameObject target;
    private Vector3    targetCameraVector;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        targetCameraVector = transform.position - target.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + targetCameraVector;
    }
}
