using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BakeTimeCircle : MonoBehaviour
{
    public TextMeshProUGUI successText;
    public Image timeCircle;
    public Image itemImage;

    public Color defColor;

    private void Start()
    {
        defColor = new Color(0.7f, 1f, 0.7f);
        Reset();
    }

    public void Reset()
    {
        timeCircle.fillAmount = 0;
        itemImage.sprite = null;
        successText.gameObject.SetActive(false);
        timeCircle.color = defColor;

        
    }
}
