using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Defines;

public class AddItem : MonoBehaviour
{
    public ExpendMgr ExpendMgr;
    public TimeCircle timeCirclePrefeb;
    public TimeCircle timeCircle;
    public TextMeshProUGUI needText;

    public float endTime = 150f;
    public int needGold = 100;
    public int increseGold = 900;
    public float curTime;

    private bool isMakable = false;
    private bool isCreate = false;


    private void Start()
    {
        timeCircle = Instantiate(timeCirclePrefeb, transform.position, Quaternion.identity, ExpendMgr.playUi.transform);
        needText.text = needGold.ToString();
    }
    private void Update()
    {
        if (isMakable)
        {
            curTime += Time.deltaTime;
            if (curTime >= endTime)
            {
                isCreate = true;
            }
        }
    }

    private void OnMouseUp()
    {
        if(RayCastGoWithTag(gameObject.tag))
        {
            if (!isMakable)
                CheckGold();
            if (!isCreate)
                return;
            switch (gameObject.tag)
            {
                case "AddBase":
                    ExpendMgr.AddBase();
                    break;
                case "AddFurnace":
                    ExpendMgr.AddFurnace();
                    break;
                case "AddStand":
                    ExpendMgr.AddStand();
                    break;
                default:
                    break;
            }
            timeCircle.Reset();
            isMakable = false;
            isCreate = false;
            curTime = 0;
        }
        
    }

    public void AddPlate()
    {
        ExpendMgr.AddPlate();
    }

    private void CheckGold()
    {
        if (ExpendMgr.gm.totalGold >= needGold)
        {
            isMakable = true;
            ExpendMgr.gm.MinusTotalGold(needGold);

            timeCircle.transform.position = transform.position;
            timeCircle.time = curTime;
            timeCircle.endTime = endTime;
            timeCircle.gameObject.SetActive(true);
        }
            
    }

    public void SetNeedGold(int amount)
    {
        needGold += amount;
        needText.text = needGold.ToString();
    }
}
