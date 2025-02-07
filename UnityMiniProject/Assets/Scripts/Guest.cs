using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static SellItemTable;
using Random = UnityEngine.Random;

public class Guest : MonoBehaviour
{
    public enum Status
    {
        MoveBuy,
        MoveNotBuy,
        StopBuy,
        StopNotBuy,
        Max
    }

    public GameManager gm;
    public bool isSelect = false;
    public bool isRight;
    public float speed = 25f;
    public StandMgr standMgr;

    public GameObject wayPoint;
    public List<Transform> wayPoints = new List<Transform>();
    public int posNum = 0;
    private int prePosNum = -1;
    public GameObject rightPos;
    public GameObject leftPos;
    private float endPos;

    private float prePosX;
    private Status status = Status.MoveNotBuy;

    private bool isCal;

    private List<SellItemData> buyItems = new List<SellItemData>();
    private void Start()
    {
        /*for (int i = 0; i < wayPoint.transform.childCount; i++)
        {
            wayPoints.Add(wayPoint.transform.GetChild(i));
        }*/

        int temp = Random.Range(0, 2);
        if (temp == 0)
        {
            transform.position = new Vector2(rightPos.transform.position.x, rightPos.transform.position.y + 10f); 
            isRight = false;
            endPos = leftPos.transform.position.x;
        }
        else
        {
            transform.position = new Vector2(leftPos.transform.position.x, leftPos.transform.position.y - 10f);
            isRight = true;
            endPos = rightPos.transform.position.x;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        prePosX = transform.position.x;
    }

    private void Update()
    {

        if (!isSelect)
        {
            Move();
        }
        else
        {
            if (isCal)
                return;

            if(transform.position.x > wayPoints[posNum].position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (transform.position.x != wayPoints[0].position.x && posNum == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    new Vector2(wayPoints[posNum].position.x, transform.position.y), speed * Time.deltaTime);
                return;
            }


            
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[posNum].position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, wayPoints[posNum].position) < 0.1f)
            {
                
                if (gm.CheckWayPoint(this) && posNum > wayPoints.Count - 6 && posNum > wayPoints.Count - 2)
                    return;

                if(prePosNum == posNum || speed == 0)
                {
                    return;
                }

                if (standMgr.stands.Where(n => n.wayPoint == wayPoints[posNum]).Count() > 0)
                    status = CheckStand(standMgr.stands.Where(n => n.wayPoint == wayPoints[posNum]).First());
                else
                    status = Status.MoveNotBuy;

                if(status == Status.MoveNotBuy || status == Status.MoveBuy)
                {
                    prePosNum = posNum;
                    posNum++;
                }
                    
            }

            if (posNum == wayPoints.Count)
            {
                gm.RemoveGuest(this);
                Destroy(gameObject);
            }
        }

        if( (isRight && transform.position.x >= endPos) || (!isRight && transform.position.x <= endPos) )
        {
            gm.RemoveGuest(this);
            Destroy(gameObject);
        }
           

    }

    private void Move()
    {
        Vector2 pos = transform.position;
        if (isRight)
            pos.x += speed * Time.deltaTime;
        else
            pos.x -= speed * Time.deltaTime;

        transform.position = pos;
    }

    public Status CheckStand(Stand stand)
    {
        if (stand.itemCount == 0)
            return Status.MoveNotBuy;

        Status ranStat = (Status)Random.Range(0, (int)Status.Max);
        Debug.Log(ranStat);
        switch (ranStat)
        {
            case Status.MoveBuy:
                // 해당 진열대에서 아이템 갯수 감소
                stand.PickUpItem();
                buyItems.Add(stand.data);
                break;
            case Status.MoveNotBuy:
                // 처리 필요 x
                break;
            case Status.StopBuy:
                // 해당 진열대에 머물기
                StartCoroutine(StopGuest());
                // 해당 진열대 아이템 갯수 감소
                stand.PickUpItem();
                // 보유 아이템 추가
                buyItems.Add(stand.data);
                break;
            case Status.StopNotBuy:
                // 해당 진열대 머물기
                StartCoroutine(StopGuest());
                break;
            case Status.Max:
                break;
        }

        return ranStat;
    }

    IEnumerator StopGuest()
    {
        speed = 0;
        yield return new WaitForSecondsRealtime(2.0f);
        // 함수 내용
        speed = 25f;
        prePosNum = posNum;
        posNum++;
    }

    public void SetWayPoint()
    {
        wayPoints = standMgr.wayPoints;
    }
}
