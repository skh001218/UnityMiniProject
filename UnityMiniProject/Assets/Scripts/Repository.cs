using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Repository : MonoBehaviour
{
    private readonly string plateTag = "Plate";
    private List<Plate> plates = new List<Plate>();

    public int connectStandIdx = -1;

    public void UpdatePlate()
    {
        plates = FindObjectsByType<Plate>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
    }
    public bool SetItemInPlate(string id)
    {
        foreach (Plate plate in plates)
        {
            if(plate.GetItem() == null)
            {
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

    public void OnClickDeploy()
    {
        gameObject.SetActive(false);
        Debug.Log(connectStandIdx);
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
