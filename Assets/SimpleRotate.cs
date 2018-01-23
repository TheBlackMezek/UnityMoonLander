using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour {

    public Vector3 velocity;

    private void Update()
    {
        transform.Rotate(velocity * Time.deltaTime);
    }

}
