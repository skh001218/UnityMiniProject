using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO.Pipes;
using System.Linq;
using UnityEngine;
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
        if (data == null)
            return;
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
        itemMgr.MoveItem();
        itemMgr.CombineItem();
        itemMgr.selectWp = null;
        if(itemMgr.collideFur.Count > 0 && data != null)
            collideFur = itemMgr.collideFur.Last();
        if (collideFur != null && !collideFur.IsBake)
        {
            Debug.Log(data.ID);
            collideFur.BakeItem(this);
            data = null;
            itemImage.sprite = null;
        }
        
        transform.position = originPos;
        itemImage.sortingLayerName = beforeLayer;
        itemMgr.moveWp.Clear();
        itemMgr.collideFur.Clear();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "Furnace")
        {
            itemMgr.collideFur.Add(collision.gameObject.GetComponent<Furnace>());
        }
        //Weapon otherWp = collision.gameObject.GetComponent<Weapon>();
        if(data != null && collision.gameObject.tag != "Furnace") 
            itemMgr.combineWp.Add(this);
        if (data == null && collision.gameObject.tag != "Furnace")
            itemMgr.moveWp.Add(this);

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collideFur = null;
        itemMgr.collideFur.Clear();
        itemMgr.combineWp.Remove(this);
    }

    public void SetData(string data)
    {
        this.data = DataTableManager.WeaponTable.Get(data);
        itemImage.sprite = this.data.IconSprite;
    }

    public void SetDataEmpty()
    {
        this.data = null;
        itemImage.sprite = null;
    }

}
