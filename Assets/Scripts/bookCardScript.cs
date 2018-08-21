using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bookCardScript : MonoBehaviour {

    public string path;

    public void goRead()
    {
        PlayerPrefs.SetString("current_path", path);
        Application.LoadLevel(1);       //reading scene
    }
}
