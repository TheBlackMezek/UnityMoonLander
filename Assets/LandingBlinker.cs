using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingBlinker : MonoBehaviour {

    public float blinkInterval = 1.0f;

    public Material offMaterial;
    public Material onMaterial;

    public bool startOn = false;

    public Renderer renderr;

    private float timer = 0;
    private bool on = false;



    private void Start()
    {
        if (startOn)
        {
            renderr.material = onMaterial;
        }
        else
        {
            renderr.material = offMaterial;
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
                renderr.material = onMaterial;
            }
            else
            {
                renderr.material = offMaterial;
            }
            on = !on;
        }
    }

}
