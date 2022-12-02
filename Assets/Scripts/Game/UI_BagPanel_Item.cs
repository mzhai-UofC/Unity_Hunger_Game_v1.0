using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_BagPanel_Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image bg;
    public Image icon;

    private bool isSlect;

    public bool IsSlect
    {
        get => isSlect;
        set
        {
            isSlect = value;
            if (isSlect)
            {
                bg.color = Color.green;
            }
            else
            {
                bg.color = Color.white;
            }
        }
    }

    public ItemDefine itemDefine;

    #region 
    public void OnPointerEnter(PointerEventData eventData)
    {
        IsSlect = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsSlect = false;
    }

   
    public void OnBeginDrag(PointerEventData eventData)
    {
        PlayerController.Instance.isDraging = true;
        if (itemDefine == null) return;
        //icon.transform.SetParent(BagPanel.Instance.DragLayer);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (itemDefine == null) return;
       icon.transform.position = eventData.position;
    }
 
    public void OnEndDrag(PointerEventData eventData)
    {
        PlayerController.Instance.isDraging = false;
        if (itemDefine == null) return;
       // icon.transform.SetParent(transform);
        

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            string targetTag = hitInfo.collider.tag;
           icon.transform.localPosition = Vector3.zero;
            switch (itemDefine.ItemType)
            {
                case ItemType.wood:

                    if (targetTag == "Ground")
                    {
                        Instantiate(itemDefine.Prefab, hitInfo.point + new Vector3(0, 1.5f, 0), Quaternion.identity);
                        Init(null);
                    }
                    else if (targetTag == "Campfire")
                    {
                        hitInfo.collider.GetComponent<Campfire>().AddTime();
                        Init(null);
                    }
                    break;
                case ItemType.meat:
                  
                    if (targetTag == "Ground")
                    {
                        Instantiate(itemDefine.Prefab, hitInfo.point + new Vector3(0, 1.5f, 0), Quaternion.identity);
                        Init(null);
                    }
                    else if (targetTag == "Campfire")
                    {
                        Init(ItemManager.Instance.GetItemDefine(ItemType.CookedMeat));
                    }
                    break;
                case ItemType.CookedMeat:
              
                    if (targetTag == "Ground")
                    {
                        Instantiate(itemDefine.Prefab, hitInfo.point + new Vector3(0, 1.5f, 0), Quaternion.identity);
                        Init(null);
                    }
                    else if (targetTag == "Campfire")
                    {
                        hitInfo.collider.GetComponent<Campfire>().AddTime();
                        Init(null);
                    }
                    break;
                case ItemType.Campfire:
                 
                    if (targetTag == "Ground")
                    {
                        Instantiate(itemDefine.Prefab, hitInfo.point, Quaternion.identity);
                        Init(null);
                    }
                    break;
                case ItemType.berry:

                    if (targetTag == "Ground")
                    {
                        Instantiate(itemDefine.Prefab, hitInfo.point + new Vector3(0, 1.5f, 0), Quaternion.identity);
                        Init(null);
                    }
                    break;
            }
        }
    }


    #endregion



    private void Update()
    {

        if (IsSlect && itemDefine != null && Input.GetMouseButtonDown(1))
        {
           // Debug.LogError("11111111111111111111111111111");
            if (PlayerController.Instance.UseItem(itemDefine.ItemType))
            {
                Init(null);
               // Debug.LogError("2222222222222222222222222222");
            }
        }
    }

    public void Init(ItemDefine itemDefine)
    {
        this.itemDefine = itemDefine;
        IsSlect = false;
       
        if (itemDefine == null)
        {
            icon.gameObject.SetActive(false);
        }
       
        else
        {
            icon.gameObject.SetActive(true);
            icon.sprite = itemDefine.Sprite;
        }
    }


}