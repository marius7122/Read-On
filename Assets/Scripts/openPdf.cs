using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;


public class openPdf : MonoBehaviour
{

    public string pathFileString;

    public string path;
    public string title;
    public int startFromPage = 1;
    public string startFromPageStr = "1";

    public TextMeshProUGUI err;



    void Start()
    {
        pathFileString = Application.persistentDataPath + "/path.txt";

        path = EditorUtility.OpenFilePanel("Alege fisierul PDF", "", "pdf");
        while (path == "")
            path = EditorUtility.OpenFilePanel("Alege un PDF valid", "", "pdf");

        //if path is first time added
        if(PlayerPrefs.GetString(path+"-title").Length == 0)
            TextController.appendToFile(pathFileString, path);
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
        if (!int.TryParse(startFromPageStr, out startFromPage))
        {
            err.text = "Pagina de inceput trebuie sa fie un numar";
            return;
        }

        if(startFromPage < 1)
        {
            err.text = "Prima pagina este numerotata cu 1";
            return;
        }

        iTextSharp.text.pdf.PdfReader pdfReader = new iTextSharp.text.pdf.PdfReader(path);
        if (pdfReader.NumberOfPages < startFromPage)
        {
            err.text = "Fisierul PDF are numai " + pdfReader.NumberOfPages + " pagini";
            return;
        }

        PlayerPrefs.SetString(path + "-title", title);
        PlayerPrefs.SetInt(path + "-startPage", startFromPage);
        PlayerPrefs.SetString("current_path", path);

        Application.LoadLevel(1);   //load reading scene
    }
}
