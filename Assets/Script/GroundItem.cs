using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Author : Nguyen  Huu Hung Long
//Last modify date : 20/11/2021
//Script handling item spawning in
//Held by every item within the scene
public class GroundItem : MonoBehaviour , ISerializationCallbackReceiver
{
    public ItemObject item;

    public void OnAfterDeserialize()
    {
        
    }

    public void OnBeforeSerialize()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
        EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
    }
}
