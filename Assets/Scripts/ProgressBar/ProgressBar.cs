using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    // Start is called before the first frame update
    void Start()
    {
        ChangeProgress(startPoints);
    }

    // Update is called once per frame
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
        targetProgress = slider.value + (float)progressValue/maxPoints;
    }
}
