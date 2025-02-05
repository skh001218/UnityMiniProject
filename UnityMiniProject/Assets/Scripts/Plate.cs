using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static SellItemTable;

public class Plate : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private SellItemData data;
    public bool isSelect;
    public Button deploy;

    public Repository repository;

    private void Start()
    {
        deploy = GetComponentInChildren<Button>(includeInactive: true);
        repository = FindObjectOfType<Repository>(includeInactive: true);

        deploy.onClick.AddListener(repository.OnClickDeploy);
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
            Debug.Log(data.Id);
        }
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
        repository.SelectCancelAll();
        isSelect = true;
    }
}
