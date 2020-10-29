using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour
{
    public Camera currentCamera;

    void LateUpdate()
    {
        currentCamera = Camera.main;
        transform.LookAt(transform.position + currentCamera.transform.forward);
    }
}