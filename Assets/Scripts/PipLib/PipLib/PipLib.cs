using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

namespace PipLib
{
    
    
    public class Util
    { 
        public static float PointToward(GameObject target, GameObject pointer)
        {
            float xdis;
            float ydis;
            float dh;
            float result = 0;

            if (target != null || pointer != null) //This checks if the pointer or the target exist, in order to avoid errors. Allowing the addition or removal of objects.
            {
                //First it takes the total x and y distance between the two of them. Essentially forming a triangle.
                xdis = target.transform.position.x - pointer.transform.position.x;
                ydis = target.transform.position.y - pointer.transform.position.y;
                //Then using the square of the Hypotenuse, we'reable to find a line between the two of them.
                dh = Mathf.Sqrt(xdis * xdis + ydis * ydis);

                //The result is then the Cosine rule is used to get the angle that's required. 
                result = (Mathf.Acos(ydis / dh) * (180 / Mathf.PI));
                if (xdis > 0) //This works for most numbers, however, if the x distance is on the left side, it will give us a negative number.
                {
                    result = -result; //The easiest and simplest solution is to invert the result.
                }



                return result;
            }
            else
            { //If either object does not exist. The Z rotation is left as is. 
                result = pointer.transform.localRotation.z;
                return result;
            }

        }

        //Quick refrence to where the mouse is in the world on screen with a z value of 10. 
        //This is primarly for working in Unity2D.
        public static Vector3 MousePos()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            return Camera.main.ScreenToWorldPoint(mousePos);
        }

        //This will randomly shake the object, or set it to its original position if it's been shaken in the previous frame. 
        //AKA calling this function will shake this object within the same bounds with one frame displacing and the other frame repositioning.
        public static void Shake(Transform _position, float? amount = null, Vector3? originalPos = null)
        {
            if (originalPos == null|| originalPos == _position.position)
            {
                _position.position += new Vector2(UnityEngine.Random.Range(-1, 2) * (amount ?? 1), UnityEngine.Random.Range(-1, 2) * (amount ?? 1)).ToVector3();
            }
            else if(originalPos != _position.position)
            {
                _position.position = originalPos??_position.position;
            }
                        
        }

        //Returns a float with an absolute range, with a optional offset. For example Util.AbsRand(5, 20) would give you something between 15 and 25.
        public static float AbsRand(float absoluteRange, float? offset = null)
        {
            return UnityEngine.Random.Range(-absoluteRange, absoluteRange)+offset??0;
        }

        public static void UpdateVolume(float volumeMod)
        {
            AudioSource[] audioSources = MonoBehaviour.FindObjectsOfType<AudioSource>();
            for (int i = 0; i < audioSources.Length; i++)
            {          
                    audioSources[i].volume *= volumeMod;               
            }
        }
    }

    public class Printer : MonoBehaviour
    {
        public List<string> sentencelist = new List<string>();
        public float typingSpeed;
        public float cooldown;

        private int index;
        private TextMeshProUGUI PROtext;
        private Text text;

        public void AddToPrinter(string _text)
        {
            sentencelist.Add(_text);
        }

        public void SelectText(TextMeshProUGUI textElement)
        {
            PROtext = textElement;
        }

        public void SelectText(Text textElement)
        {
            text = textElement;
        }
        public void Print()
        {
            StartCoroutine(PrintE());
        }
        IEnumerator PrintE()
        {
            if (PROtext == null)
            {
                foreach (char letter in sentencelist[index].ToCharArray())
                {
                    text.text += letter;
                    yield return new WaitForSeconds(typingSpeed);
                }
            }
            else
            {
                foreach (char letter in sentencelist[index].ToCharArray())
                {
                    PROtext.text += letter;
                    yield return new WaitForSeconds(typingSpeed);
                }
            }
            

        }
    }

    public static class Extensions
    {
        //This gives the option to do string.WordCount() and return the number of words in the string.
        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' },
                             StringSplitOptions.RemoveEmptyEntries).Length;
        }

        //This RETURNS the value of the passed in coordinates. To actually set it you need to do something like: transform.position = transform.position.With(z: 5);
        public static Vector3 With(this Vector3 original,float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x??original.x,y??original.y,z??original.z);
        }
        
        //This RETURNS the value of the vector with the coordinates passed, but instead of Setting the coordinated, it Adds them from the original. 
        public static Vector3 Add(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x + original.x ?? original.x, y + original.y ?? original.y, z + original.z ?? original.z);
        }
        //This is an Overloaded version of the above Function, incase I just want to add two vectors together.
        public static Vector3 Add(this Vector3 original, Vector3 vector3)
        {
            return original + vector3;
        }

        //Used to flatten the y value to 0;
        public static Vector3 Flat(this Vector3 original)
        {
            return new Vector3(original.x, 0, original.z);
        }

        public static Vector3 DirectionTo(this Vector3 source, Vector3 destination)
        {
            return Vector3.Normalize(destination - source);
        }

        //This allows you attach one object to another. Again you'll want to go with transform.position = transform.position.Attach(target).
        public static Vector3 Attach(this Vector3 vec, Transform _target, Vector3? _offset = null)
        {
           return ((_offset ?? Vector3.zero) + _target.position);
          
        }

        // Vector3 -> Vector2
        public static Vector3 ToVector3(this Vector2 original, float? z = null)
        {
            return new Vector3(original.x, original.y, z ?? 0);
        }

        // Vector3 -> Vector2
        public static Vector2 ToVector2(this Vector3 original)
        {
            return new Vector2(original.x, original.y);
        }

        public static Vector3 Abs(this Vector3 vec)
        {
            return new Vector3(Mathf.Abs(vec.x),Mathf.Abs(vec.y),Mathf.Abs(vec.z));
        }

        //Turns a Bool into either a 1 or 0 for you. 
        public static int ToInt(this bool state)
        {
            if (state)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        //Turns an int into either a true or false for you. 
        public static bool ToBool(this int oneOrZero)
        {
            if (oneOrZero==1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
    }
}
