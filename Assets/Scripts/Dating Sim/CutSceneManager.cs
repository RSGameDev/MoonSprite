using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneManager : MonoBehaviour
{
    [Serializable]
    public struct Cutscene {
        [SerializeField] private string state;
        [SerializeField] public Sprite background;

        public TextData.data[] textData;
        public TextData.data[] multipleChoice;
        public int[] cutManagerLink;
        public int numberOfOptions;
    }
    
}
