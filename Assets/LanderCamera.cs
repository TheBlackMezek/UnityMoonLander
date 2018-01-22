using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanderCamera : MonoBehaviour {

    public float cameraRotationSpeed = 1.0f;
    public float cameraDist = 5.0f;
    public float cameraZoomSpeed = 1.0f;

    public float maxCamZoom = 10.0f;
    public float minCamZoom = 1.0f;
    public float maxCamAngleX = 90.0f;
    public float minCamAngleX = 0.0f;

    public Transform lander;
    public Camera myCamera;


    private Vector3 camRot = Vector3.zero;


    private void Update()
    {
        transform.position = lander.position;

        if (Input.GetKey(KeyCode.Mouse1))
        {
            camRot += new Vector3(-Input.GetAxis("Mouse Y") * cameraRotationSpeed,
                                   Input.GetAxis("Mouse X") * cameraRotationSpeed,
                                   0);
            
            camRot.x = Mathf.Clamp(camRot.x, minCamAngleX, maxCamAngleX);
        }

        cameraDist += -Input.GetAxis("Mouse ScrollWheel") * cameraZoomSpeed;
        cameraDist = Mathf.Clamp(cameraDist, minCamZoom, maxCamZoom);

        transform.eulerAngles = camRot;
        transform.position += -transform.forward * cameraDist;
    }

}
