using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneManager : MonoBehaviour
{
    [SerializeField] private string state;
    [SerializeField] public Sprite background;

    public CharacterData[] characterData;
    public TextData[] textData;
    public TextData[] multipleChoice;
    public int[] cutManagerLink;
}
