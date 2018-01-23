using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPause : MonoBehaviour {

    private void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }
	}

}
