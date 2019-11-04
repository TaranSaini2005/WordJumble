using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour
{
    Text text; 

   // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = "The correct answer was: " + DataControl.dataControl.correctWord;
    }
}
