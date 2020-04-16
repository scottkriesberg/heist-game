using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackerTimerControl : MonoBehaviour
{
    [SerializeField] Text uiText;
    [SerializeField] float maxTime;

    float timer;
    bool canCount = true;
    bool doOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= 0.0f && canCount) {
            timer -= Time.deltaTime;
            int seconds = (int) (timer % 60);
            int minutes = (int) (timer / 60) % 60;
            int hours = (int) (timer / 3600) % 24;
            if(seconds == 0) {
                uiText.fontSize = 66;
            }
            else {
                uiText.fontSize = 30;
            }
            if(timer <= 60.0f) {
                uiText.fontSize = 70;
            }
            if(timer <= 10.0f) {
                uiText.fontSize = 70;
                uiText.color = Color.red;
            }
            uiText.text = string.Format("{0:0} : {1:00} : {2:00}", hours, minutes, seconds);
            // uiText.text = timer.ToString("N0");
        }
        else if(timer <= 0.0f && !doOnce) {
            canCount = false;
            doOnce = true;
            uiText.text = "0 : 00 : 00";
            timer = 0.0f;
            TimeIsUp();
        }
    }

    void TimeIsUp() {
        GameManager.Instance.CauseDeath("You ran out of time", 0);
        Debug.Log("You lose");
    }
}
