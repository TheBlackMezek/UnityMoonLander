using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanderLegs : MonoBehaviour {

    public LanderController lander;


    

    

    private void OnTriggerStay(Collider other)
    {
        lander.OnLegCollision(other);
    }

}
