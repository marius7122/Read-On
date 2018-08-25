using UnityEngine;
using TMPro;

public class folderCardController : MonoBehaviour {

    string title;
    string path;

    public fileDialogCardInstatniator fd;

    //initialize
    public void init(string _path, string _title, fileDialogCardInstatniator _fd)
    {
        path = _path;
        title = _title;
        fd = _fd;

        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = title;
    }

    //load selected path
    public void pressed()
    {
        fd.loadPath(path);
    }

}
