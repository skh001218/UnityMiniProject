using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> areaList;
    public GameObject test2;
    public Guest[] guestPrefeb;
    public GameObject[] arrows;
    private int curArea = 0;

    private float spawnGuestTime = 5f;
    private float guestTime = 0f;

    private List<Guest> guests = new List<Guest>();

    private int size = 100;
    private UnityEngine.Color color = UnityEngine.Color.red;
    private float deltaTime = 0f;

    private float selectRandomTime = 15f;
    private float selectTime = 0f;

    private void Start()
    {
        Application.targetFrameRate = -1;

        /*int setWidth = 1080; // 사용자 설정 너비
        int setHeight = 1920; // 사용자 설정 높이

        int deviceWidth = Screen.width; // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장

        Screen.SetResolution(setWidth, setHeight, true);*/

        


        //가로 고정 ( 화면에 따라 위 아래가 더 찍힘 )
        arrows = GameObject.FindGameObjectsWithTag("Arrow");
        Vector2 temp = Camera.main.ScreenToWorldPoint(new Vector3(Screen.safeArea.x, Screen.safeArea.y));
        
        if(temp.x > test2.GetComponent<BoxCollider2D>().bounds.min.x)
        {
            while (temp.x > test2.GetComponent<BoxCollider2D>().bounds.min.x)
            {
                temp = Camera.main.ScreenToWorldPoint(new Vector3(Screen.safeArea.x, Screen.safeArea.y));
                Camera.main.orthographicSize += 0.1f;
            }
        }
        /*else if(temp.x < test2.GetComponent<BoxCollider2D>().bounds.min.x)
        {
            while (temp.x < test2.GetComponent<BoxCollider2D>().bounds.min.x)
            {
                temp = Camera.main.ScreenToWorldPoint(new Vector3(Screen.safeArea.x, Screen.safeArea.y));
                Camera.main.orthographicSize -= 0.1f;
            }
        }*/
        
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        guestTime += Time.deltaTime;
        selectTime += Time.deltaTime;

        if (guestTime > spawnGuestTime)
        {
            int idx = Random.Range(0, guestPrefeb.Length);
            Guest go = Instantiate(guestPrefeb[idx]);
            go.gameObject.SetActive(true); // 임시
            go.transform.SetParent(areaList[1].transform);
            go.transform.localScale = Vector3.one;
            guestTime = 0f;
            guests.Add(go);
        }

        if (curArea == 0)
        {
            arrows[0].SetActive(false);
            arrows[1].SetActive(true);
        }
        if (curArea >= areaList.Count - 1)
        {
            arrows[0].SetActive(true);
            arrows[1].SetActive(false);
        }

        if(selectTime >= selectRandomTime)
        {
            RandomSelectGuest();
        }
    }

    public void MoveNextArea()
    {
        
        arrows[1].SetActive(true);
       
        float xPos = Mathf.Abs(areaList[curArea + 1].transform.position.x - areaList[curArea].transform.position.x);
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + xPos + 10f, 0f, -10f);
        curArea++;
    }

    public void MoveBeforeArea()
    {
        arrows[0].SetActive(true);
        float xPos = Mathf.Abs(areaList[curArea].transform.position.x - areaList[curArea - 1].transform.position.x);
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - xPos - 10f, 0f, -10f);
        curArea--;
    }

    public void RemoveGuest(Guest guest)
    {
        guests.Remove(guest);
    }

    private void RandomSelectGuest()
    {
        if (guests.Count < 1)
            return;
        int idx = Random.Range(0, guests.Count);
        if(!guests[idx].isSelect)
            guests[idx].isSelect = true;
    }

    public bool CheckWayPoint(Guest guest)
    {
        int cnt = guests.Where(n => n != guest && n.posNum == guest.posNum + 1).Count();

        return cnt > 0;
    }

    private void OnGUI()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        GUIStyle style = new GUIStyle();

        Rect rect = Screen.safeArea;
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = size;
        style.normal.textColor = color;

        float ms = deltaTime * 1000f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.} FPS ({1:0.0} ms)", fps, ms);

        GUI.Label(rect, text, style);
#endif
    }
}
