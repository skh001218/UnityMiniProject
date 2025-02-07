using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static SellItemTable;

public class Repository : MonoBehaviour
{
    private readonly string plateTag = "Plate";
    private List<Plate> plates = new List<Plate>();
    public StandMgr standMgr;

    public int connectStandIdx = -1;

    public void UpdatePlate()
    {
        plates = FindObjectsByType<Plate>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        plates.Sort((p1, p2) => p1.transform.GetSiblingIndex().CompareTo(p2.transform.GetSiblingIndex()));
    }
    public bool SetItemInPlate(string id)
    {
        foreach (Plate plate in plates)
        {
            if(plate.GetItem() == null)
            {
                Debug.Log(id);
                plate.SetItem(id);
                return true;
            }
        }
        return false;
    }

    public void OnClickX()
    {
        connectStandIdx = -1;
        gameObject.SetActive(false);
    }

    public void OnDeploy(SellItemData data)
    {
        gameObject.SetActive(false);
        standMgr.SetStandItem(connectStandIdx, data);
    }

    public void SelectCancelAll()
    {
        foreach(Plate plate in plates)
        {
            plate.isSelect = false;
        }
    }

    public void OpenRepository(int connetIdx)
    {
        SelectCancelAll();
        connectStandIdx = connetIdx;
        gameObject.SetActive(true);
    }
}
