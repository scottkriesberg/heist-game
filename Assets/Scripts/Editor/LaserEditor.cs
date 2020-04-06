using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Laser))]
public class LaserEditor : Editor
{
    private SerializedProperty endAProp;
    private SerializedProperty endBProp;

    private void OnEnable()
    {
        this.endAProp = this.serializedObject.FindProperty("endA");
        this.endBProp = this.serializedObject.FindProperty("endB");
    }

    private void OnSceneGUI()
    {
        if (Application.isPlaying) return;
        Laser laser = (Laser)this.target;
        if (laser == null) return;
        SerializedProperty moveDist = this.serializedObject.FindProperty("moveDistance");
        this.endAProp.vector3Value = laser.transform.position;
        this.endBProp.vector3Value = laser.transform.position + (laser.transform.forward * moveDist.floatValue);
        this.serializedObject.ApplyModifiedProperties();
    }
}
