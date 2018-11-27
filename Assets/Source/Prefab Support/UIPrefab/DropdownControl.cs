using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle

public partial class DropdownControl : MonoBehaviour {

    // reference to all UI elements in the Canvas
    public Dropdown mCreateMenu = null;
    public MainController MainController = null;
    
    // Use this for initialization
    void Start() {
        Debug.Assert(mCreateMenu != null);
        Debug.Assert(MainController != null);
        mCreateMenu.onValueChanged.AddListener(UserSelection);
    }

    String[] kLoadType = {"Mesh", "Cylinder"};
    void UserSelection(int index) 
    {
        // inform the world of user's action
        MainController.ProcessUserSelection(kLoadType[index]);
    }
}
