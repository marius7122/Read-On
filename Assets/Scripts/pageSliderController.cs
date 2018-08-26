using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class pageSliderController : MonoBehaviour{

    string      text;
    string[]    words;
    int         txtIndex;
    public int[]       pageIndex;
    public int         nrOfPages;
    int         currPage;
    int         selectedWordIndex = -1;
    int         prevWordIndex = -1;
    int         lastValidWordIndex = 0;

    public Color32 selectedColor;
    public Color32 black;

    //asign in editor
    public Slider           pageSlider;  
    public TextMeshProUGUI  pageNumber;
    public TextMeshProUGUI  pageContent;

    public GameObject debugPoint;

    bool saveButtonPressed = false;

    void Start()
    {
        txtIndex = PlayerPrefs.GetInt("txtIndex");

        readTxts();

        pageSlider.minValue = 1;
        pageSlider.maxValue = nrOfPages;
        currPage = 1;
        pageSlider.value = 1f;
        loadPage(1);
    }

    void LateUpdate()
    {
        //for android
        if(Input.touchCount > 0)
        {
            Debug.Log("touch");
            press(Input.GetTouch(0).position);
        }

        //for windows
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("mouse down");

            press(Input.mousePosition);
        }
    }

    void press(Vector3 position)
    {
        selectedWordIndex = TMP_TextUtilities.FindIntersectingWord(pageContent, position, Camera.main);
        
        if(selectedWordIndex != -1 && !saveButtonPressed)
        {
            Debug.Log("curr word index : " + selectedWordIndex + " word : " + words[pageIndex[(int)pageSlider.value] + selectedWordIndex]);

            lastValidWordIndex = selectedWordIndex;

            colorWord(selectedWordIndex, selectedColor);

            if (prevWordIndex != -1 && selectedWordIndex != prevWordIndex)
                colorWord(prevWordIndex, black);

            prevWordIndex = selectedWordIndex;
        }
    }

    void colorWord(int wordIndex, Color32 color)
    {
        TMP_WordInfo info = pageContent.textInfo.wordInfo[wordIndex];

        for (int i = 0; i < info.characterCount; ++i)
        {
            int charIndex = info.firstCharacterIndex + i;
            int meshIndex = pageContent.textInfo.characterInfo[charIndex].materialReferenceIndex;
            int vertexIndex = pageContent.textInfo.characterInfo[charIndex].vertexIndex;

            Color32[] vertexColors = pageContent.textInfo.meshInfo[meshIndex].colors32;
            vertexColors[vertexIndex + 0] = color;
            vertexColors[vertexIndex + 1] = color;
            vertexColors[vertexIndex + 2] = color;
            vertexColors[vertexIndex + 3] = color;
        }

        pageContent.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
        
    }

    void readTxts()
    {
        readWords();
        readIndex();
    }

    void readWords()
    {
        string path = Application.persistentDataPath + "/" + txtIndex.ToString() + ".txt";

        text = File.ReadAllText(path);

        words = text.Split(' ');
    }

    void readIndex()
    {
        string path = Application.persistentDataPath + "/" + txtIndex.ToString() + "-pages" + ".txt";

        string aux = File.ReadAllText(path);
        string[] nums = aux.Split(' ');

        nrOfPages = nums.Length - 1;

        pageIndex = new int[nums.Length + 2];

        for (int i = 0; i < nums.Length; ++i)
        {
            if (nums[i] == "")
                continue;

            //Debug.Log(i + " -> " + nums[i]);
            pageIndex[i + 2] = int.Parse(nums[i]);
        }

        //Debug.Log("after parsing");
    }


    public void prevPage()
    {
        if (currPage == 1)
            return;

        --currPage;

        pageSlider.value = (float)currPage;
    }

    public void nextPage()
    {
        if (currPage == nrOfPages)
            return;

        ++currPage;

        pageSlider.value = (float)currPage;
    }

    //change page after some time, for smoother page slider
    public void changePageWrapper(float page)
    {
        currPage = (int)page;

        pageNumber.text = "Pagina " + currPage.ToString();

        StopAllCoroutines();
        StartCoroutine(loadPageCoroutine(currPage));
    }

    IEnumerator loadPageCoroutine(int page)
    {
        yield return new WaitForSeconds(0.15f);
        loadPage(page);
    }

    void loadPage(int page)
    {
        string pageText = "";
        for (int i = pageIndex[page]; i < pageIndex[page + 1]; ++i)
        {
            pageText += (words[i] + " ");
        }

        pageContent.text = pageText;
    }

    public void goBack()
    {
        int prevScene = PlayerPrefs.GetInt("returnToScene");
        Application.LoadLevel(prevScene);
    }

    public void saveWordIndex()
    {
        saveButtonPressed = true;

        int wordIndex = lastValidWordIndex + pageIndex[(int)pageSlider.value];

        Debug.Log("saving " + wordIndex + " as word index");
        Debug.Log("also, " + lastValidWordIndex);
        Debug.Log("word: " + words[lastValidWordIndex]);

        if (wordIndex != -1)
        {
            PlayerPrefs.SetInt("selectedWord", wordIndex);

            string path = PlayerPrefs.GetString("path");
            PlayerPrefs.SetInt(path + "-lastIndex", wordIndex);
        }
    }
}
