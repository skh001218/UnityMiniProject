using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> areaList;
    public GameObject test2;
    public Guest guestPrefeb;
    public GameObject[] arrows;
    private int curArea = 0;

    private float spawnGuestTime = 5f;
    private float guestTime = 0f;

    private List<Guest> guests = new List<Guest>();

    private int size = 15;
    private UnityEngine.Color color = UnityEngine.Color.red;
    private float deltaTime = 0f;

    private void Start()
    {
        Application.targetFrameRate = -1;

        //가로 고정 ( 화면에 따라 위 아래가 더 찍힘 )
        arrows = GameObject.FindGameObjectsWithTag("Arrow");
        Vector2 temp = Camera.main.ScreenToWorldPoint(new Vector3(Screen.safeArea.x, Screen.safeArea.y));

        while (temp.x > test2.GetComponent<BoxCollider2D>().bounds.min.x)
        {
            temp = Camera.main.ScreenToWorldPoint(new Vector3(Screen.safeArea.x, Screen.safeArea.y));
            Camera.main.orthographicSize += 0.1f;
        }
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        guestTime += Time.deltaTime;

        if (guestTime > spawnGuestTime)
        {
            Guest go = Instantiate(guestPrefeb);
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
    }

    public void MoveNextArea()
    {
        
        arrows[1].SetActive(true);
        float xPos = Mathf.Abs(areaList[curArea + 1].transform.position.x - areaList[curArea].transform.position.x);
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + xPos, 0f, -10f);
        curArea++;
    }

    public void MoveBeforeArea()
    {
        arrows[0].SetActive(true);
        float xPos = Mathf.Abs(areaList[curArea].transform.position.x - areaList[curArea - 1].transform.position.x);
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - xPos, 0f, -10f);
        curArea--;
    }

    public void RemoveGuest(Guest guest)
    {
        guests.Remove(guest);
    }

    private void RandomSelectGuest()
    {
        int idx = Random.Range(0, guests.Count);
        if(!guests[idx].isSelect)
            guests[idx].isSelect = true;
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, Screen.width, Screen.height);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = size;
        style.normal.textColor = color;

        float ms = deltaTime * 1000f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.} FPS ({1:0.0} ms)", fps, ms);

        GUI.Label(rect, text, style);
    }
}
