using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextData : ScriptableObject
{
    [Serializable]
    public struct data
    {
        public string content;
        public CharacterData character;
        public int[] disposition;
        public int showCharacter;
        public bool fadeIn;
        public bool fadeOut;
    }
}
