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

    private bool isCollision = false;
    private Furnace collideFur;
    private Vector2 originPos;

    private void Start()
    {
        originPos = transform.position;
        
    }

    private void OnMouseDown()
    {
        itemMgr.selectWp = this;
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
        if (collideFur != null)
        {
            collideFur.BakeItem(this);
            data = null;
            itemImage.sprite = null;
        }
        transform.position = originPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isCollision = true;
        if(collision.gameObject.tag == "Furnace")
        {
            collideFur = collision.gameObject.GetComponent<Furnace>();
        }
        //Weapon otherWp = collision.gameObject.GetComponent<Weapon>();
        itemMgr.combineWp.Add(this);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isCollision = false;
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
}
