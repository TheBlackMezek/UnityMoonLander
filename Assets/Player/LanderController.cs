using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanderController : MonoBehaviour {

    [Range(0, 1)]
    public int defaultCamControls = 0;

    public float gravityForce = 1.0f;
    public float thrustrForce = 5.0f;
    public float rotationAmt = 1.0f;

    public float maxFuel = 100.0f;
    public float thrustFuelCost = 1.0f;
    public float crashSpeed = 10.0f;

    public LevelHighScore highScoreScript;
    public Rigidbody body;
    public GameObject thrustParticles;
    public GameObject explodeParticles;
    public Image fuelBar;
    public Image speedBar;
    public Color speedColorGood;
    public Color speedColorBad;
    public GameObject gameOverPanel;
    public Text crashSuccessText;
    public Text scoreText;
    public LanderCamera cam;


    private float fuel;
    private float prevVel = 0;
    private int safeLegs = 0;
    private LandingPad currentPad = null;
    private int useCamRelativeControls;




    private void Start()
    {
        useCamRelativeControls = PlayerPrefs.GetInt("UseCamRelativeControls", defaultCamControls);
        fuel = maxFuel;
        Physics.gravity = new Vector3(0, -gravityForce, 0);
    }

    private void FixedUpdate()
    {
        Vector3 rotVec = Vector3.zero;
        Vector3 rotMod = cam.camRot.normalized;
        
        rotVec += new Vector3(0, Input.GetAxis("Yaw"), 0);
        rotVec += new Vector3(Input.GetAxis("Pitch"), 0, 0);
        rotVec += new Vector3(0, 0, Input.GetAxis("Roll"));

        

        if(rotVec != Vector3.zero)
        {
            if(useCamRelativeControls > 0)
            {
                rotVec = cam.transform.TransformVector(rotVec);
            }

            transform.Rotate(rotVec);
        }



        float thrusterInput = Input.GetAxis("Thruster");

        if (thrusterInput > 0 && fuel != 0)
        {
            Vector3 forceVec = Vector3.zero;
            
            forceVec += transform.up * thrusterInput * thrustrForce * Time.fixedDeltaTime;
            thrustParticles.SetActive(true);

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
            thrustParticles.SetActive(false);
        }

        prevVel = body.velocity.magnitude;

        speedBar.fillAmount = body.velocity.magnitude / crashSpeed;
        if(speedBar.fillAmount == 1)
        {
            speedBar.color = speedColorBad;
        }
        else
        {
            speedBar.color = speedColorGood;
        }



        if (safeLegs == 4)
        {
            GameOver("Successful landing", (int)(fuel + currentPad.landingScore), false);
        }
        safeLegs = 0;

        if(transform.position.y < 0)
        {
            GameOver("Left landing area", 0, false);
        }
    }

    public void OnLegCollision(Collider other)
    {
        LandingPad pad = other.gameObject.GetComponent<LandingPad>();
        
        if (prevVel >= crashSpeed || !pad)
        {
            GameOver("Crashed", 0, true);
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
            GameOver("Crashed", 0, true);
        }
    }

    private void GameOver(string cstext, int score, bool crashed)
    {
        string scoretext = "Score: " + score;

        gameOverPanel.SetActive(true);
        crashSuccessText.text = cstext;
        scoreText.text = scoretext;
        thrustParticles.SetActive(false);

        if(crashed)
        {
            explodeParticles.SetActive(true);
            explodeParticles.transform.parent = null;

            Collider[] colliders = GetComponentsInChildren<Collider>();
            foreach (Collider c in colliders)
            {
                c.isTrigger = false;
                c.transform.parent = null;

                if(!c.gameObject.GetComponent<Rigidbody>())
                {
                    c.gameObject.AddComponent<Rigidbody>();
                }
            }
        }
        else
        {
            Destroy(body);
        }

        highScoreScript.AddHighScore(score);
        highScoreScript.UpdateScoreboard();
        highScoreScript.SaveScores();

        this.enabled = false;
    }



    public float GetFuel()
    {
        return fuel;
    }

}
