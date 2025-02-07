using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class SellItemTable : DataTable
{

    public class SellItemData
    {
        public enum WeaponType
        {
            Sword,
            Arrow
        }
        private readonly string imagePath = "Sprite/{0}";

        public string ID { get; set; }
        public string Division { get; set; }
        public int Level { get; set; }
        public int Kind { get; set; }
        public int AcquisitionsCount { get; set; }
        public int SwordStorage { get; set; }
        public int Overlaps { get; set; }
        public string DoughID { get; set; }
        public string RecipeID { get; set; }
        public int Price { get; set; }

        public int Sale {  get; set; }
        public string ImageFileName { get; set; }

        public Sprite IconSprite
        {
            get
            {
                var sprite = Resources.Load<Sprite>($"{string.Format(imagePath, ImageFileName)}");
                return sprite;
            }
        }
    }

    private Dictionary<string, SellItemData> items = new();
    public static SellItemData empty = new SellItemData();
    public int itemCount = 0;

    public override void Load(string filename)
    {
        var path = string.Format(FormatPath, filename);

        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<SellItemData>(textAsset.text);
        items.Clear();
        list.ForEach(x =>
        {
            if (!items.ContainsKey(x.ID))
            {
                items.Add(x.ID, x);
            }
            else Debug.Log("Å° Áßº¹");
        }
        );
        itemCount = items.Count;
    }

    public SellItemData Get(string key)
    {
        if (!items.ContainsKey(key))
        {
            return empty;
        }
        return items[key];
    }

    public SellItemData GetToBakeItem(string key)
    {
        foreach (var item in items)
        {
            if (item.Value.DoughID == key)
                return item.Value;
        }
        return empty;
    }
}
