using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float frames;

    public Vector3 cameraOffset;

    public Transform PlayerTransform;
    void Update()
    {
        transform.position = Vector3.Lerp(PlayerTransform.position, transform.position, frames) + cameraOffset;
    }
}
