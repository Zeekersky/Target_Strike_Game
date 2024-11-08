using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    public Camera TopViewCamera;
    public Camera FirstPersonCamera;
    public Camera ThirdPersonCamera;

    public void ShowTopViewCamera()
    {
        TopViewCamera.enabled = true;
        FirstPersonCamera.enabled = false;
        ThirdPersonCamera.enabled = false;
    }

    public void ShowFirstPersonCamera()
    {
        TopViewCamera.enabled = false;
        FirstPersonCamera.enabled = true;
        ThirdPersonCamera.enabled = false;
    }

    public void ShowThirdPersonCamera()
    {
        TopViewCamera.enabled = false;
        FirstPersonCamera.enabled = false;
        ThirdPersonCamera.enabled = true;
    }

    void Start()
    {
        ShowThirdPersonCamera();
    }

    void Update()
    {
        if (Input.GetKeyDown("[1]"))
        {
            ShowFirstPersonCamera();
        }
        else if (Input.GetKeyDown("[2]"))
        {
            ShowThirdPersonCamera();
        }
        else if (Input.GetKeyDown("[3]"))
        {
            ShowTopViewCamera();
        }
    }
}
