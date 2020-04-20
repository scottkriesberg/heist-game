using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    public static Alarm Instance;

    [SerializeField]
    private float flashFreq;
    [SerializeField]
    private float alarmVolume;
    [SerializeField]
    private Light[] lights;
    [SerializeField]
    private AudioSource alarmSource;
    private bool alarmOn;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        this.alarmOn = true;
        this.StopAlarm();
    }

    private void Update()
    {
        if (!this.alarmOn) return;

        foreach (Light light in this.lights) light.color = Color.Lerp(Color.red, Color.black, Mathf.PingPong((Time.timeSinceLevelLoad - 0.1f) * this.flashFreq, 1f));
    }

    public void StartAlarm(string reason = "")
    {
        if (this.alarmOn) return;
        GameManager.Instance.CauseDeath(reason, GameManager.Instance.CurrScene, 5f);
        this.alarmOn = true;
        foreach (Light light in this.lights) light.enabled = true;
        this.alarmSource.volume = this.alarmVolume;
    }

    public void StopAlarm()
    {
        if (!this.alarmOn) return;
        this.alarmOn = false;
        foreach (Light light in this.lights) light.enabled = false;
        this.alarmSource.volume = 0f;
    }
}
