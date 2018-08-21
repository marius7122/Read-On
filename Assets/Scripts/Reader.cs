using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEditor;

[System.Serializable]
public class Reader
{
    string path;                            //path to file
    public string text;
    public string[] words;
    int currWordIndex;                     //index of current word
    int startPage = 1;                     //start from that page

    static int rewindWords = 5;      //how many words to go back in rewind function


    bool isCharacter(char c)
    {
        if ("şŞţŢâÂăĂîÎ".IndexOf(c) != -1)
            return true;

        return ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'));
    }

    //parse text in words
    void parseText()
    {
        //words = text.Split(' ');

        List<string> _words = new List<string>();
        string _word = "";
        string punctuationMarks = ".?!;:\"\n";
        //bool lastIsMark = false;

        char lastC = '\0';

        foreach(char c in text)
        {
            if (c == ' ' || c == '\n')
            {
                if (_word != "")
                {
                    _words.Add(_word);
                    _word = "";
                }

                continue;
            }

            _word += c;

            if (_word.Length > 1 && isCharacter(c) && punctuationMarks.IndexOf(lastC) != -1)
            {
                _words.Add(_word);
                _word = "";
            }


            lastC = c;
        }

        words = _words.ToArray(); 
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
    }
    public Reader(string _path, int _startPage)
    {
        path = _path;
        startPage = _startPage;

        openPdf(path);
    }

    public void openPdf(string path)
    {
        iTextSharp.text.pdf.PdfReader pdfReader = new iTextSharp.text.pdf.PdfReader(path);
        StringBuilder sb = new StringBuilder();

        for(int i=startPage; i <= pdfReader.NumberOfPages; ++i)
        {
            sb.Append(iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(pdfReader, i));
        }

        text = sb.ToString();
        parseText();
    }

    //get next word
    public string getNextWord()
    {
        PlayerPrefs.SetInt(path + "-lastIndex", currWordIndex);

        if (currWordIndex < words.Length)
            return words[currWordIndex++];
        else
            return "Ai terminat de citit!";     //replace in relase
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

}
