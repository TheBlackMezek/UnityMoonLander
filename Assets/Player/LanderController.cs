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
    public float crashSpeed = 10.0f;

    public Rigidbody body;
    public GameObject particles;
    public Image fuelBar;
    public GameObject gameOverPanel;
    public Text crashSuccessText;
    public Text scoreText;


    private float fuel;
    private float prevVel = 0;
    private int safeLegs = 0;
    private LandingPad currentPad = null;

    private bool shouldRotate = false;




    private void Start()
    {
        fuel = maxFuel;
        Physics.gravity = new Vector3(0, -gravityForce, 0);
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

        if(rotVec != Vector3.zero)
        {
            transform.Rotate(rotVec);
        }


        
        if(Input.GetKey(KeyCode.Space) && fuel != 0)
        {
            Vector3 forceVec = Vector3.zero;
            
            forceVec += transform.up * thrustrForce * Time.fixedDeltaTime;
            particles.SetActive(true);

            fuel -= thrustFuelCost * Time.fixedDeltaTime;
            if(fuel < 0)
            {
                fuel = 0;
            }

            fuelBar.fillAmount = fuel / maxFuel;

            body.AddForce(forceVec);
        }
        else
        {
            particles.SetActive(false);
        }

        prevVel = body.velocity.magnitude;

        
        if(safeLegs == 4)
        {
            GameOver("Successful landing", "Score: " + (int)(fuel + currentPad.landingScore));
        }
        safeLegs = 0;

        if(transform.position.y < 0)
        {
            GameOver("Left landing area", "Score: 0");
        }
    }

    public void OnLegCollision(Collider other)
    {
        LandingPad pad = other.gameObject.GetComponent<LandingPad>();
        
        if (prevVel >= crashSpeed || !pad)
        {
            GameOver("Crashed", "Score: 0");
        }
        else
        {
            safeLegs += 1;
            currentPad = pad;
        }
    }

    public void OnHeadCollision(Collider other)
    {
        if(other.transform.parent != transform)
        {
            GameOver("Crashed", "Score: 0");
        }
    }

    private void GameOver(string cstext, string scoretext)
    {
        gameOverPanel.SetActive(true);
        crashSuccessText.text = cstext;
        scoreText.text = scoretext;
        this.enabled = false;
        Destroy(body);
        particles.SetActive(false);
    }



    public float GetFuel()
    {
        return fuel;
    }

}
