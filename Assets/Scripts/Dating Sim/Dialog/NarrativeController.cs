using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeController : MonoBehaviour
{
    [SerializeField] private List<CutSceneManager> _cutscenes;

    public GameObject mainDisplay;
    public GameObject choiceOne;
    public GameObject choiceTwo;
    public GameObject slimeGameObject;
    public DialogPrinter dialogPrinter;
    public TextMeshProUGUI header;
    [SerializeField] private Image background;
    
    private bool _slime;
    private int _cutsceneIndex = 0;
    private int _characterDataIndex = 0;
    private int _dialogueIndex = 0;
    private int _dialogueSize = 0;
    private int _multipleChoiceIndex = 0;

    private int _progression = 0;
    private bool _isChoosing;

    private void Start()
    {
        background.sprite = _cutscenes[_cutsceneIndex].background;
        _slime = _cutscenes[_cutsceneIndex].characterData[_characterDataIndex].slime;
        header.text = _cutscenes[_cutsceneIndex].characterData[_characterDataIndex].name;
        dialogPrinter.PrintText(_cutscenes[_cutsceneIndex].textData[_dialogueIndex].content);
        _dialogueSize = _cutscenes[_cutsceneIndex].textData.Length;
        _dialogueIndex++;
    }

    private void Update()
    {
        SlimeEnabled();

        MultipleChoice();

        PlayerInput();
    }

    private void SlimeEnabled()
    {
        slimeGameObject.SetActive(_slime);
        slimeGameObject.GetComponent<RawImage>().color = _cutscenes[_cutsceneIndex].characterData[_characterDataIndex].slimeColour;
    }

    private void MultipleChoice()
    {
        if (_isChoosing)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                mainDisplay.SetActive(true);
                choiceOne.SetActive(false);
                choiceTwo.SetActive(false);
                _cutsceneIndex = 1;
                _dialogueIndex = 0;
                _multipleChoiceIndex = 0;
                _characterDataIndex = 1;
                _progression = -1;
                _isChoosing = false;
                background.sprite = _cutscenes[_cutsceneIndex].background;
                dialogPrinter.PrintText(_cutscenes[_cutsceneIndex].textData[_dialogueIndex].content);
                _dialogueSize = _cutscenes[_cutsceneIndex].textData.Length;
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                mainDisplay.SetActive(true);
                choiceOne.SetActive(false);
                choiceTwo.SetActive(false);
                _cutsceneIndex = 2;
                _dialogueIndex = 0;
                _multipleChoiceIndex = 0;
                _characterDataIndex = 1;
                _progression = -1;
                _isChoosing = false;
                background.sprite = _cutscenes[_cutsceneIndex].background;
                dialogPrinter.PrintText(_cutscenes[_cutsceneIndex].textData[_dialogueIndex].content);
                _dialogueSize = _cutscenes[_cutsceneIndex].textData.Length;
            }
        }
    }

    private void PlayerInput()
    {
        if (Input.anyKeyDown && !_isChoosing)
        {
            _progression++;

            //multiple choice One stage
            if (_progression == _dialogueSize)
            {
                mainDisplay.SetActive(false);
                choiceOne.SetActive(true);
                choiceTwo.SetActive(true);
                dialogPrinter.StopPrinting();
                dialogPrinter.PrintStandard(_cutscenes[_cutsceneIndex].multipleChoice[_multipleChoiceIndex].content, "A");
                _multipleChoiceIndex++;
                dialogPrinter.PrintStandard(_cutscenes[_cutsceneIndex].multipleChoice[_multipleChoiceIndex].content, "B");

                _isChoosing = true;
            }
            else
            {
                dialogPrinter.StopPrinting();
                if (_characterDataIndex == 0)
                {
                    _characterDataIndex++;
                }
                else
                {
                    _characterDataIndex--;
                }

                _slime = _cutscenes[_cutsceneIndex].characterData[_characterDataIndex].slime;
                header.text = _cutscenes[_cutsceneIndex].characterData[_characterDataIndex].name;
                dialogPrinter.PrintText(_cutscenes[_cutsceneIndex].textData[_dialogueIndex].content);
                _dialogueIndex++;
            }
        }
    }
}