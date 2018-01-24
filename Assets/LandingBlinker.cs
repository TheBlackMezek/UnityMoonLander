using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingBlinker : MonoBehaviour {

    public float blinkInterval = 1.0f;

    public Material offMaterial;
    public Material onMaterial;

    public bool startOn = false;

    public Renderer renderr;
    public Light pointlight;

    private float timer = 0;
    private bool on = false;



    private void Start()
    {
        if (startOn)
        {
            renderr.material = onMaterial;
            pointlight.color = onMaterial.color;
        }
        else
        {
            renderr.material = offMaterial;
            pointlight.color = offMaterial.color;
        }
        on = startOn;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= blinkInterval)
        {
            timer = 0;
            
            if (!on)
            {
                pointlight.color = onMaterial.color;
                renderr.material = onMaterial;
            }
            else
            {
                renderr.material = offMaterial;
                pointlight.color = offMaterial.color;
            }
            on = !on;
        }
    }

}
