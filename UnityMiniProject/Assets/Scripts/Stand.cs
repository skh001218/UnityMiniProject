using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static SellItemTable;

public class Stand : MonoBehaviour
{
    public Repository repository;
    public GameManager gameManager;
    public StandMgr standMgr;
    public Transform wayPoint;
    public float magY = 50f;
    public SpriteRenderer image;

    public int itemCount;
    public SellItemData data;

    public TextMeshProUGUI tempCountText;

    private void Start()
    {
        repository = FindObjectOfType<Repository>(includeInactive: true);
        image.gameObject.SetActive(false);
    }
    private void Update()
    {
        
        //tempCountText.text = itemCount.ToString();
#if !UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
              
            if (gameManager.sellAreaMgr.isDrag || repository.gameObject.activeSelf 
                || gameManager.debugUi.gameObject.activeSelf || gameManager.tutorialUi.gameObject.activeSelf)
                return;
            var wPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(wPos, Vector2.zero, 1, LayerMask.GetMask("Stand"));
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Debug.Log(transform.GetSiblingIndex());
                repository.OpenRepository(transform.GetSiblingIndex());
            }
        }

#elif UNITY_ANDROID
        
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (gameManager.sellAreaMgr.isDrag || repository.gameObject.activeSelf 
                || gameManager.debugUi.gameObject.activeSelf || gameManager.tutorialUi.gameObject.activeSelf)
                return;
            var wPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            var hit = Physics2D.Raycast(wPos, Vector2.zero, 1, LayerMask.GetMask("Stand"));
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (hit.collider.gameObject.tag == tag)
                {
                    repository.OpenRepository(transform.GetSiblingIndex());
                }
            }
        }
#endif
    }

    public void PickUpItem()
    {
        itemCount--;
        image.sprite = DataTableManager.SellItemTable.Get(data.ID).IconSprite(itemCount);
        if (itemCount == 0)
        {
            data = null;
            image.sprite = null;
        }
    }

    public void SetItemData(SellItemData data)
    {
        this.data = data;
        image.gameObject.SetActive(true);
        image.sprite = this.data.IconSprite(5);
        itemCount = 5;
    }

    public void SetWayPoint(Transform parent)
    {
        GameObject go = new GameObject("wayPoint");
        go.transform.SetParent(parent);
        go.transform.position = gameObject.transform.position;
        go.transform.tag = "wp";
        wayPoint = go.transform;
        wayPoint.position = new Vector2(wayPoint.position.x, wayPoint.position.y - magY);
    }
}
