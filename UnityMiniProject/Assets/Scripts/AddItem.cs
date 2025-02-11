using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Defines;

public class AddItem : MonoBehaviour
{
    public ExpendMgr ExpendMgr;

    private void Update()
    {
        
        
    }

    private void OnMouseUp()
    {
        if(RayCastGoWithTag(gameObject.tag))
        {
            switch(gameObject.tag)
            {
                case "AddBase":
                    ExpendMgr.AddBase();
                    break;
                case "AddFurnace":
                    ExpendMgr.AddFurnace();
                    break;
                case "AddStand":
                    ExpendMgr.AddStand();
                    break;
                default:
                    break;
            }
        }
        
    }
}
