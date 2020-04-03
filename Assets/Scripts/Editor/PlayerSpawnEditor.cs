using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerSpawnPoint))]
public class PlayerSpawnEditor : Editor
{
    private void OnEnable()
    {
        SceneView.duringSceneGui += this.MyScene;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= this.MyScene;
    }

    protected virtual void MyScene(SceneView sv)
    {
        Handles.color = Color.green;
        Transform transform = ((PlayerSpawnPoint)this.target).transform;
        Handles.SphereHandleCap(0, transform.position, Quaternion.identity, 0.3f, EventType.Repaint);
    }
}
