using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenConfig : MonoBehaviour {

    private void Awake()
    {
        Screen.SetResolution(564, 960, false);
    }
}
