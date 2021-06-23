using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PipLib;

public class FollowMouse : MonoBehaviour
{
    
    void Update()
    {
        transform.position = Util.MousePos();
    }
}
