using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameara : MonoBehaviour
{
    private GameObject player;
    private Vector3 offset;
    public float cam = 1.0f;

    public Vector3 cameraOffset;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        offset = transform.position - player.transform.position;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position + cameraOffset, player.transform.position + offset, cam * Time.deltaTime);
    }
}
