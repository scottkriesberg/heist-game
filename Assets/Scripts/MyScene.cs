using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScene : MonoBehaviour
{
    [SerializeField]
    protected PlayerSpawnPoint mySpawn;

    public virtual void OnLoad()
    {
        GameManager.Instance.RegisterSpawnPoint(this.mySpawn);
        GameManager.Instance.SetCameraMode(false);
    }

    public virtual void OnUnload()
    {
        Alarm.Instance.StopAlarm();
    }
}
