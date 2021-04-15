using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    void Start() {
        Input.backButtonLeavesApp = true;
    }

    void Update()
    {    
        // Check if Back was pressed this frame
        if (Input.GetKeyDown(KeyCode.Escape)) {
            // Quit the application
            Application.Quit();
        }
    }
}
