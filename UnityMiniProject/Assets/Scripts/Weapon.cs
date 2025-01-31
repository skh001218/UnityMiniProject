using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static WeaponTable;

public class Weapon : MonoBehaviour
{
    public WeaponData data;
    public SpriteRenderer itemImage;
    public CreateItem itemMgr;

    public Furnace collideFur;
    private Vector2 originPos;
    private readonly string topItemLayer = "TopItem";
    private readonly string beforeLayer = "Item";

    private void Start()
    {
        originPos = transform.position;
    }

    private void Update()
    {
        //touchMove();
    }

    private void OnMouseDown()
    {
        itemMgr.selectWp = this;
        itemImage.sortingLayerName = topItemLayer;
    }

    private void OnMouseDrag()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;
    }

    private void OnMouseUp()
    {
        itemMgr.CombineItem();
        itemMgr.selectWp = null;
        if (collideFur != null && !collideFur.IsBake)
        {
            collideFur.BakeItem(this);
            data = null;
            itemImage.sprite = null;
        }
        transform.position = originPos;
        itemImage.sortingLayerName = beforeLayer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Furnace")
        {
            collideFur = collision.gameObject.GetComponent<Furnace>();
        }
        //Weapon otherWp = collision.gameObject.GetComponent<Weapon>();
        if(data != null && collision.gameObject.tag != "Furnace") 
            itemMgr.combineWp.Add(this);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collideFur = null;
        itemMgr.combineWp.Remove(this);
    }

    public void SetData(WeaponData data)
    {
        this.data = data;
        itemImage.sprite = this.data.IconSprite;
    }

    public void SetDataEmpty()
    {
        this.data = null;
        itemImage.sprite = null;
    }

    private void touchMove()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnMouseDown();
                    break;
                case TouchPhase.Stationary:
                case TouchPhase.Moved:
                    OnMouseDrag();
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    OnMouseUp();
                    break;
            }
        }
    }
}
