using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class Defines 
{
    public static class DataTableIds
    {
        public static readonly string weapon = "DoughTable";
        public static readonly string sellItem = "ItemTable";
    }

    public static bool RayCastGoWithTag(string tag)
    {
        RaycastHit hitData;
        var wPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = new Ray(wPos, Vector3.forward);

        Debug.Log(tag);

        if (Physics.Raycast(ray, out hitData, 10) && hitData.collider.tag == tag)
        {
            Debug.Log(hitData.collider.tag);
            return true;
        }
        /*var wPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.Raycast(wPos, Vector2.zero, 1);

        Debug.Log(tag);
        Debug.Log(hit.collider.tag);

        if (hit.collider != null && hit.collider.tag == tag)
        {
            
            return true;
        }*/
        return false;
    }
}


