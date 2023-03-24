using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowCamera : MonoBehaviour
{
    private GameObject target;
    private Vector3    targetCameraVector;
    private Vector2    cameraOffset;
    private Vector2    cameraMovement;

    public float       cameraSpeed;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        targetCameraVector = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        transform.position = target.transform.position + targetCameraVector + new Vector3(cameraOffset.x, cameraOffset.y, 0.0f);
        cameraOffset += cameraMovement * cameraSpeed;
    }

    public void OnCameraMove(InputValue input)
    {
        Vector2 value = input.Get<Vector2>();
        if (value == Vector2.zero)
            cameraOffset = Vector2.zero;
        cameraMovement = value;
    }
}
