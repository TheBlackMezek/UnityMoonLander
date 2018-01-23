using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DeleteSavesButton : MonoBehaviour {

    public string[] levels;
    public Text text;

    private bool firstClick = false;
    private bool deleted = false;

    public void DeleteSaveData()
    {
        if(!firstClick)
        {
            text.text = "Are you sure?";
            firstClick = true;
        }
        else if(!deleted)
        {
            foreach (string s in levels)
            {
                File.Delete(Application.persistentDataPath + "/" + s + ".save");
                text.text = "Deleted";
            }
            deleted = true;
        }
    }

}
