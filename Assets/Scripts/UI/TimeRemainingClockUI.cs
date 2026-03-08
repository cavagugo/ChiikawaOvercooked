using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeRemainingClockUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeRemaining;


    private void Update()
    {
        float remainingTime = GameManager.Instance.GetRemainingTime();

        if (remainingTime < 0)
        {
            remainingTime = 0;
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timeRemaining.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
