using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiStuff : MonoBehaviour
{

    Text text;
    int currentHeath = 3;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }
    public void updateText(int amount)
    {
        currentHeath = currentHeath + amount;
        text.text = "lives: " + currentHeath;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
