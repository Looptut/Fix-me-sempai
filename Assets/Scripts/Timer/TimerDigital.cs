using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TimerDigital : MonoBehaviour
{
    public Text textBox;
    public int minutesStart;
    public int hoursStart;
    public int minutesEnd;
    public int hoursEnd;
    public float secondsDuration;
    public bool dayEnd;
    public static event Action onTimeIsOver = delegate { };

    private int minutesDuration;
    private float secondsToPass;
    void Start()
    {
        textBox = gameObject.GetComponent<Text>();
        textBox.text = timeToString(hoursStart, minutesStart);
        minutesDuration = (hoursEnd - hoursStart) * 60 + minutesEnd - minutesStart;
        secondsToPass = secondsDuration;
        dayEnd = false;
    }

    void Update()
    {
        if (!dayEnd)
        {
            if (textBox.text == timeToString(hoursEnd, minutesEnd))
            {
                Debug.Log("Pora domoi");
                dayEnd = true;
                onTimeIsOver();
            }
            else
            {
                secondsToPass -= Time.deltaTime;
                textBox.text = timeCurrent();
            }
        }
    }

    private string timeToString(int hours, int minutes)
    {
        return ((hours < 10 ? "0" : "") + hours.ToString() + ":" + (minutes < 10 ? "0" : "") + minutes.ToString());
    }

    private string timeCurrent() 
    {
        int minutes = minutesDuration - Mathf.RoundToInt(minutesDuration * secondsToPass / secondsDuration) + minutesStart;
        int hours = hoursStart;
        while (minutes > 59)
        {
            hours++;
            minutes -= 60;
        }
        return timeToString(hours, minutes);
    }
}
