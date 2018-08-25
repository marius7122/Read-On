using UnityEngine;
using System.IO;

public class fileDialogCardInstatniator : MonoBehaviour
{

    //to be asigned in editor
    public GameObject folderCard;
    public GameObject fileCard;

    public int level = 0;
    public string path = "";

    void Start()
    {
        loadMain();
    }

    public void loadPath(string newPath)
    {
        if (newPath.Length > path.Length)
            ++level;
        else
            --level;

        path = newPath;

        if (level == 0)
        {
            loadMain();
        }
        else
        {
            clearCards();

            createFolderCard(lastFolder(path), "..");

            string[] dirs = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path, "*.pdf");

            foreach (string dir in dirs)
            {
                createFolderCard(dir, dirName(dir));
            }
            foreach (string file in files)
            {
                createFileCard(file, fileName(file));
            }
        }
    }

    public void returnPath(string path)
    {
        PlayerPrefs.SetString("dialogWindowPath", path);
        int returnToScene = PlayerPrefs.GetInt("returnToScene");

        Application.LoadLevel(returnToScene);
    }

    public void returnFail()
    {
        returnPath("");
    }

    void loadMain()
    {
        path = "";

        clearCards();

        //for windows
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            createFolderCard("C:\\", "C:\\");
            createFolderCard("D:\\", "D:\\");
        }

        //for android
        if (Application.platform == RuntimePlatform.Android)
        {
            createFolderCard("mnt/sdcard/", "Memorie Interna");
            createFolderCard("mnt/ext_sdcard/", "SD Card");
        }
    }

    void createFolderCard(string path, string title)
    {
        //instantiate as child
        GameObject obj = Instantiate(folderCard, gameObject.transform) as GameObject;

        obj.GetComponent<folderCardController>().init(path, title, this);
    }
    void createFileCard(string path, string title)
    {
        GameObject obj = Instantiate(fileCard, gameObject.transform) as GameObject;

        obj.GetComponent<fileCardController>().init(path, title, this);
    }

    void clearCards()
    {
        foreach (Transform child in this.transform)
            Destroy(child.gameObject);
    }

    //extract directory name from path
    string dirName(string path)
    {
        //if path is just drive
        if (path.Length <= 3)
            return path;

        return "/" + fileName(path);
    }
    //extract file name from path
    string fileName(string path)
    {
        string name = "";

        int i = path.Length - 1;
        if (path[i] == '/' || path[i] == '\\')      //if path ends with / or \
            --i;

        for (; i >= 0; --i)
        {
            if (path[i] == '/' || path[i] == '\\')
                break;
            name += path[i];
        }

        name = reverse(name);

        return name;
    }

    //one folder back
    string lastFolder(string path)
    {
        //we are in drive
        if (path.Length <= 3)
            return "";

        string lastPath = "";
        int lastDelimiter = 0;

        for (int i = 0; i < path.Length - 1; ++i)
            if (path[i] == '/' || path[i] == '\\')
                lastDelimiter = i;

        for (int i = 0; i <= lastDelimiter; ++i)
            lastPath += path[i];
        

        Debug.Log("last of " + path + " is " + lastPath);

        return lastPath;
    }

    //reverse a string
    string reverse(string str)
    {
        char[] strArr = str.ToCharArray();
        str = "";

        for (int i = strArr.Length - 1; i >= 0; --i)
            str += strArr[i];

        return str;
    }

}
