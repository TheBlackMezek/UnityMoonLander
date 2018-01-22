using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanderController : MonoBehaviour {

    public float gravityForce = 1.0f;
    public float thrustrForce = 5.0f;
    public float rotationAmt = 1.0f;

    public float maxFuel = 100.0f;
    public float thrustFuelCost = 1.0f;

    public Rigidbody body;
    public GameObject particles;
    public Image fuelBar;


    private float fuel;




    private void Start()
    {
        fuel = maxFuel;
    }

    private void FixedUpdate()
    {
        Vector3 rotVec = Vector3.zero;

        if(Input.GetKey(KeyCode.Q))
        {
            rotVec += new Vector3(0, -rotationAmt, 0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotVec += new Vector3(0, rotationAmt, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            rotVec += new Vector3(rotationAmt, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rotVec += new Vector3(-rotationAmt, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rotVec += new Vector3(0, 0, rotationAmt);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotVec += new Vector3(0, 0, -rotationAmt);
        }

        transform.Rotate(rotVec);



        Vector3 forceVec = Vector3.down * gravityForce * Time.fixedDeltaTime;
        if(Input.GetKey(KeyCode.Space) && fuel != 0)
        {
            forceVec += transform.up * thrustrForce * Time.fixedDeltaTime;
            particles.SetActive(true);

            fuel -= thrustFuelCost * Time.fixedDeltaTime;
            if(fuel < 0)
            {
                fuel = 0;
            }

            fuelBar.fillAmount = fuel / maxFuel;
        }
        else
        {
            particles.SetActive(false);
        }

        body.AddForce(forceVec);
    }



    public float GetFuel()
    {
        return fuel;
    }

}
