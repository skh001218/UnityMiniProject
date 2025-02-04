using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellItem : MonoBehaviour
{
    private bool isSell = false;
    public Slider slider;
    public Image sliderImage;

    private float sellTime = 5f;
    private float curSellTime;

    private Guest curGuest;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSell)
        {
            slider.gameObject.SetActive(true);
            curSellTime += Time.deltaTime;
            if (curSellTime > sellTime)
            {

            }
            slider.value = curSellTime / sellTime;
            if (slider.value <= 0f)
            {
                slider.fillRect.gameObject.SetActive(false);
            }
            else
            {
                slider.fillRect.gameObject.SetActive(true);
            }
            slider.GetComponentInChildren<TextMeshProUGUI>().text = $"{(int)(sellTime - curSellTime) + 1}s";
        }
        else
        {
            slider.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            isSell = true;
        }
    }
}
