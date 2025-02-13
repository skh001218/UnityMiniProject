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

    public int needGold = 60;
    public int increseGold = 50;
    public int maxIncreseGold = 1000;
    public int needDia = 10;
    public int increseDia = 50;
    public int maxIncreseDia = 1000;
    public float endTime = 60f;
    public float increseTime = 30f;
    public float maxIncreseTime = 300f;
    public float curTime;

    private bool isMakable = false;
    private bool isCreate = false;


    private void Start()
    {
        needGold = 60;
        increseGold = 50;
        maxIncreseGold = 1000;
        needDia = 10;
        increseDia = 50;
        maxIncreseDia = 1000;
        endTime = 60f;
        increseTime = 30f;
        maxIncreseTime = 300f;

        timeCircle = Instantiate(timeCirclePrefeb, transform.position, Quaternion.identity, ExpendMgr.playUi.transform);
        if (tag != "AddPlate")
            needText.text = needGold.ToString();
    }
    private void Update()
    {
        if (ExpendMgr.noTime)
        {
            isMakable = isCreate = true;
            return;
        }
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
        if (ExpendMgr.sellAreaMgr.repository.gameObject.activeSelf
                || ExpendMgr.gm.debugUi.gameObject.activeSelf || ExpendMgr.gm.tutorialUi.gameObject.activeSelf)
            return;

        if (RayCastGoWithTag(gameObject.tag))
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
        CheckDia();
        if(isMakable)
        {
            ExpendMgr.AddPlate();
            isMakable = false;
        }
            
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

    private void CheckDia()
    {
        if (ExpendMgr.gm.totalDiamond >= needDia)
        {
            isMakable = true;
            ExpendMgr.gm.MinusTotalDia(needDia);
        }

    }

    public void SetNeedGold(int amount)
    {
        needGold = Mathf.Clamp(needGold, needGold + amount, maxIncreseGold);
        needText.text = needGold.ToString();
    }

    public void SetNeedDia(int amount)
    {
        needDia = Mathf.Clamp(needDia, needDia + amount, maxIncreseDia);
        needText.text = needDia.ToString();
    }

    public void SetNeedTime(float amount)
    {
        endTime += amount;
    }
}
