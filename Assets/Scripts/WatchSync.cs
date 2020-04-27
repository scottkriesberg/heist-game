using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchSync : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI minutesOne;
    [SerializeField]
    TMPro.TextMeshProUGUI minutesTen;
    [SerializeField]
    TMPro.TextMeshProUGUI secondsOne;
    [SerializeField]
    TMPro.TextMeshProUGUI secondsTen;

    private void Update()
    {
        if (HackerTimerControl.currTimer == null) return;

        int minutesOne = HackerTimerControl.currTimer.minutes % 10;
        int minutesTen = (HackerTimerControl.currTimer.minutes - minutesOne) / 10;

        int secondsOne = HackerTimerControl.currTimer.seconds % 10;
        int secondsTen = (HackerTimerControl.currTimer.seconds - secondsOne) / 10;

        this.minutesOne.text = minutesOne.ToString();
        this.minutesTen.text = minutesTen.ToString();
        this.secondsOne.text = secondsOne.ToString();
        this.secondsTen.text = secondsTen.ToString();
    }
}
