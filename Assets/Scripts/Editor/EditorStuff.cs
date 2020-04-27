using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorStuff : Editor
{
    [MenuItem("Tools/Select/AllAudioListeners")]
    public static void SelectListeners()
    {
        AudioListener[] objects = GameObject.FindObjectsOfType<AudioListener>();
        List<GameObject> listenerObjects = new List<GameObject>();
        foreach (AudioListener au in objects)
        {
            listenerObjects.Add(au.gameObject);
        }

        Selection.objects = listenerObjects.ToArray();
    }
}
