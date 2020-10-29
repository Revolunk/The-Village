using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ThirdPersonCamera : MonoBehaviour
{
    public Transform playerBody;
    public Transform cameraRotator;

    public float mouseSensitivity;
    public bool isInverted;

    private float xRotation;

    public float minClampAngle;
    public float maxClampAngle;

    public float zoomSpeed;
    public float currentDistance;
    public float savedDistance;
    public float minDistance;
    public float maxDistance;
    public float currentHeight;
    public float minHeight;
    public float maxHeight;

    private Vector3 dollyDirection;

    void Awake()
    {
        dollyDirection = transform.localPosition.normalized;
        savedDistance = currentDistance;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.cyan);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        playerBody.Rotate(Vector3.up * mouseX);

        if (isInverted == true)
        {
            xRotation += mouseY;
        }
        else
        {
            xRotation -= mouseY;
        }

        cameraRotator.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        xRotation = Mathf.Clamp(xRotation, minClampAngle, maxClampAngle);

        transform.position = cameraRotator.transform.position + cameraRotator.forward * -currentDistance;
        currentDistance += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        savedDistance += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        Vector3 desiredDistance = transform.parent.TransformPoint(dollyDirection * currentDistance);

        RaycastHit hit;
        if (Physics.Linecast(transform.parent.position, desiredDistance, out hit))
        {
            currentDistance = Mathf.Clamp(hit.distance, 0, maxDistance);
        }
        else
        {
            savedDistance = Mathf.Clamp(savedDistance, minDistance, maxDistance);
            currentDistance = savedDistance;
        }

        transform.position = transform.position + Vector3.up * currentHeight;
        currentHeight = Mathf.Clamp(currentHeight, minHeight, maxHeight);
    }
}
