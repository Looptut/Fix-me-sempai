using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    private float targetProgress = 0;
    private int currentPoints = 0;

    public float fillSpeed;
    public int startPoints;
    public int maxPoints;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }
    void Start()
    {
        ChangeProgress(startPoints);
    }

    void Update()
    {
        if (slider.value < targetProgress)
            slider.value += fillSpeed * Time.deltaTime;
        if (slider.value > targetProgress)
            slider.value -= fillSpeed * Time.deltaTime;
    }

    public void ChangeProgress(int progressValue)
    {
        if ((currentPoints + progressValue) > maxPoints)
            progressValue = maxPoints - currentPoints;
        if ((currentPoints + progressValue) < 0)
            progressValue = -currentPoints;
        currentPoints += progressValue;
        targetProgress = slider.value + (float)progressValue / maxPoints;
    }
}
