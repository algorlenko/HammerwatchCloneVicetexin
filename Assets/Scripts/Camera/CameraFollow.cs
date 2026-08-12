using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f,0f,-5f);

    [SerializeField] private float cameraMovementSpeed = 5f;
    private Vector3 currentVelocity;
    [SerializeField] private float smoothTime = 0.1f;
    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = targetPosition;
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime); // If I want damping back, more specifically, for the charachter to be offset from the center while moving so that its more clear what direction he's going. You may need to change to fixed update for this.
    }
}
