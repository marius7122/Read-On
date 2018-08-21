using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class TextController : MonoBehaviour {

    //objects, assign in editor
    public TextMeshProUGUI centerText;  //assign from editor
    public Camera camera;               //camera
    public progressControl progCtrl;    //for showing what percentage is read and changing color
    public speedSliderControler slc;    //for changing color
    public Image nightModeImg;          //for toggle night/day mode
    public Button[] buttons;            //plus, minus, pause/play, rewind buttons, settings
    public Image ppImg;                   //play/pause button image

    public Sprite playImg;
    public Sprite pauseImg;


    public string mode = "day";     //default mode is day
    //night mode colors
    public Color nightBackground;
    public Color nightText;
    //day mode colors
    public Color dayBackground;
    public Color dayText;

    public Reader currentReder;         //reader

    public int readSpeed = 220;     //read speed in WPS
    public float displayTime;       //display time for a word
    public float nextWordTime;      //when current word will be displayed

    public bool pause = true;

    public string pathFileString;       //file with path to all pdf
    

	void Start ()
    {
        pathFileString = Application.persistentDataPath + "/path.txt";

        displayTime = 60f / readSpeed;

        string current_path = PlayerPrefs.GetString("current_path");

        if(current_path == "example" || current_path == "")
        {
            currentReder = new Reader(sampleText.text2, "example");
        }
        else if(current_path == "daily_reading")
        {
            currentReder = new Reader(dailyReads.todayRead(), "daily read");
        }
        else
        {
            int startPage = PlayerPrefs.GetInt(current_path + "-startPage");
            currentReder = new Reader(current_path, startPage);
        }

        //display first word
        displayNextWord();
    }

    void Update ()
    {
        //display words 
        if (!pause && Time.time >= nextWordTime)
            displayNextWord();
        
        //rewind
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rewind();
            Debug.Log("rewind");
        }
        
        //pause
        if (Input.GetKeyDown(KeyCode.Space))
        {
            togglePause();
            Debug.Log("pause/play");
        }

        //update progress bar
        updateProgressBar();
    }

    //display next word
    void displayNextWord()
    {
        centerText.text = currentReder.getNextWord();
        nextWordTime = Time.time + displayTime;
    }

    //update progress bar
    void updateProgressBar()
    {
        progCtrl.updateProgress(currentReder.percentageRead());
    }

    //rewind
    public void rewind()
    {
        currentReder.rewind();
    }

    //toggle pause
    public void togglePause()
    {
        pause = !pause;

        if (!pause)
        {
            nextWordTime = Time.time + displayTime;
            ppImg.sprite = pauseImg;
        }
        else
            ppImg.sprite = playImg;
    }

    //this will be called from speed slider
    public void updateSpeed(float speed)
    {
        readSpeed = (int)speed * 10;
        displayTime = 60f / readSpeed;
    }

    //change color of scene
    public void changeColor(Color text, Color background)
    {
        camera.backgroundColor = background;
        centerText.color = text;
        progCtrl.changeColor(text);
        slc.changeColor(text, background);
        nightModeImg.color = text;

        foreach (Button btn in buttons) 
        {
            ColorBlock cb = btn.colors;
            cb.normalColor = text;
            btn.colors = cb;
        }
    }

    //toggle night/day mode
    public void toggleNightMode()
    {
        if(mode == "day")
        {
            mode = "night";
            changeColor(nightText, nightBackground);
        }

        else if(mode == "night")
        {
            mode = "day";
            changeColor(dayText, dayBackground);
        }
    }

    public static void appendToFile(string path, string text)
    {
        if (!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {
            }
        }


        using (StreamWriter sw = File.AppendText(path))
        {
            sw.WriteLine(text);
        }
    }

    //return true if found
    public static bool searchInFile(string path, string line)
    {
        if(!File.Exists(path))
            return false;

        string[] lines = File.ReadAllLines(path);

        foreach(string _line in lines)
        {
            if (line == _line)
                return true;
        }

        return false;
    }

    public void goToMainMenu()
    {
        Application.LoadLevel(0);   //load main menu
    }
  
}
