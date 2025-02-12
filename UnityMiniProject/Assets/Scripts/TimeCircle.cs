using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeCircle : MonoBehaviour
{
    public TextMeshProUGUI successText;
    public Image timeCircle;
    public TextMeshProUGUI timeText;

    private string timeFormat = "{0:D2}:{1:D2}";
    public float time;
    public float endTime;

    public Color defColor;

    private void Start()
    {
        defColor = new Color(0.7f, 1f, 0.7f);
        Reset();
    }

    private void Update()
    {
        
        timeText.text = String.Format(timeFormat, (int)((endTime - time) / 60), (int)((endTime - time) % 60));
        timeCircle.fillAmount = time / endTime;
        
        if (time >= endTime)
        {
            successText.gameObject.SetActive(true);
            timeCircle.color = new Color(1f, 0.5f, 0f);
        }
        else
        {
            time += Time.deltaTime;
        }
    }

    public void Reset()
    {
        timeCircle.fillAmount = 0;
        timeText.text = "00:00";
        successText.gameObject.SetActive(false);
        timeCircle.color = defColor;
        gameObject.SetActive(false);
        
    }
}
