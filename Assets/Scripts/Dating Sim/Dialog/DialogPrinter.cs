using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogPrinter : MonoBehaviour
{
    public string printableText;
    public float standardDelay = 0.5f;
    public TextMeshProUGUI mainTextBox;
    public TextMeshProUGUI choiceOneTextBox;
    public TextMeshProUGUI choiceTwoTextBox;

    private Coroutine lastRoutine = null;

    public int charIndex;

    public void SetDialogPiece(string text)
    {
        printableText = text;
    }

    public void PrintText(string text)
    {
        lastRoutine = StartCoroutine(PrintOverTime(text, standardDelay));
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

    public void StopPrinting()
    {
        StopCoroutine(lastRoutine);
        charIndex = 0;
        mainTextBox.text = "";
    }

    public void PrintStandard(string text, string choice)
    {
        switch (choice)
        {
            case "A":
                choiceOneTextBox.text = text;
                break;
            case "B":
                choiceTwoTextBox.text = text;
                break;
        }
    }

    private IEnumerator PrintOverTime(string text, float delay)
    {
        while (charIndex < text.Length)
        {
            mainTextBox.text += text[charIndex];
            yield return new WaitForSeconds(delay);

            charIndex++;
            print("running");
        }


        Debug.Log("END");
    }

    private void Start()
    {
        Debug.Log("START");
        charIndex = 0;
        mainTextBox.text = "";
        Print();
    }
}