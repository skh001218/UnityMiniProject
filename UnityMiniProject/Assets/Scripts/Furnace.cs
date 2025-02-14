using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Furnace : MonoBehaviour
{
    public bool IsBake { get; private set; } = false;
    public bool CollideItem { get; private set; } = false;
    private float bakeTime = 10f;
    private float curBakingTime;

    public BakeTimeCircle bakeTimeCircle;
    public BakeCount countPrefeb;

    private void Update()
    {
        if(IsBake && bakeTimeCircle.timeCircle.fillAmount < 1)
        {
            bakeTimeCircle.gameObject.SetActive(true);
            curBakingTime += Time.deltaTime;
            /*if (curBakingTime > bakeTime)
            {
                IsBake = false;
            }*/
            bakeTimeCircle.timeCircle.fillAmount = curBakingTime / bakeTime;
            
        }

        if(bakeTimeCircle.timeCircle.fillAmount >= 1)
        {
            bakeTimeCircle.successText.gameObject.SetActive(true);
            bakeTimeCircle.timeCircle.color = new Color(1f, 0.5f, 0f);
            
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollideItem = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        CollideItem = false;
    }

    public void BakeItem(Weapon weapon)
    {
        if (!CollideItem)
            return;

        IsBake = true;
        bakeTime = weapon.data.BakeTime;
        bakeTimeCircle.timeCircle.gameObject.SetActive(true);
        bakeTimeCircle.itemImage.sprite = weapon.data.IconSprite;
        bakeTimeCircle.successText.gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    private void OnMouseUp()
    {
        if(bakeTimeCircle.timeCircle.fillAmount >= 1)
        {
            BakeCount temp = Instantiate(countPrefeb, bakeTimeCircle.transform.position, Quaternion.identity);
            temp.transform.SetParent(bakeTimeCircle.transform.parent);
            temp.itemImage.sprite = bakeTimeCircle.itemImage.sprite;

            bakeTimeCircle.gameObject.SetActive(false);
            bakeTimeCircle.Reset();
            IsBake = false;
            curBakingTime = 0;
            CollideItem = false;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            
        }
    }
}
