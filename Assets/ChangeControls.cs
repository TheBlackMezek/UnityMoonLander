using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeControls : MonoBehaviour {

    public Toggle toggle;

    private void Start()
    {
        int i = PlayerPrefs.GetInt("UseCamRelativeControls");
        if(i > 0)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
    }

    public void ToggleCamRelativeControls()
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("UseCamRelativeControls", 1);
        }
        else
        {
            PlayerPrefs.SetInt("UseCamRelativeControls", 0);
        }
    }

}
