using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static SellItemTable;

public class WeaponTable : DataTable
{

    public class WeaponData
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
        public float ProductionTime { get; set; }
        public int DropCount { get; set; }
        public float BakingTime { get; set; }
        public int Sale { get; set; }
        public string ItemID { get; set; }
        public string EffectID { get; set; }
        public string RecipeID { get; set; }
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

    private Dictionary<string, WeaponData> items = new();
    public static WeaponData empty = new WeaponData();
    public int itemCount = 0;

    public override void Load(string filename)
    {
        var path = string.Format(FormatPath, filename);

        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<WeaponData>(textAsset.text);
        items.Clear();
        list.ForEach(x =>
        {
            if (!items.ContainsKey((x.ID)))
            {
                items.Add((x.ID), x);
            }
            else Debug.Log("Å° Áßº¹");
        }
        );
        itemCount = items.Count;
    }

    public WeaponData Get(string key)
    {
        if (!items.ContainsKey(key))
        {
            return empty;
        }
        return items[key];
    }

    public WeaponData GetToLevelAndKind(int level, int kind)
    {
        foreach (var item in items)
        {
            if (item.Value.Level == level && item.Value.Kind == kind)
                return item.Value;
        }
        return empty;
    }
}
