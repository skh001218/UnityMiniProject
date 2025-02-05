using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

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

        public string Id { get; set; }
        public int Level { get; set; }
        public int Kind { get; set; }
        public float BakeTime { get; set; }
        /*public bool Useable { get; set; }*/
        public int Price { get; set; }
        public string Root { get; set; }

        public Sprite IconSprite
        {
            get
            {
                var sprite = Resources.Load<Sprite>($"{string.Format(imagePath, Root)}");
                return sprite;
            }
        }
    }

    private Dictionary<(int, int), WeaponData> items = new();
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
            if (!items.ContainsKey((x.Kind, x.Level)))
            {
                items.Add((x.Kind, x.Level), x);
            }
            else Debug.Log("Å° Áßº¹");
        }
        );
        itemCount = items.Count;
    }

    public WeaponData Get((int, int) key)
    {
        if (!items.ContainsKey(key))
        {
            return empty;
        }
        return items[key];
    }
}
