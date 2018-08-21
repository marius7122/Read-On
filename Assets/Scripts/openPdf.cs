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



    void Start()
    {

        pathFileString = Application.persistentDataPath + "/path.txt";

        Debug.Log(pathFileString);

        var extensions = new[] {
            new SFB.ExtensionFilter("Text", "pdf"),
        };
        string path = StandaloneFileBrowser.OpenFilePanel("Deschideti fisierul PDF dorit", "", extensions, true)[0];

        Debug.Log(path);

        //not working for build
        /*path = EditorUtility.OpenFilePanel("Alege fisierul PDF", "", "pdf");
        while (path == "")
            path = EditorUtility.OpenFilePanel("Alege un PDF valid", "", "pdf");*/
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

        //if path is first time added
        if (PlayerPrefs.GetString(path + "-title").Length == 0)
            TextController.appendToFile(pathFileString, path);

        PlayerPrefs.SetString(path + "-title", title);
        PlayerPrefs.SetInt(path + "-startPage", startFromPage);
        PlayerPrefs.SetString("current_path", path);

        Application.LoadLevel(1);   //load reading scene
    }

    public void goToMainMenu()
    {
        Application.LoadLevel(2);
    }
}
