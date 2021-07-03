using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeController : MonoBehaviour
{
    public CutSceneManager.Cutscene[] _cutscenes;
    
    public GameObject mainDisplay;
    public GameObject[] choiceOption;
    public GameObject slimeGameObject;
    public DialogPrinter dialogPrinter;
    public TextMeshProUGUI header;
    [SerializeField] private Image background;
    private CharacterData cd;

    private int textDataSize;
    private int multipleChoiceSize;

    private bool _slime;
    private int _cutsceneIndex = 0;
    private int _characterDataIndex = 0;
    private int _dialogueIndex = 0;
    private int _multipleChoiceIndex = 0;

    private int _progression = 0;
    private bool _isChoosing;
    public int _brennDisposition, _mayneDisposition,_zipDisposition;

    private void Start()
    {
        UpdateHeader(_cutscenes[_cutsceneIndex].textData[_dialogueIndex].character);
        ParseDialogue(_cutsceneIndex, _dialogueIndex);

    }

    public void Update()
    {
       
        PlayCutscene(_cutsceneIndex);
    }

    //This funciton is responsible for going through and making sure the header and character being presented is accurate. If the same character speaks twice there's no need to re-set the header.
    public void UpdateHeader(CharacterData _cd = null)
    {

        if (cd!=_cd)
        {
            cd = _cd;
            if (cd == null)
            {
                header.gameObject.transform.parent.gameObject.SetActive(false);
                slimeGameObject.SetActive(false);
            }
            else
            {
                header.gameObject.transform.parent.gameObject.SetActive(true);
                slimeGameObject.SetActive(cd.slime);
                slimeGameObject.GetComponent<RawImage>().color = cd.slimeColour;
                header.text = cd.characterName;
            }
        }
            
    }
    
    //This manages the currently active Cutscene, taking a click input from the player to proceed. If they've reached the end of the scene it will pop up the appropriate choice from the two prefab variants.
    private void PlayCutscene(int i)
    {
        if (Input.GetButtonDown("Fire1") && !dialogPrinter.isRunning()&&!_isChoosing)
        {
            if (_dialogueIndex < _cutscenes[_cutsceneIndex].textData.Length)
            {
                if (dialogPrinter.mainTextBox.text != "")
                {
                    dialogPrinter.StopPrinting();
                }
                ParseDialogue(i, _dialogueIndex);
            }
            else
            {
                
                _isChoosing = true;
                SetUpOptions(_cutscenes[_cutsceneIndex].multipleChoice, _cutscenes[_cutsceneIndex].cutManagerLink);
                mainDisplay.SetActive(false);
            }
            
        }
        
    }

    //This sets up and updates the options given to the player for making choices and makes sure they're able to navigate to the correct link. 
    //Not that #0 element connects to the second option, #1 element connects to the first and #2 connects to the third option. This will be patched out later.
    private void SetUpOptions(TextData.data[] optionText, int[] locations)
    {
        var options = Instantiate(choiceOption[_cutscenes[_cutsceneIndex].numberOfOptions - 2], transform.position, Quaternion.identity);
        options.transform.parent = transform;
        options.transform.localScale = new Vector3(100, 100, 1);
        GameObject[] choices = GameObject.FindGameObjectsWithTag("Choice");
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].GetComponentInChildren<TextMeshProUGUI>().text = optionText[i].content;
            choices[i].GetComponentInChildren<SceneRelocator>().sceneID = locations[i];

        }
    }

    //This function parses the correct dialogue along with making sure the header and character dispositions are properly handled. 
    private void ParseDialogue(int i, int y)
    {
        DispotionsParse();
        UpdateHeader(_cutscenes[_cutsceneIndex].textData[_dialogueIndex].character);
        dialogPrinter.PrintText(_cutscenes[i].textData[y].content);
        _dialogueIndex++;
    }

    //This updates character dispositions if disposition is properly set up via the inspector, otherwise it ignores it.
    private void DispotionsParse()
    {
        if (_cutscenes[_cutsceneIndex].textData[_dialogueIndex].disposition.Length ==3)
        {
            if (_cutscenes[_cutsceneIndex].textData[_dialogueIndex].disposition[0] != 0 || _cutscenes[_cutsceneIndex].textData[_dialogueIndex].disposition[1] != 0 || _cutscenes[_cutsceneIndex].textData[_dialogueIndex].disposition[2] != 0)
            {
                _brennDisposition += _cutscenes[_cutsceneIndex].textData[_dialogueIndex].disposition[0];
                _mayneDisposition += _cutscenes[_cutsceneIndex].textData[_dialogueIndex].disposition[1];
                _zipDisposition += _cutscenes[_cutsceneIndex].textData[_dialogueIndex].disposition[2];
            }
        }
       
    }

    //This is a public function allowing other scripts (such as scene relocator) to set what scene needs to come next. 
    public void SetCutScene(int i)
    {
        dialogPrinter.StopPrinting();
        _dialogueIndex = 0;
        _cutsceneIndex = i;
        GameObject[] cleanup = GameObject.FindGameObjectsWithTag("CleanUP");
        for (int n = 0; n < cleanup.Length; n++)
        {
            Destroy(cleanup[n]);
        }
        mainDisplay.SetActive(true);
        _isChoosing = false;

        ParseDialogue(_cutsceneIndex, _dialogueIndex);
    }

    
}