using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRelocator : MonoBehaviour
{
    public int sceneID;

    public void SetNarrativeScene()
    {
        FindObjectOfType<NarrativeController>().SetCutScene(sceneID);
    }
}
