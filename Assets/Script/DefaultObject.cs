using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author : Nguyen  Huu Hung Long
//Last modify date : 20/11/2021
//Default format for item 
//Scriptable object 
//
[CreateAssetMenu(fileName ="New Default Object", menuName = "Inventory System/Item/Default")]
public class DefaultObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Default;
    }
}
