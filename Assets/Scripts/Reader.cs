using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reader
{
    public string text;
    public string[] words;
    int currWordIndex;              //index of current word
    const int rewindWords = 5;      //how many words to go back in rewind function


    //parse text in words
    void parseText()
    {
        words = text.Split(' ');

        /*List<string> _words = new List<string>();
        string _word = "";
        string punctuationMarks = ".?!;:\"";
        bool lastIsMark = false;

        foreach(char c in text)
        {
            if (c == ' ')
            {
                lastIsMark = false;

                if (_word != "")
                {
                    _words.Add(_word);
                    _word = "";
                }

                continue;
            }

            _word += c;

            //c is a punctuation mark
            if (punctuationMarks.IndexOf(c) != -1)
                lastIsMark = true;
            
            //c is letter or digit
            if((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
            {
                if(lastIsMark)
                {
                    _words.Add(_word);
                    _word = "";
                    lastIsMark = false;
                }

                _word += c;
            }
            Debug.Log(_words);
            Debug.Log(_word);
        }*/

    }


    /*  PUBLIC FUNCTIONS */

    //constructors
    public Reader()
    {
        text = "";
        currWordIndex = 0;
    }
    public Reader(string _text)
    {
        text = _text;
        currWordIndex = 0;
        parseText();
    }

    //get next word
    public string getNextWord()
    {
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
