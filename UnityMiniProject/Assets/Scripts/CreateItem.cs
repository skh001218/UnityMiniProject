using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateItem : MonoBehaviour
{
    public GameObject item;
    public Slider slider;
    private readonly float baseTime = 5f;
    private float createTime;

    public List<ItemBase> itemBases;

    private void Update()
    {
        createTime += Time.deltaTime;
        if (createTime > baseTime)
        {
            foreach (var itemBase in itemBases)
            {
                if (itemBase.item == null)
                {
                    itemBase.item = Instantiate(item);
                    itemBase.item.transform.localPosition = itemBase.transform.position;
                    itemBase.item.transform.parent = itemBase.transform;
                    break;
                }
            }
            createTime = 0f;
        }
        slider.value = createTime / baseTime;
        if(slider.value <= 0f)
            slider.fillRect.gameObject.SetActive(false);
        else 
            slider.fillRect.gameObject.SetActive(true);

        slider.GetComponentInChildren<TextMeshProUGUI>().text = $"{(int)(baseTime - createTime) + 1}s";

        CheckItem();
    }

    private void CheckItem()
    {
        List<ItemBase> temp = itemBases.Where(n => n.item == null).ToList();
        if (temp.Count <= 0)
        {
            slider.gameObject.SetActive(false);
        }
        else
        {
            slider.gameObject.SetActive(true);
        }

    }
}
