using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogPrinter : MonoBehaviour
{
    public string printableText;
    public float standardDelay = 0.5f;
    public TextMeshProUGUI textBox;

    private int charIndex;
    public void SetDialogPiece(string text)
    {
        printableText = text;
    }

    public void PrintText(string text)
    {
        StartCoroutine(PrintOverTime(text, standardDelay));
    }

    public void PrintText(string text, float customDelay)
    {
        StartCoroutine(PrintOverTime(text, customDelay));
    }

    public void Print()
    {
        StartCoroutine(PrintOverTime(printableText, standardDelay));
    }
    public void Print(float customDelay)
    {
        StartCoroutine(PrintOverTime(printableText, customDelay));
    }


    private IEnumerator PrintOverTime(string text,float delay)
    {
        while(charIndex < text.Length){


            textBox.text += text[charIndex];
            yield return new WaitForSeconds(delay);

            charIndex++;
        }
     
        
         Debug.Log("END");
        
    }

    private void Start()
    {
        Debug.Log("START");
        charIndex = 0;
        textBox.text = "";
        Print();
    }
}
