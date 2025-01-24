using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public List<GameObject> areaList;
    public GameObject test2;
    private int curArea = 0;

    public void MoveNextArea()
    {
        if (curArea >= areaList.Count - 1)
            return;
        float xPos = Mathf.Abs(areaList[curArea + 1].transform.position.x - areaList[curArea].transform.position.x);
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + xPos, 0f, -10f);
        curArea++;
    }

    public void MoveBeforeArea()
    {
        if (curArea == 0)
            return;
        float xPos = Mathf.Abs(areaList[curArea].transform.position.x - areaList[curArea - 1].transform.position.x);
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - xPos, 0f, -10f);
        curArea--;
    }
}
