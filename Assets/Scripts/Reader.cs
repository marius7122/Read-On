using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

[System.Serializable]
public class Reader
{
    string path;                            //path to file
    public string text;
    public string[] words;
    List<string> words_l = new List<string>();
    int wordsNr = 0;                        //words number
    int currWordIndex;                      //index of current word
    // int startPage = 1;                      //start from that page
    int[] wordsTillPage;                    //wordsTillPage[i] = how many words were up to page i
    int wordsWithPoint = 0;

    static int rewindWords = 5;             //how many words to go back in rewind function


    bool isCharacter(char c)
    {
        if ("şŞţŢâÂăĂîÎ".IndexOf(c) != -1)
            return true;

        return ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'));
    }

    bool containLetters(string s)
    {
        foreach(char c in s)
        {
            if (isCharacter(c))
                return true;
        }
        return false;
    }

    //parse text in words
    void parseText()
    {
        string _word = "";
        string punctuationMarks = ".?!;:\"\n";

        char lastC = '\0';

        foreach(char c in text)
        {
            if (c == ' ' || c == '\n')
            {
                if (_word != "")
                {
                    if (containLetters(_word))
                    {
                        words_l.Add(_word);
                        ++wordsNr;
                    }
                    _word = "";
                }

                continue;
            }

            _word += c;

            if (_word.Length > 1 && isCharacter(c) && punctuationMarks.IndexOf(lastC) != -1)
            {
                if (containLetters(_word))
                {
                    Debug.Log("ok");
                    words_l.Add(_word);
                    ++wordsNr;
                }
                _word = "";
            }

            lastC = c;
        }

        if (_word.Length > 0)
        {
            if (containLetters(_word))
            {
                Debug.Log("ok");
                words_l.Add(_word);
                ++wordsNr;
            }
            _word = "";
        }
    }

    //convert list of words in array and delete the list
    void putWordsInArray()
    {
        words = words_l.ToArray();

        //count words that end with point
        foreach(string word in words)
        {
            if(word.Contains("."))
                ++wordsWithPoint;
        }

        words_l.Clear();
    }

    void putWordsInTxt()
    {
        int txtIndex = PlayerPrefs.GetInt("curr_txt_index");
        string path = Application.persistentDataPath + "/" + txtIndex.ToString() + ".txt";

        using (StreamWriter sw = File.CreateText(path))
        {
            foreach(string word in words)
            {
                sw.Write(word + " ");
            }
        }

        putPageIndexInTxt(txtIndex);

        PlayerPrefs.SetInt(path + "-index", txtIndex);
        PlayerPrefs.SetInt("curr_txt_index", txtIndex + 1);
    }

    void putPageIndexInTxt(int txtIndex)
    {
        string path = Application.persistentDataPath + "/" + txtIndex.ToString() + "-pages" + ".txt";

        using (StreamWriter sw = File.CreateText(path))
        {
            for(int i = 2; i < wordsTillPage.Length; ++i)
            {
                sw.Write(wordsTillPage[i].ToString() + " ");
            }
        }
    }


    /*  PUBLIC FUNCTIONS */

    //constructors
    public Reader()
    {
        text = "";
        currWordIndex = 0;
    }
    public Reader(string _text, string description)
    {
        text = _text;

        if(description == "example")
            currWordIndex = 0;
        else if(description == "daily read")
        {
            path = System.DateTime.Now.ToString("yyyy/MM/dd") + " read";
            currWordIndex = PlayerPrefs.GetInt(path + "-lastIndex");
        }

        parseText();
        putWordsInArray();
    }
    public Reader(string _path)
    {
        path = _path;
        if (PlayerPrefs.GetInt(path + "-wasAdded") == 0)
        {
            currWordIndex = 0;
            openPdf(path);
            PlayerPrefs.SetInt(path + "-wasAdded", 1);
        }
        else
        {
            currWordIndex = PlayerPrefs.GetInt(path + "-lastIndex");

            string txtPath = Application.persistentDataPath + "/" + PlayerPrefs.GetInt(path + "-index").ToString() + ".txt";

            openTxt(txtPath);
        }
    }

    //open and parse pdf
    public void openPdf(string path)
    {
        Debug.Log("in open pdf");

        iTextSharp.text.pdf.PdfReader pdfReader = new iTextSharp.text.pdf.PdfReader(path);
        StringBuilder sb = new StringBuilder();

        wordsTillPage = new int[pdfReader.NumberOfPages + 2];

        for (int i=1; i <= pdfReader.NumberOfPages; ++i)
        {
            text = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(pdfReader, i);
            parseText();
            wordsTillPage[i + 1] = wordsNr;
        }

        putWordsInArray();
        putWordsInTxt();
    }

    //open and parse txt
    public void openTxt(string path)
    {
        Debug.Log("open txt " + path);

        text = File.ReadAllText(path);

        words = text.Split(' ');
    }

    //get next word
    public string getNextWord()
    {
        PlayerPrefs.SetInt(path + "-lastIndex", currWordIndex);

        if (currWordIndex < words.Length)
            return words[currWordIndex++];
        else
            return "Ai terminat de citit!";
    }

    //go back some words
    public void rewind()
    {
        currWordIndex -= 5;

        if (currWordIndex < 0)
            currWordIndex = 0;
    }

    //return the percentage of text we finished reading as integer
    public float percentageRead()
    {
        return (currWordIndex + 1f) / words.Length * 100;
    }

    public int getFileIndex()
    {
        return PlayerPrefs.GetInt(path + "-index");
    }
    public string getPath()
    {
        return path;
    }
    public int getWordsWithPoint()
    {
        return wordsWithPoint;
    }
    public int wordsNumber()
    {
        return words.Length;
    }
}
