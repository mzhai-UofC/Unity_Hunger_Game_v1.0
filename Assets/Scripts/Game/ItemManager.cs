using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public enum ItemType
{
    wood,
    meat,
    CookedMeat,
    Campfire,
    berry
}

[System.Serializable]
public class ItemDefine
{
    public ItemType ItemType;
    public Sprite Sprite;
    public GameObject Prefab;
    public bool CanUse;

    public float HP;
    public float Hungry;
}

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    public ItemDefine[] ItemDefines;


    

    private void Awake()
    {
        Instance = this;
     
    }

    public ItemDefine GetItemDefine(string itemTypeName)
    {
        return GetItemDefine(Enum.Parse<ItemType>(itemTypeName));
    }

    public ItemDefine GetItemDefine(ItemType itemType)
    {
        return ItemDefines[(int)itemType];
    }
}
