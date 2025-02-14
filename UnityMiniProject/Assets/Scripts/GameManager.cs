using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public List<GameObject> areaList;
    public Guest[] guestPrefeb;
    public GameObject[] arrows;
    public GameObject topUi;

    public SellAreaMgr sellAreaMgr;
    public SellItemMgr sellItemMgr;
    public StandMgr standMgr;
    public TutorialUi tutorialUi;
    public DebugUi debugUi;

    private int curArea = 0;

    public float spawnGuestTime = 5f;
    private float guestTime = 0f;

    private List<Guest> guests = new List<Guest>();

    private int size = 100;
    private UnityEngine.Color color = UnityEngine.Color.red;
    private float deltaTime = 0f;

    private float selectRandomTime = 15f;
    private float selectTime = 0f;

    public int totalGold;
    public int totalDiamond;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI DiaText;

    private void Start()
    {
        Application.targetFrameRate = 60;

        Camera camera = Camera.main;
        Rect rect = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / (9f / 16f);
        float scalewidth = 1f / scaleheight;

        // 위 아래 공백 생성 (휴대폰이 날씬한 경우)
        if (scaleheight < 1)
        {
            Camera.main.orthographicSize = Screen.height / (Screen.width / 16f) * 9f;
        }
        // 좌 우 공백 생성 (휴대폰이 뚱뚱한 경우)
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }
        camera.rect = rect;

        Vector2 safeAreaPos = new Vector2(topUi.transform.position.x,
            Screen.safeArea.position.y + Screen.safeArea.height);
        topUi.transform.position = safeAreaPos;

        //가로 고정 ( 화면에 따라 위 아래가 더 찍힘 )
        arrows = GameObject.FindGameObjectsWithTag("Arrow");

        sellAreaMgr.SetCameraArea();
        MoveNextArea();
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
            go.standMgr = standMgr;
            go.sellItemMgr = sellItemMgr;
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
        else
        {
            selectTime = 0;
            return;
        }

        guests[idx].SetWayPoint();
        selectTime = 0;

    }

    public bool CheckWayPoint(Guest guest)
    {
        int cnt = guests.Where(n => n != guest && n.posNum == guest.posNum + 1).Count();

        return cnt > 0;
    }

    public void SetTotalGold(int price)
    {
        totalGold += price;
        goldText.text = totalGold.ToString();
    }

    public void MinusTotalGold(int price)
    {
        totalGold -= price;
        goldText.text = totalGold.ToString();
    }

    public void SetTotalDia(int price)
    {
        totalDiamond += price;
        DiaText.text = totalDiamond.ToString();
    }

    public void MinusTotalDia(int price)
    {
        totalDiamond -= price;
        Debug.Log(price);
        Debug.Log(totalDiamond);
        DiaText.text = totalDiamond.ToString();
    }

    

    private void OnGUI()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        /*GUIStyle style = new GUIStyle();

        Rect rect = Screen.safeArea;
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = size;
        style.normal.textColor = color;

        float ms = deltaTime * 1000f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.} FPS ({1:0.0} ms)", fps, ms);

        GUI.Label(rect, text, style);*/
#endif
    }
}
