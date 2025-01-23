using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Furnace : MonoBehaviour
{
    private bool isBake = false;
    private bool collideItem = false;
    private readonly float bakeTime = 10f;
    private float curBakingTime;

    public Image timeCircle;
    public Image itemImage;

    private void Start()
    {
        timeCircle.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(isBake)
        {
            Debug.Log(curBakingTime / bakeTime);
            curBakingTime += Time.deltaTime;
            if (curBakingTime > bakeTime)
            {
               isBake = false;
            }
            timeCircle.fillAmount = curBakingTime / bakeTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collideItem = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collideItem = false;
    }

    public void BakeItem(Weapon weapon)
    {
        if (!collideItem)
            return;

        isBake = true;
        timeCircle.gameObject.SetActive(true);
        itemImage.sprite = weapon.data.IconSprite;

    }
}
