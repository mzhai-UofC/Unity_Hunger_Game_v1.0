using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPanel : MonoBehaviour
{
    public static BagPanel Instance;
    [SerializeField] GameObject ItemPrefab;

    private UI_BagPanel_Item[] items = new UI_BagPanel_Item[6];
    public Transform DragLayer;

    private void Awake()
    {
        Instance = this;

     }
    void Start()
    {
        items = new UI_BagPanel_Item[5];
        UI_BagPanel_Item item = Instantiate(ItemPrefab, transform).GetComponent<UI_BagPanel_Item>();
        item.Init(ItemManager.Instance.GetItemDefine(ItemType.Campfire));
        items[0] = item;

        for (int i = 1; i < 5; i++)
        {
            item = Instantiate(ItemPrefab, transform).GetComponent<UI_BagPanel_Item>();
            item.Init(null);
            items[i] = item;
        }
    }


    public bool AddItem(ItemType itemType)
    {
        //check if there is empty space
        for (int i = 0; i < items.Length; i++)
        {
            //empty
            if (items[i].itemDefine == null)
            {
                ItemDefine itemDefine = ItemManager.Instance.GetItemDefine(itemType);
                items[i].Init(itemDefine);
                return true;
            }
        }
        return false;
    }
}
