using UnityEngine;
using System.IO;

public class mainMenu : MonoBehaviour {

	public void read()
    {
        Application.LoadLevel(2);       //select scene
    }

    public void open()
    {
        Application.LoadLevel(3);       //scene with pdf open
    }

    public void dailyRead()
    {
        PlayerPrefs.SetString("current_path", "daily_reading");
        Application.LoadLevel(1);       //reading scene
    }

    public void info()
    {
        //not ready  :(
    }

    public void close()
    {
        Application.Quit();
    }

    public void resetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();

        File.WriteAllText(Application.persistentDataPath + "/path.txt", string.Empty);
    }

    public void logPersistentPath()
    {
        Debug.Log(Application.persistentDataPath);
    }
}
