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
    public GameObject[] choiceOption;
    public GameObject slimeGameObject;
    public DialogPrinter dialogPrinter;
    public TextMeshProUGUI header;
    [SerializeField] private Image background;

    private int textDataSize;
    private int multipleChoiceSize;

    private bool _slime;
    private int _cutsceneIndex = 0;
    private int _characterDataIndex = 0;
    private int _dialogueIndex = 0;
    private int _multipleChoiceIndex = 0;

    private int _progression = 0;
    private bool _isChoosing;

    private void Start()
    {
        textDataSize = _cutscenes[_cutsceneIndex].textData.Length;
        multipleChoiceSize = _cutscenes[_cutsceneIndex].multipleChoice.Length;
        background.sprite = _cutscenes[_cutsceneIndex].background;
        _slime = _cutscenes[_cutsceneIndex].characterData[_characterDataIndex].slime;
        header.text = _cutscenes[_cutsceneIndex].characterData[_characterDataIndex].name;
        dialogPrinter.PrintText(_cutscenes[_cutsceneIndex].textData[_dialogueIndex].content);
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
        slimeGameObject.GetComponent<RawImage>().color =
            _cutscenes[_cutsceneIndex].characterData[_characterDataIndex].slimeColour;
    }

    private void MultipleChoice()
    {
        if (_isChoosing)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                mainDisplay.SetActive(true);
                for (int i = 0; i <= multipleChoiceSize; i++)
                {
                    choiceOption[i].SetActive(false);
                }

                _cutsceneIndex = _cutscenes[_cutsceneIndex].cutManagerLink[0];
                _dialogueIndex = 0;
                _multipleChoiceIndex = 0;
                _characterDataIndex = 1;
                _progression = -1;
                _isChoosing = false;
                background.sprite = _cutscenes[_cutsceneIndex].background;
                dialogPrinter.PrintText(_cutscenes[_cutsceneIndex].textData[_dialogueIndex].content);
                textDataSize = _cutscenes[_cutsceneIndex].textData.Length;
                multipleChoiceSize = _cutscenes[_cutsceneIndex].multipleChoice.Length;
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                mainDisplay.SetActive(true);
                for (int i = 0; i <= multipleChoiceSize; i++)
                {
                    choiceOption[i].SetActive(false);
                }

                _cutsceneIndex = _cutscenes[_cutsceneIndex].cutManagerLink[1];
                _dialogueIndex = 0;
                _multipleChoiceIndex = 0;
                _characterDataIndex = 1;
                _progression = -1;
                _isChoosing = false;
                background.sprite = _cutscenes[_cutsceneIndex].background;
                dialogPrinter.PrintText(_cutscenes[_cutsceneIndex].textData[_dialogueIndex].content);
                textDataSize = _cutscenes[_cutsceneIndex].textData.Length;
                multipleChoiceSize = _cutscenes[_cutsceneIndex].multipleChoice.Length;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                mainDisplay.SetActive(true);
                for (int i = 0; i <= multipleChoiceSize; i++)
                {
                    choiceOption[i].SetActive(false);
                }

                _cutsceneIndex = _cutscenes[_cutsceneIndex].cutManagerLink[2];
                _dialogueIndex = 0;
                _multipleChoiceIndex = 0;
                _characterDataIndex = 1;
                _progression = -1;
                _isChoosing = false;
                background.sprite = _cutscenes[_cutsceneIndex].background;
                dialogPrinter.PrintText(_cutscenes[_cutsceneIndex].textData[_dialogueIndex].content);
                textDataSize = _cutscenes[_cutsceneIndex].textData.Length;
                multipleChoiceSize = _cutscenes[_cutsceneIndex].multipleChoice.Length;
            }
        }
    }

    private void PlayerInput()
    {
        if (Input.anyKeyDown && !_isChoosing)
        {
            _progression++;

            if (_progression == textDataSize)
            {
                mainDisplay.SetActive(false);
                for (int i = 0; i < multipleChoiceSize; i++)
                {
                    choiceOption[i].SetActive(true);
                }

                dialogPrinter.StopPrinting();

                switch (multipleChoiceSize)
                {
                    case 1:
                        dialogPrinter.PrintStandard(
                            _cutscenes[_cutsceneIndex].multipleChoice[_multipleChoiceIndex].content, "A");
                        break;
                    case 2:
                        dialogPrinter.PrintStandard(
                            _cutscenes[_cutsceneIndex].multipleChoice[_multipleChoiceIndex].content, "A");
                        _multipleChoiceIndex++;
                        dialogPrinter.PrintStandard(
                            _cutscenes[_cutsceneIndex].multipleChoice[_multipleChoiceIndex].content, "B");
                        break;
                    case 3:
                        dialogPrinter.PrintStandard(
                            _cutscenes[_cutsceneIndex].multipleChoice[_multipleChoiceIndex].content, "A");
                        _multipleChoiceIndex++;
                        dialogPrinter.PrintStandard(
                            _cutscenes[_cutsceneIndex].multipleChoice[_multipleChoiceIndex].content, "B");
                        _multipleChoiceIndex++;
                        dialogPrinter.PrintStandard(
                            _cutscenes[_cutsceneIndex].multipleChoice[_multipleChoiceIndex].content, "C");
                        break;
                }

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