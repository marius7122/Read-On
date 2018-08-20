using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    //objects, assign in editor
    public TextMeshProUGUI centerText;  //assign from editor
    public Camera camera;               //camera
    public progressControl progCtrl;    //for showing what percentage is read and changing color
    public speedSliderControler slc;    //for changing color
    public Image nightModeImg;          //for toggle night/day mode
    public Button[] buttons;          //plus, minus, pause, play, rewind buttons


    public string mode = "day";     //default mode is day
    //night mode colors
    public Color nightBackground;
    public Color nightText;
    //day mode colors
    public Color dayBackground;
    public Color dayText;

    public Reader testReader;           //reader for testing purpose
    public Reader currentReder;         //used reader

    public int readSpeed = 220;     //read speed in WPS
    public float displayTime;       //display time for a word

    public float nextWordTime;          //when current word will be displayed

    public bool pause = false;
     


	void Start ()
    {
        //initialize testReader
        testReader = new Reader(sampleText.text1);

        currentReder = testReader;

        displayTime = 60f / readSpeed;

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
            nextWordTime = Time.time + displayTime;
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
}
