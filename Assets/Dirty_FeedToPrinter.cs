using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Dirty_FeedToPrinter : MonoBehaviour
{
    public TextData data;
    public GameObject header;
    public TextMeshProUGUI headerName;
    public DialogPrinter printer;
    public GameObject slime_object;
    

    void Start()
    {
        if (data.character != null)
        {
            header.SetActive(true);
            headerName.text = data.character.name;

            if (data.character.slime)
            {
                slime_object.SetActive(true);
                slime_object.GetComponent<RawImage>().color = data.character.slimeColour;
            }
        }
        printer.PrintText(data.content);
    }

    
}
