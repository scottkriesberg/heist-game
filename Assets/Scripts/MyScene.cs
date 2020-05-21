using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScene : MonoBehaviour
{
    [SerializeField]
    protected PlayerSpawnPoint mySpawn;
    [SerializeField]
    protected Light[] alarmLights = new Light[0];
    [SerializeField]
    protected Hacker hacker;

    public virtual void OnLoad()
    {
        if (this.hacker != null) CameraControl.instance = this.hacker.camControl;
        GameManager.Instance.RegisterSpawnPoint(this.mySpawn);
        GameManager.Instance.SetCameraMode(false);
        GameManager.Instance.SetPlayerHeadSmall(true);
        Alarm.Instance.lights = alarmLights;
        Alarm.Instance.StopAlarm();
        HackerTimerControl.currTimer?.StartTimer();
        Terminal.ClearScreen();
        Laser.PlayerDied = false;
        GuardState.GlobalToggle = true;
        GameManager.CurrFailed = false;
    }

    public virtual void OnUnload()
    {
        Alarm.Instance.StopAlarm();
    }
}
