using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackerTimerControl : MonoBehaviour
{
    public static HackerTimerControl currTimer;

    [SerializeField] Text uiText;
    [SerializeField] float maxTime;
    
    public float timer;
    public int minutes;
    public int seconds;
    bool isRunning = false;

    public void OnEnable()
    {
        currTimer = this;
        this.timer = this.maxTime;
    }

    private void OnDisable()
    {
        currTimer = null;
    }

    public void StartTimer()
    {
        this.isRunning = true;
    }

    public void StopTimer()
    {
        this.isRunning = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (!this.isRunning) return;

        if (timer >= 0.0f)
        {
            timer -= Time.deltaTime;
            seconds = (int)(timer % 60);
            minutes = (int)(timer / 60) % 60;
            if (seconds == 0)
            {
                uiText.fontSize = 66;
            }
            else
            {
                uiText.fontSize = 30;
            }
            if (timer <= 60.0f)
            {
                uiText.fontSize = 70;
            }
            if (timer <= 10.0f)
            {
                uiText.fontSize = 70;
                uiText.color = Color.red;
            }
            uiText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        }
        else
        {
            this.isRunning = false;
            uiText.text = "00 : 00";
            timer = 0.0f;
            TimeIsUp();
        }
    }

    void TimeIsUp() {
        //GameManager.Instance.CauseDeath("You ran out of time", 0);
        Debug.Log("You lose");
    }
}
