using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellItemMgr : MonoBehaviour
{
    public bool IsSell { get; set; }
    public Slider slider;

    public Image sliderImage;
    public TextMeshProUGUI sliderText;
    public TextMeshProUGUI sumGoldText;

    private float sellTime = 5f;
    private float curSellTime;

    private Guest curGuest;
    public int sumPrice = 0;
    public BakeCount countPrefeb;

    public Animator anim;
    private void Update()
    {
        
        if (IsSell)
        {
            anim.SetBool("5_Debuff", true);
            slider.gameObject.SetActive(true);
            curSellTime += Time.deltaTime;
            if (curSellTime > sellTime && curGuest.itemCount > 0)
            {
                curSellTime = 0;
                sumPrice += curGuest.buyItems[curGuest.buyItems.Count - curGuest.itemCount].Price;
                sumGoldText.text = sumPrice.ToString();
                curGuest.itemCount--;
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
            sliderText.text = $"{(int)(sellTime - curSellTime) + 1}s";
        }
        else
        {
            anim.SetBool("5_Debuff", false);
            slider.gameObject.SetActive(false);
            curSellTime = 0;
            sumPrice = 0;
            sumGoldText.text = sumPrice.ToString();
        }

    }

    public void SetCurGuest(Guest guest)
    {
        curGuest = guest;
    }

    public void CreateGoldCount()
    {
        BakeCount temp = Instantiate(countPrefeb, 
            new Vector2(slider.transform.position.x, slider.transform.position.y + 10), Quaternion.identity);
        temp.transform.SetParent(slider.transform.parent);
        temp.itemCount.text = $"   {sumPrice.ToString()}";
    }

}
