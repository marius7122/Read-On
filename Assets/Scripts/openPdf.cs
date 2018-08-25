using UnityEngine;
using UnityEditor;
using TMPro;
using SFB;

public class openPdf : MonoBehaviour
{

    public string pathFileString;

    public string path;
    public string title;
    public int startFromPage = 1;
    public string startFromPageStr = "1";

    public TextMeshProUGUI err;

    public TMP_InputField titleInput;
    public TextMeshProUGUI pathText;



    void Start()
    {
        pathFileString = Application.persistentDataPath + "/path.txt";


        Debug.Log("searching for data from dialog window");

        title = PlayerPrefs.GetString("tempTitle");
        path = PlayerPrefs.GetString("dialogWindowPath");

        Debug.Log("data found: title=" + title + ";path=" + path);

        titleInput.text = title;
        pathText.text = path;
    }

    public void setTitle(string s)
    {
        title = s;
    }
    public void setPage(string s)
    {
        startFromPageStr = s;
    }

    public void read()
    {
        //check data
        if (title.Length < 2)
        {
            err.text = "Titlul este prea scurt";
            return;
        }

        if (path.Length == 0)
        {
            err.text = "Alegeti un fisier PDF";
            return;
        }


        //if path is first time added
        if (!TextController.searchInFile(pathFileString, path))
        {
            TextController.appendToFile(pathFileString, path);
        }


        PlayerPrefs.SetString(path + "-title", title);
        PlayerPrefs.SetInt(path + "-startPage", startFromPage);
        PlayerPrefs.SetString("current_path", path);

        PlayerPrefs.SetString("tempTitle", "");
        PlayerPrefs.SetString("dialogWindowPath", "");

        Application.LoadLevel(1);   //load reading scene
    }

    public void goToMainMenu()
    {
        PlayerPrefs.SetString("tempTitle", "");
        Application.LoadLevel(0);
    }

    public void openFileDialog()
    {
        PlayerPrefs.SetString("tempTitle", title);

        PlayerPrefs.SetInt("returnToScene", Application.loadedLevel);
        Application.LoadLevel(4);   //file dialog window
    }
}
