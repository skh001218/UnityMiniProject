using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static SellItemTable;

public class StandMgr : MonoBehaviour
{
    public List<Stand> stands = new List<Stand>();
    public List<Transform> wayPoints = new List<Transform>();
    public List<Transform> StartWayPoints = new List<Transform>();
    public List<Transform> LastWayPoints = new List<Transform>();
    public GameObject wayPointList;
    public Transform addStandWayPoint;

    private void Start()
    {
        
        SetWayPoint();
    }

    public void SetWayPoint()
    {
        foreach(var wayPoint in wayPoints)
        {
            if (wayPoint.tag == "wp")
                Destroy(wayPoint.transform.gameObject);
        }
        wayPoints.Clear();

        stands = GetComponentsInChildren<Stand>().ToList();
        stands.Sort((n1, n2) => n1.transform.GetSiblingIndex().CompareTo(n2.transform.GetSiblingIndex()));

        foreach (Stand stand in stands)
        {
            stand.SetWayPoint(wayPointList.transform);
        }

        foreach (Transform t in StartWayPoints)
        {
            wayPoints.Add(t);
        }
        

        for (int i = 0; i < stands.Count; i++)
        {
            wayPoints.Add(stands[i].wayPoint);
            float mag;
            if((i + 1) % 5 == 0 || i + 1 == stands.Count)
            {
                GameObject tempGo = new GameObject("wayPoint");
                Transform temp = tempGo.transform;
                temp.SetParent(wayPointList.transform);
                temp.position = stands[i].wayPoint.position;
                temp.tag = "wp";
                mag = 50f;//Mathf.Abs(stands[i].wayPoint.position.x - stands[i - 1].wayPoint.position.x);
                if ((i / 5) % 2 != 0)
                {
                    temp.position = new Vector2(temp.position.x + mag, temp.position.y);
                    wayPoints.Add(temp);

                    GameObject lastPoint = new GameObject("wayPoint");
                    Transform lastTrans = lastPoint.transform;
                    lastTrans.SetParent(wayPointList.transform);
                    lastTrans.position = temp.position;
                    lastTrans.tag = "wp";
                    lastTrans.position = new Vector2(LastWayPoints[0].position.x, lastTrans.position.y);
                    wayPoints.Add(lastTrans);
                }
                else
                {
                    if(i + 1 == stands.Count && addStandWayPoint != null)
                    {
                        wayPoints.Add(addStandWayPoint);
                    }
                    if (i + 1 == stands.Count)
                        temp.position = new Vector2(temp.position.x - mag * 2, temp.position.y);
                    else
                        temp.position = new Vector2(temp.position.x - mag, temp.position.y);
                    wayPoints.Add(temp);

                    if (i + 1 == stands.Count)
                    {
                        GameObject lastPoint = new GameObject("wayPoint");
                        Transform lastTrans = lastPoint.transform;
                        lastTrans.SetParent(wayPointList.transform);
                        lastTrans.position = temp.position;
                        lastTrans.tag = "wp";
                        lastTrans.position = new Vector2(lastTrans.position.x, lastTrans.position.y + stands[i].magY * 2);
                        wayPoints.Add(lastTrans);

                        GameObject lastPoint2 = new GameObject("wayPoint");
                        Transform lastTrans2 = lastPoint2.transform;
                        lastTrans2.SetParent(wayPointList.transform);
                        lastTrans2.position = lastTrans.position;
                        lastTrans2.tag = "wp";
                        lastTrans2.position = new Vector2(LastWayPoints[0].position.x, lastTrans2.position.y);
                        wayPoints.Add(lastTrans2);
                    }


                }
                    
            }
        }
       
        foreach (Transform t in LastWayPoints)
        {
            wayPoints.Add(t);
        }
    }
    

    public void SetStandItem(int standIdx, SellItemData data)
    {
        stands[standIdx].SetItemData(data);
    }
}
