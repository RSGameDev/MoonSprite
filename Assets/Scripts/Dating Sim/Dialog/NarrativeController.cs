using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PipLib.Stage;

public class NarrativeController : MonoBehaviour
{
    public CutSceneManager.Cutscene[] _cutscenes;

    public Animator cameraAnim;
    public GameObject mainDisplay;
    public GameObject[] choiceOption;
    public GameObject slimeGameObject;
    public DialogPrinter dialogPrinter;
    public TextMeshProUGUI header;
    public Image characterArt;
    public Animator characterAnimator;
    [SerializeField] private Image background;
    private CharacterData cd;

    public ParticleSystem[] ps;


    private int textDataSize;
    private int multipleChoiceSize;

    private bool _slime;
    private int _cutsceneIndex = 0;
    private int _dialogueIndex = 0;



    private bool _isChoosing;
    public int _brennDisposition, _mayneDisposition,_zipDisposition;
    public int threshold;

    public int brennProposal, mayneProposal, zipProposal, brennSucess, mayneSucess, zipSucess, brennFail, mayneFail, zipFail;
    private void Start()
    {
        UpdateHeader(_cutscenes[_cutsceneIndex].textData[_dialogueIndex].character);
        ParseDialogue(_cutsceneIndex, _dialogueIndex);
        UpdateChances();

    }

    public void Update()
    {
        if (background.sprite != _cutscenes[_cutsceneIndex].background)
        {
            background.sprite = _cutscenes[_cutsceneIndex].background;
        }
        if (_dialogueIndex < _cutscenes[_cutsceneIndex].textData.Length)
        {
            if (dialogPrinter.mainTextBox.text == _cutscenes[_cutsceneIndex].textData[_dialogueIndex].content)
            {
                _dialogueIndex++;
            }
        }
       
        PlayCutscene(_cutsceneIndex);
        UpdateChances();
        
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
                if (_cutscenes[_cutsceneIndex].cutManagerLink.Length < 2)
                {
                    if (_cutscenes[_cutsceneIndex].cutManagerLink[0] == 555)
                    {
                        SceneControls.NextLevel();
                    }
                    

                    if (_cutscenes[_cutsceneIndex].cutManagerLink[0] == 777)
                    {
                        Application.Quit();
                    }
                    else
                    {
                        SetCutScene(_cutscenes[_cutsceneIndex].cutManagerLink[0]);
                       
                    }
                   
                }
                else
                {
                    _isChoosing = true;
                    SetUpOptions(_cutscenes[_cutsceneIndex].multipleChoice, _cutscenes[_cutsceneIndex].cutManagerLink);
                    mainDisplay.SetActive(false);
                }
            }

        }
        else
        {
            if (Input.GetButtonDown("Fire1") && dialogPrinter.isRunning() && !_isChoosing)
            {
                //Skip to the end function for text printer.
                dialogPrinter.StopPrinting();
                dialogPrinter.StopAllCoroutines();
                dialogPrinter.printableText = "";
                dialogPrinter.mainTextBox.text = _cutscenes[_cutsceneIndex].textData[_dialogueIndex].content;
                
            }
        }
        
    }

    //This sets up and updates the options given to the player for making choices and makes sure they're able to navigate to the correct link. 
    //Not that #0 element connects to the second option, #1 element connects to the first and #2 connects to the third option. This will be patched out later.
    private void SetUpOptions(TextData.data[] optionText, int[] locations)
    {
        var options = Instantiate(choiceOption[_cutscenes[_cutsceneIndex].multipleChoice.Length - 2], transform.position, Quaternion.identity);
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
        ParseArt();
        DispotionsParse();
        UpdateHeader(_cutscenes[_cutsceneIndex].textData[_dialogueIndex].character);
        dialogPrinter.PrintText(_cutscenes[i].textData[y].content);
        if (_cutscenes[_cutsceneIndex].textData[_dialogueIndex].music > 0)
        {
            ParseMusic(_cutscenes[_cutsceneIndex].textData[_dialogueIndex].music);
        }
        if (_cutscenes[_cutsceneIndex].textData[_dialogueIndex].music == -1)
        {
            ParseMusic(0);
        }
        if (_cutscenes[_cutsceneIndex].textData[_dialogueIndex].sound>0)
        {
            FindObjectOfType<DatingSimSoundBoard>().PlaySound(_cutscenes[_cutsceneIndex].textData[_dialogueIndex].sound);
        }
        if (_cutscenes[_cutsceneIndex].textData[_dialogueIndex].shaketime > 0)
        {
            ParseShake(_cutscenes[_cutsceneIndex].textData[_dialogueIndex].shaketime);
        }

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

            if (_cutscenes[_cutsceneIndex].textData[_dialogueIndex].disposition[0] > 0)
            {
                ps[0].Emit(_cutscenes[_cutsceneIndex].textData[_dialogueIndex].disposition[0]);
            }
            if (_cutscenes[_cutsceneIndex].textData[_dialogueIndex].disposition[1] > 0)
            {
                ps[1].Emit(_cutscenes[_cutsceneIndex].textData[_dialogueIndex].disposition[1]);
            }
            if (_cutscenes[_cutsceneIndex].textData[_dialogueIndex].disposition[2] > 0)
            {
                ps[2].Emit(_cutscenes[_cutsceneIndex].textData[_dialogueIndex].disposition[2]);
            }

            if (_cutscenes[_cutsceneIndex].textData[_dialogueIndex].disposition[0] < 0)
            {
                ps[3].Emit(Mathf.Abs(_cutscenes[_cutsceneIndex].textData[_dialogueIndex].disposition[0]));
            }
            if (_cutscenes[_cutsceneIndex].textData[_dialogueIndex].disposition[1] < 0)
            {
                ps[4].Emit(Mathf.Abs(_cutscenes[_cutsceneIndex].textData[_dialogueIndex].disposition[1]));
            }
            if (_cutscenes[_cutsceneIndex].textData[_dialogueIndex].disposition[2] < 0)
            {
                
                ps[5].Emit(Mathf.Abs(_cutscenes[_cutsceneIndex].textData[_dialogueIndex].disposition[2]));
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

    public void ParseArt()
    {
        if (_cutscenes[_cutsceneIndex].textData[_dialogueIndex].showCharacter > 0)
        {
            if (_cutscenes[_cutsceneIndex].textData[_dialogueIndex].character!=null)
            {
                characterArt.sprite = _cutscenes[_cutsceneIndex].textData[_dialogueIndex].character.characterArt[_cutscenes[_cutsceneIndex].textData[_dialogueIndex].showCharacter - 1];
            }
           
        }
        if (_cutscenes[_cutsceneIndex].textData[_dialogueIndex].fadeIn)
        {
            characterAnimator.ResetTrigger("FadeIn");
            if (characterArt.color.a == 0)
            {
                characterAnimator.SetTrigger("FadeIn");
            }
            
           
        }
        if (_cutscenes[_cutsceneIndex].textData[_dialogueIndex].fadeOut)
        {
            characterAnimator.ResetTrigger("FadeOut");
            if (characterArt.color.a == 1)
            {
                characterAnimator.SetTrigger("FadeOut");
            }
        }
    }

    public void UpdateChances()
    {
        if (_brennDisposition>threshold)
        {
            _cutscenes[brennProposal].cutManagerLink[0] = brennSucess;
        }
        else
        {
            _cutscenes[brennProposal].cutManagerLink[0] = brennFail;
        }

        if (_mayneDisposition > threshold)
        {
            _cutscenes[mayneProposal].cutManagerLink[0] = mayneSucess;
        }
        else
        {
            _cutscenes[mayneProposal].cutManagerLink[0] = mayneFail;
        }

        if (_zipDisposition > threshold)
        {
            _cutscenes[zipProposal].cutManagerLink[0] = zipSucess;
        }
        else
        {
            _cutscenes[zipProposal].cutManagerLink[0] = zipFail;
        }
    }

    public void ParseMusic(int track)
    {
        FindObjectOfType<DatingSimMusicManager>().Change(track);
        
    }

    public void ParseShake(float time)
    {
        cameraAnim.SetFloat("ShakeTime", time);
    }
}