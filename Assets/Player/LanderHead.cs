using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanderHead : MonoBehaviour {

    public LanderController lander;



    private void OnTriggerEnter(Collider other)
    {
        lander.OnHeadCollision(other);
    }

}
