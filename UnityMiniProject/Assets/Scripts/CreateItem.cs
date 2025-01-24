using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static WeaponTable;

public class CreateItem : MonoBehaviour
{
    public Slider slider;
    public Image sliderItemImage;
    public List<Weapon> combineWp = new List<Weapon>();

    private readonly float baseTime = 2f;
    private float createTime;
    private WeaponData createItem;

    public Weapon selectWp;

    public List<ItemBase> itemBases;

    private void Start()
    {
        RandomData();
    }

    private void Update()
    {
        
        if (createTime > baseTime)
        {
            foreach (var itemBase in itemBases)
            {
                if (itemBase.item.data == null)
                {
                    itemBase.item.SetData(createItem);
                    RandomData();
                    break;
                }
            }
            createTime = 0f;
        }
        slider.value = createTime / baseTime;
        if(slider.value <= 0f)
        {
            slider.fillRect.gameObject.SetActive(false);
        }
        else
        {
            slider.fillRect.gameObject.SetActive(true);
        }
            

        slider.GetComponentInChildren<TextMeshProUGUI>().text = $"{(int)(baseTime - createTime) + 1}s";

        CheckItem();
    }

    private void CheckItem()
    {
        List<ItemBase> temp = itemBases.Where(n => n.item.data == null).ToList();
        if (temp.Count <= 0)
        {
            
            slider.gameObject.SetActive(false);
        }
        else
        {
            createTime += Time.deltaTime;
            slider.gameObject.SetActive(true);
        }

    }

    public void CombineItem()
    {
        if (combineWp.Count <= 1)
            return;

        Weapon combine1 = combineWp.Find(n => n == selectWp);
        Weapon combine2 = combineWp.Find(n => n != selectWp);

        if (!(combine1.data.Level == combine2.data.Level && combine1.data.Kind == combine2.data.Kind)
            || DataTableManager.WeaponTable.Get((combine2.data.Kind, combine2.data.Level + 1)) == empty)
            return;

        combine1.SetDataEmpty();
        combine2.SetData(DataTableManager.WeaponTable.Get((combine2.data.Kind, combine2.data.Level + 1)));

        combineWp.Clear();
    }

    private void RandomData()
    {
        createItem = DataTableManager.WeaponTable.Get((1, Random.Range(1, 1)));
        sliderItemImage.sprite = createItem.IconSprite;
    }
}
