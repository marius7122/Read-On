using UnityEngine;
using TMPro;

public class bookCardInstantiator : MonoBehaviour {

    public GameObject bookCardPrefab;


	void Start ()
    {
        string path = Application.persistentDataPath + "/path.txt";

        string[] paths = System.IO.File.ReadAllLines(path);

        foreach(string p in paths)
        {
            string title = PlayerPrefs.GetString(p + "-title");

            Debug.Log(title);

            GameObject g = Instantiate(bookCardPrefab, gameObject.transform);   //instantiate as child
            g.GetComponent<bookCardScript>().path = p;                          //set path
            g.GetComponentInChildren<TextMeshProUGUI>().text = title;           //set title
        }

    }
    
    public void goToMainMenu()
    {
        Application.LoadLevel(2);
    }
}
