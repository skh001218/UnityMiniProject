using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Defines;

public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> tables = new();

    static DataTableManager()
    {
        /*foreach (var id in DataTableIds.String)
        {
            var table = new StringTable();
            table.Load(id);
            tables.Add(id, table);
        }*/

        var weaponTable = new WeaponTable();
        var weaponTableId = DataTableIds.weapon;
        weaponTable.Load(weaponTableId);
        tables.Add(weaponTableId, weaponTable);

        var sellItemTable = new SellItemTable();
        var sellItemTableId = DataTableIds.sellItem;
        sellItemTable.Load(sellItemTableId);
        tables.Add(sellItemTableId, sellItemTable);
    }

    public static T Get<T>(string id) where T : DataTable
    {
        if (!tables.ContainsKey(id))
        {

        }
        return tables[id] as T;
    }

    public static WeaponTable WeaponTable
    {
        get
        {
            return Get<WeaponTable>(DataTableIds.weapon);
        }
    }

    public static SellItemTable SellItemTable
    {
        get
        {
            return Get<SellItemTable>(DataTableIds.sellItem);
        }
    }
}
