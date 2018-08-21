using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenu : MonoBehaviour {

	public void read()
    {

    }

    public void open()
    {
        Application.LoadLevel(0);       //scene with pdf open
    }

    public void dailyRead()
    {
        PlayerPrefs.SetString("current_path", "daily_reading");
        Application.LoadLevel(1);       //reading scene
    }

    public void info()
    {
        //nothing for now :(
    }

    public void close()
    {
        Application.Quit();
    }
}
