using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class speedSliderControler : MonoBehaviour {

    Slider slider;
    TextMeshProUGUI text;
    TextMeshProUGUI infoText;

    //assigned in editor
    public Image sliderBackground;
    public Image sliderFill;


    void Start()
    {
        slider = transform.GetChild(0).GetComponent<Slider>();
        text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }


    //set slider value (speed /  10)
    public void setSpeed(float speed)
    {
        slider.value = speed;
        text.text = (slider.value * 10).ToString() + " cuvinte pe minut";
    }

    //increase speed by 10 WPM (words per minute)
    public void increaseSpeed()
    {
        int newSpeed = getSpeed() + 1;

        //you can't read at more than 1000WPM
        if (newSpeed > 100)
            newSpeed = 100;

        setSpeed(newSpeed);
    }

    //decrease speed by 10 WPM
    public void decreaseSpeed()
    {
        int newSpeed = getSpeed() - 1;

        //you can't read at less than 100 WPM
        if (newSpeed < 10)
            newSpeed = 10;

        setSpeed(newSpeed);
    }

    //get slider value
    public int getSpeed()
    {
        return (int)slider.value;
    }

    //change slider color
    public void changeColor(Color textColor, Color background)
    {
        sliderBackground.color = background;
        sliderFill.color = textColor;
        text.color = textColor;
    }

}
