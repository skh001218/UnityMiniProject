using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using static SellItemTable;

public class Plate : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public SellItemData data;
    public bool isSelect;
    public Button deploy;
    public Image itemImage;

    public Repository repository;

    private void Start()
    {
        deploy = GetComponentInChildren<Button>(includeInactive: true);
        repository = FindObjectOfType<Repository>(includeInactive: true);

        deploy.onClick.AddListener(OnClickDeploy);
    }

    private void Update()
    {
        if (!isSelect)
            deploy.gameObject.SetActive(false);
        else 
            deploy.gameObject.SetActive(true);
    }

    public void SetItem(string id)
    {
        data = DataTableManager.SellItemTable.GetToBakeItem(id);
        if(data == null || data == empty)
        {
            Debug.LogError("NO Sell Item");
        }
        else
        {
            itemImage.sprite = data.IconSprite(5);
            itemImage.gameObject.SetActive(true);
        }
    }

    private void OnClickDeploy()
    {

        if (!repository.OnDeploy(data))
            return;
        data = null;
        itemImage.sprite = null;
        itemImage.gameObject.SetActive(false);
        //repository.SetPlateOrder();

    }
    public SellItemData GetItem() => data;

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (data == null || data == empty)
            return;

        repository.SelectCancelAll();
        isSelect = true;
    }

    public void addPlate()
    {

    }
}
