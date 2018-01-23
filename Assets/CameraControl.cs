using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public bool isCameraRelative = true;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
