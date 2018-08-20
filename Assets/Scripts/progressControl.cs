using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class progressControl : MonoBehaviour
{
    RawImage progressBar;
    TextMeshProUGUI progressText;


    void Start()
    {
        progressBar = transform.GetChild(0).GetComponent<RawImage>();
        progressText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void updateProgress(float percentage)
    {
        progressBar.rectTransform.localScale = new Vector2(percentage / 100f, 1);
        progressText.text = "Ai parcurs " + (int)percentage + "%";
    }

    public void changeColor(Color color)
    {
        progressBar.color = color;
        progressText.color = color;
    }
}