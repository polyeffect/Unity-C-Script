using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEvent : MonoBehaviour
{
    private string keyPrint = " key pressed.";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            keyPrintEvent("space");
        }

        if (Input.GetKey("escape"))
        {
            keyPrintEvent("ESC");
            Application.Quit();
        }
    }

    void keyPrintEvent(string key)
    {
        print("space" + keyPrint);
    }
}
